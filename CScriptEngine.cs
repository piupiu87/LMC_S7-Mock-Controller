using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdlinkMockController
{
    class CScriptEngine
    {
        private readonly CIOBase _io;
        private readonly Control _uiContext;
        private readonly BindingList<Script> _scripts = new BindingList<Script>();

        public BindingList<Script> Scripts => _scripts;

        public static string DefaultPath => Path.Combine(Application.StartupPath, "scripts.json");

        public CScriptEngine(CIOBase io, Control uiContext)
        {
            _io = io;
            _uiContext = uiContext;
        }

        public void Add(Script s)    => _scripts.Add(s);
        public void Remove(Script s) => _scripts.Remove(s);

        public void ReplaceAll(IEnumerable<Script> scripts)
        {
            _scripts.Clear();
            if (scripts == null) return;
            foreach (var s in scripts) { s.LastEvaluation = false; _scripts.Add(s); }
        }

        public void ResetEdgeState(Script s) => s.LastEvaluation = false;

        public void Tick()
        {
            for (int i = 0; i < _scripts.Count; i++)
            {
                var s = _scripts[i];
                if (!s.Enabled) { s.LastEvaluation = false; continue; }
                bool now = Evaluate(s);
                if (now && !s.LastEvaluation) ExecuteActions(s);
                s.LastEvaluation = now;
            }
        }

        public void ExecuteActions(Script s)
        {
            var actions = s.Actions.ToList();
            Task.Run(async () =>
            {
                foreach (var a in actions)
                    await ExecuteAction(a);
            });
        }

        private Task ExecuteAction(ScriptAction a)
        {
            switch (a.Type)
            {
                case ScriptActionType.Delay:
                    return Task.Delay(a.DelayMs > 0 ? a.DelayMs : 0);

                case ScriptActionType.IOWrite:
                    DispatchToUI(() => _io.WriteIOPointOutput(a.IOId, a.IOOn));
                    return Task.CompletedTask;

                case ScriptActionType.ServoOn:
                    CAps168.APS_set_servo_on(a.AxisId, a.IOOn ? 1 : 0);
                    return Task.CompletedTask;

                case ScriptActionType.MotionAbsolute:
                    CAps168.APS_absolute_move(a.AxisId, a.Position, a.Speed);
                    return Task.CompletedTask;

                case ScriptActionType.MotionRelative:
                    CAps168.APS_relative_move(a.AxisId, a.Position, a.Speed);
                    return Task.CompletedTask;

                case ScriptActionType.MotionVelocity:
                    CAps168.APS_velocity_move(a.AxisId, a.Speed);
                    return Task.CompletedTask;

                case ScriptActionType.MotionHome:
                    CAps168.APS_home_move(a.AxisId);
                    return Task.CompletedTask;

                case ScriptActionType.MotionStop:
                    CAps168.APS_stop_move(a.AxisId);
                    return Task.CompletedTask;

                case ScriptActionType.MotionEmgStop:
                    CAps168.APS_emg_stop(a.AxisId);
                    return Task.CompletedTask;

                case ScriptActionType.ResetError:
                    CAps168.APS_reset_error(a.AxisId);
                    return Task.CompletedTask;

                case ScriptActionType.InjectMotionStatus:
                    InjectStatusBit("motionStatus", a.AxisId, a.Position, a.IOOn);
                    return Task.CompletedTask;

                case ScriptActionType.InjectMotionIOStatus:
                    InjectStatusBit("motionIOStatus", a.AxisId, a.Position, a.IOOn);
                    return Task.CompletedTask;

                default:
                    return Task.CompletedTask;
            }
        }

        private void DispatchToUI(Action action)
        {
            if (_uiContext == null || !_uiContext.IsHandleCreated || _uiContext.IsDisposed) return;
            try { _uiContext.BeginInvoke(action); }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        private bool Evaluate(Script s)
        {
            if (s.TriggerGroups == null || s.TriggerGroups.Count == 0) return false;
            foreach (var g in s.TriggerGroups)
            {
                if (g.Conditions == null || g.Conditions.Count == 0) continue;
                bool groupMatches = true;
                foreach (var c in g.Conditions)
                    if (!EvaluateCondition(c)) { groupMatches = false; break; }
                if (groupMatches) return true;
            }
            return false;
        }

        private static bool EvaluateCondition(TriggerCondition c)
        {
            switch (c.Type)
            {
                case TriggerType.AxisPosition:
                    int actual;
                    if (c.Source == PositionSource.Command)
                        CAps168.APS_get_command(c.AxisId, out actual);
                    else
                        CAps168.APS_get_position(c.AxisId, out actual);
                    return Compare(actual, c.Position, c.Comparator);

                case TriggerType.IO:
                default:
                    var io = Main.ioList.FirstOrDefault(x => x.shIO_ID == c.ShIOId);
                    if (io == null) return false;
                    return LogicalOn(io) == c.ExpectedOn;
            }
        }

        private static bool Compare(int actual, int threshold, PositionComparator op)
        {
            switch (op)
            {
                case PositionComparator.Equal:          return actual == threshold;
                case PositionComparator.NotEqual:       return actual != threshold;
                case PositionComparator.Less:           return actual <  threshold;
                case PositionComparator.LessOrEqual:    return actual <= threshold;
                case PositionComparator.Greater:        return actual >  threshold;
                case PositionComparator.GreaterOrEqual: return actual >= threshold;
                default:                                return false;
            }
        }

        public static bool LogicalOn(StIOPointDef io) => io.bNormallyOpen ? io.bStatus : !io.bStatus;

        internal static void InjectStatusBit(string field, int axisId, int bitMask, bool value)
        {
            var sb = new StringBuilder(260);
            if (CAps168.APS_mock_get_state_path(sb, sb.Capacity) != 0) return;
            string path = sb.ToString();
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;

            try
            {
                var root = JObject.Parse(File.ReadAllText(path));
                var axes = root["axes"] as JArray;
                if (axes == null) return;
                foreach (var axNode in axes)
                {
                    if ((int)axNode["id"] != axisId) continue;
                    int current = (int)axNode[field];
                    axNode[field] = value ? (current | bitMask) : (current & ~bitMask);
                    break;
                }
                File.WriteAllText(path, root.ToString(Formatting.None));
            }
            catch { /* best-effort; DLL may briefly hold the file */ }
        }

        public void LoadFromFile(string path)
        {
            if (!File.Exists(path)) { _scripts.Clear(); return; }
            var loaded = JsonConvert.DeserializeObject<List<Script>>(File.ReadAllText(path))
                         ?? new List<Script>();
            ReplaceAll(loaded);
        }

        public void SaveToFile(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(_scripts.ToList(), Formatting.Indented));
        }
    }
}
