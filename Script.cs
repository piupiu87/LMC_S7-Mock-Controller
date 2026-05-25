using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;

namespace AdlinkMockController
{
    public enum TriggerType
    {
        IO,
        AxisPosition
    }

    public enum PositionComparator
    {
        Equal,
        NotEqual,
        Less,
        LessOrEqual,
        Greater,
        GreaterOrEqual
    }

    public enum PositionSource
    {
        Feedback,
        Command
    }

    public class TriggerCondition
    {
        public TriggerType Type { get; set; } = TriggerType.IO;

        // IO
        public short ShIOId     { get; set; }
        public bool  ExpectedOn { get; set; }

        // AxisPosition
        public int                AxisId     { get; set; }
        public PositionComparator Comparator { get; set; } = PositionComparator.GreaterOrEqual;
        public int                Position   { get; set; }
        public PositionSource     Source     { get; set; } = PositionSource.Feedback;
    }

    public class TriggerGroup
    {
        public List<TriggerCondition> Conditions { get; set; } = new List<TriggerCondition>();
    }

    public enum ScriptFireMode
    {
        RisingEdge,
        Continuous
    }

    public enum ScriptActionType
    {
        IOWrite,
        ServoOn,
        MotionAbsolute,
        MotionRelative,
        MotionVelocity,
        MotionHome,
        MotionStop,
        MotionEmgStop,
        ResetError,
        InjectMotionStatus,
        InjectMotionIOStatus,
        Delay
    }

    public class ScriptAction
    {
        public ScriptActionType Type { get; set; } = ScriptActionType.IOWrite;

        // IOWrite
        public short IOId { get; set; }
        public bool  IOOn { get; set; }   // also: ServoOn flag; InjectMotion* bit value

        // All axis-based actions
        public int AxisId { get; set; }

        // MotionAbsolute / MotionRelative: target or distance
        // InjectMotionStatus / InjectMotionIOStatus: bit mask (IOOn = set/clear)
        public int Position { get; set; }

        // MotionAbsolute / MotionRelative / MotionVelocity
        public int Speed { get; set; }

        // Delay
        public int DelayMs { get; set; }
    }

    public class Script
    {
        public string         Name     { get; set; }
        public bool           Enabled  { get; set; } = true;
        public ScriptFireMode FireMode { get; set; } = ScriptFireMode.RisingEdge;

        public int ButtonColorArgb { get; set; } = System.Drawing.Color.SteelBlue.ToArgb();

        [JsonIgnore]
        public Color ButtonColor
        {
            get => Color.FromArgb(ButtonColorArgb);
            set => ButtonColorArgb = value.ToArgb();
        }

        public List<TriggerGroup> TriggerGroups { get; set; } = new List<TriggerGroup>();
        public List<ScriptAction> Actions       { get; set; } = new List<ScriptAction>();

        [JsonIgnore]
        public bool LastEvaluation { get; set; }

        // 0 = idle, 1 = an ExecuteActions task is in flight. Used by CScriptEngine
        // to prevent overlapping runs in Continuous mode (where Tick would
        // otherwise spawn a new Task every ~100ms while triggers stay true).
        [JsonIgnore]
        public int RunningFlag;
    }
}
