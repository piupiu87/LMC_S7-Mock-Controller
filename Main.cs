using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AdlinkMockController
{
    public partial class Main : Form
    {
        public static List<StIOPointDef> ioList = new List<StIOPointDef>();
        private static CIOBase clsIO = null;
        private CScriptEngine _engine;

        // Motion
        private readonly Dictionary<int, AxisEntry> _axisEntries = new Dictionary<int, AxisEntry>();

        public Main()
        {
            InitializeComponent();
            InitializeIO();
            InitializeIOControls();
            InitializeScripts();
            InitializeMotionTab();

            lblJsonFilePath.Text = clsIO.GetMockStateJsonFilePath();

           // tabControl1.TabPages.Remove(tabPageMotion);
        }

        private void InitializeIO()
        {
            ioList = CDatabase.LoadDigitalIO();

            clsIO = new CIOAdlink(CIOAdlink.I_ADLINK_CARD_ID, CIOAdlink.I_ADLINK_CONN_INDEX_IO, CIOAdlink.I_ADLINK_CONN_INDEX_MTN);

            if (clsIO == null)
            {
                MessageBox.Show("Error Creating Mock IO Card");
                Application.Exit();
            }
            else
            {
                tmrUpdateStatus.Enabled = true;
            }
        }

        private void InitializeIOControls()
        {
            int inputBtnStartPosX = 6;
            int inputBtnStartPosY = 24;
            int inputLblStartPosX = 64;
            int inputLblStartPosY = 31;

            for (int i = 0; i < ioList.Count(); i++)
            {
                Label lbl = new Label();
                Button btn = new Button();
                StIOPointDef io = new StIOPointDef();

                io = ioList[i];
                btn.Name = $"IO{i}";
                btn.Text = "OFF";
                btn.BackColor = Color.Red;
                btn.Size = button1.Size;
                btn.Location = new Point(inputBtnStartPosX + i % 64 / 16 * 300, inputBtnStartPosY + i % 64 % 16 * 30);
                btn.Click += Btn_Click;
                btn.Visible = true;

                lbl.Text = io.sIOName;
                lbl.Size = label1.Size;
                lbl.Location = new Point(inputLblStartPosX + i % 64 / 16 * 300, inputLblStartPosY + i % 64 % 16 * 30);
                lbl.Visible = true;

                if (io.enIOTyp == EnIOType.Input)
                {
                    tabPageInput.Controls.Add(btn);
                    tabPageInput.Controls.Add(lbl);
                }
                else
                {
                    tabPageOutput.Controls.Add(btn);
                    tabPageOutput.Controls.Add(lbl);
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                int io_id = int.Parse(((Button)sender).Name.Replace("IO",string.Empty));
                ToggleIO(io_id);
            }
        }

        private void ToggleIO(int io_id)
        {
            clsIO.WriteIOPointOutput(ioList[io_id].shIO_ID, ioList[io_id].bNormallyOpen ? !ioList[io_id].bStatus : ioList[io_id].bStatus);
        }

        private void tmrUpdateStatus_Tick(object sender, EventArgs e)
        {
            clsIO.ReadIOPointStatus();

            for (int i = 0; i < ioList.Count(); i++)
            {
                var btn = ioList[i].enIOTyp == EnIOType.Input ? tabPageInput.Controls.Find($"IO{i}", true) : tabPageOutput.Controls.Find($"IO{i}", true);
                if (CScriptEngine.LogicalOn(ioList[i]))
                {
                    btn[0].Text = "ON";
                    btn[0].BackColor = Color.Lime;
                }
                else
                {
                    btn[0].Text = "OFF";
                    btn[0].BackColor = Color.Red;
                }
            }

            // Poll per-axis status for the Servo toggle + position readouts
            foreach (var kv in _axisEntries)
            {
                int axisId = kv.Key;
                bool servoOn = (CAps168.APS_motion_io_status(axisId) & 0x80) != 0;
                CAps168.APS_get_position(axisId, out int position);
                CAps168.APS_get_command(axisId, out int command);
                UpdateAxisEntry(kv.Value, servoOn, position, command);
            }

            _engine?.Tick();
        }

        private void btnSetJsonFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                short shErr = clsIO.SetMockStateJsonFilePath(openFileDialog.FileName);
                if (shErr != 0)
                {
                    MessageBox.Show("Failed to Set Json File Path!");
                }
                else
                {
                    lblJsonFilePath.Text = clsIO.GetMockStateJsonFilePath();
                }
            }
        }

        private void InitializeScripts()
        {
            _engine = new CScriptEngine(clsIO, this);

            dataGridViewScripts.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colFire",
                HeaderText = "",
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                UseColumnTextForButtonValue = false,
                Width = 120
            });
            dataGridViewScripts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Enabled",
                DataPropertyName = "Enabled",
                Width = 70
            });
            dataGridViewScripts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                DataPropertyName = "Name",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 60,
                ReadOnly = true
            });
            dataGridViewScripts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Mode",
                Name = "colFireMode",
                ReadOnly = true,
                Width = 80
            });
            dataGridViewScripts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Trigger Groups",
                Name = "colTriggerGroups",
                ReadOnly = true,
                Width = 110
            });
            dataGridViewScripts.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Actions",
                Name = "colActions",
                ReadOnly = true,
                Width = 90
            });
            dataGridViewScripts.AutoGenerateColumns = false;
            dataGridViewScripts.DataSource = _engine.Scripts;
            dataGridViewScripts.CellFormatting += DataGridViewScripts_CellFormatting;
            dataGridViewScripts.CellValueChanged += DataGridViewScripts_CellValueChanged;
            dataGridViewScripts.CurrentCellDirtyStateChanged += DataGridViewScripts_CurrentCellDirtyStateChanged;

            try
            {
                _engine.LoadFromFile(CScriptEngine.DefaultPath);
                lblScriptStatus.Text = System.IO.File.Exists(CScriptEngine.DefaultPath)
                    ? $"Loaded {_engine.Scripts.Count} script(s) from {CScriptEngine.DefaultPath}"
                    : $"No scripts file yet ({CScriptEngine.DefaultPath})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load scripts: {ex.Message}", "Scripts", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewScripts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _engine.Scripts.Count) return;
            var s = _engine.Scripts[e.RowIndex];
            var col = dataGridViewScripts.Columns[e.ColumnIndex];
            if (col.Name == "colFire")
            {
                e.Value = s.Name;
                e.CellStyle.BackColor = s.ButtonColor;
                e.FormattingApplied = true;
            }
            else if (col.Name == "colFireMode")
            {
                e.Value = s.FireMode == ScriptFireMode.Continuous ? "Continuous" : "Edge";
                e.FormattingApplied = true;
            }
            else if (col.Name == "colTriggerGroups")
            {
                e.Value = s.TriggerGroups.Count.ToString();
                e.FormattingApplied = true;
            }
            else if (col.Name == "colActions")
            {
                e.Value = s.Actions.Count.ToString();
                e.FormattingApplied = true;
            }
        }

        private void DataGridViewScripts_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewScripts.IsCurrentCellDirty && dataGridViewScripts.CurrentCell is DataGridViewCheckBoxCell)
            {
                dataGridViewScripts.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridViewScripts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _engine.Scripts.Count) return;
            var col = dataGridViewScripts.Columns[e.ColumnIndex];
            if (col.DataPropertyName == "Enabled")
            {
                _engine.ResetEdgeState(_engine.Scripts[e.RowIndex]);
            }
        }

        private void btnNewScript_Click(object sender, EventArgs e)
        {
            var seed = new Script { Name = "New Script", Enabled = true };
            using (var editor = new ScriptEditor(seed))
            {
                if (editor.ShowDialog(this) == DialogResult.OK && editor.Result != null)
                {
                    _engine.Add(editor.Result);
                    dataGridViewScripts.Refresh();
                }
            }
        }

        private void btnEditScript_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedScript();
            if (selected == null) return;
            EditScript(selected);
        }

        private void dataGridViewScripts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= _engine.Scripts.Count) return;
            if (dataGridViewScripts.Columns[e.ColumnIndex].Name != "colFire") return;
            _engine.ExecuteActions(_engine.Scripts[e.RowIndex]);
        }

        private void dataGridViewScripts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _engine.Scripts.Count) return;
            var colName = dataGridViewScripts.Columns[e.ColumnIndex].Name;
            if (colName == "colFire") return;
            if (dataGridViewScripts.Columns[e.ColumnIndex].DataPropertyName == "Enabled") return;
            EditScript(_engine.Scripts[e.RowIndex]);
        }

        private void EditScript(Script script)
        {
            using (var editor = new ScriptEditor(script))
            {
                if (editor.ShowDialog(this) == DialogResult.OK && editor.Result != null)
                {
                    int idx = _engine.Scripts.IndexOf(script);
                    if (idx >= 0)
                    {
                        _engine.Scripts[idx] = editor.Result;
                        _engine.ResetEdgeState(editor.Result);
                    }
                    dataGridViewScripts.Refresh();
                }
            }
        }

        private void btnDeleteScript_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedScript();
            if (selected == null) return;
            if (MessageBox.Show(this, $"Delete script \"{selected.Name}\"?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _engine.Remove(selected);
            }
        }

        private void btnSaveScripts_Click(object sender, EventArgs e)
        {
            try
            {
                _engine.SaveToFile(CScriptEngine.DefaultPath);
                lblScriptStatus.Text = $"Saved {_engine.Scripts.Count} script(s) to {CScriptEngine.DefaultPath}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save scripts: {ex.Message}", "Scripts", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadScripts_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "json files (*.json)|*.json" })
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    _engine.LoadFromFile(dlg.FileName);
                    dataGridViewScripts.Refresh();
                    lblScriptStatus.Text = $"Loaded {_engine.Scripts.Count} script(s) from {dlg.FileName}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load scripts: {ex.Message}", "Scripts", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Script GetSelectedScript()
        {
            if (dataGridViewScripts.CurrentRow == null) return null;
            int idx = dataGridViewScripts.CurrentRow.Index;
            if (idx < 0 || idx >= _engine.Scripts.Count) return null;
            return _engine.Scripts[idx];
        }

        // ── Motion Tab ────────────────────────────────────────────────────────

        private void InitializeMotionTab()
        {
            var configs = LoadAxisConfigs();
            int rows = (configs.Count + 1) / 2;
            tblMotionAxes.RowStyles.Clear();
            tblMotionAxes.RowCount = Math.Max(1, rows);
            for (int r = 0; r < tblMotionAxes.RowCount; r++)
                tblMotionAxes.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            foreach (var cfg in configs)
            {
                var entry = BuildAxisEntry(cfg.AxisId, cfg.Name);
                entry.GrpAxis.Dock = DockStyle.Fill;
                _axisEntries[cfg.AxisId] = entry;
                tblMotionAxes.Controls.Add(entry.GrpAxis);
            }
        }

        private static List<AxisConfig> LoadAxisConfigs()
        {
            string path = Path.Combine(Application.StartupPath, "axes.json");
            if (File.Exists(path))
            {
                try
                {
                    var configs = JsonConvert.DeserializeObject<List<AxisConfig>>(File.ReadAllText(path));
                    if (configs != null && configs.Count > 0) return configs;
                }
                catch { }
            }
            return Enumerable.Range(0, 8)
                .Select(i => new AxisConfig { AxisId = i, Name = $"Axis {i}" })
                .ToList();
        }

        private sealed class AxisEntry
        {
            public int AxisId;
            public bool LastServoOn;
            public GroupBox GrpAxis;
            public Button   BtnServo;
            public Label    LblPosVal;
            public Label    LblCmdVal;
        }

        private AxisEntry BuildAxisEntry(int axisId, string axisName)
        {
            var e = new AxisEntry { AxisId = axisId };
            var grp = new GroupBox
            {
                Text   = $"Axis {axisId}: {axisName}",
                Font   = grpAxis.Font,
                Size   = grpAxis.Size,
                Margin = new Padding(3)
            };
            e.GrpAxis = grp;

            var servo = new Button
            {
                Text                    = btnServo.Text,
                Font                    = btnServo.Font,
                Location                = btnServo.Location,
                Size                    = btnServo.Size,
                BackColor               = btnServo.BackColor,
                UseVisualStyleBackColor = btnServo.UseVisualStyleBackColor
            };
            servo.Click += (s, ev) => CAps168.APS_set_servo_on(axisId, e.LastServoOn ? 0 : 1);
            grp.Controls.Add(servo);
            e.BtnServo = servo;

            var reset = new Button
            {
                Text                    = btnResetErr.Text,
                Font                    = btnResetErr.Font,
                Location                = btnResetErr.Location,
                Size                    = btnResetErr.Size,
                UseVisualStyleBackColor = btnResetErr.UseVisualStyleBackColor
            };
            reset.Click += (s, ev) => CAps168.APS_reset_error(axisId);
            grp.Controls.Add(reset);

            Label CloneLbl(Label tmpl, string overrideText = null)
            {
                var l = new Label
                {
                    Text     = overrideText ?? tmpl.Text,
                    Font     = tmpl.Font,
                    Location = tmpl.Location,
                    Size     = tmpl.Size,
                    AutoSize = false
                };
                grp.Controls.Add(l);
                return l;
            }

            CloneLbl(lblPosHdr);
            e.LblPosVal = CloneLbl(lblPosVal, "0");
            CloneLbl(lblCmdHdr);
            e.LblCmdVal = CloneLbl(lblCmdVal, "0");

            return e;
        }

        private static void UpdateAxisEntry(AxisEntry e, bool servoOn, int position, int command)
        {
            e.LastServoOn = servoOn;
            if (servoOn) { e.BtnServo.Text = "SERVO ON";  e.BtnServo.BackColor = Color.Lime; }
            else         { e.BtnServo.Text = "SERVO OFF"; e.BtnServo.BackColor = Color.Red;  }
            e.LblPosVal.Text = position.ToString();
            e.LblCmdVal.Text = command.ToString();
        }
    }
}
