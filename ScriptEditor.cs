using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AdlinkMockController
{
    public partial class ScriptEditor : Form
    {
        private class IOEntry { public short Id { get; set; } public string Display { get; set; } }

        private class MaskEntry { public int Value { get; set; } public string Display { get; set; } }

        private Color _chosenColor;
        private readonly Script _working;
        private readonly List<IOEntry> _ioEntries;
        private readonly List<BindingList<TriggerCondition>> _groupBindings = new List<BindingList<TriggerCondition>>();
        private BindingList<ScriptAction> _actionBinding;

        private static readonly List<MaskEntry> MotionStatusBits =
            System.Enum.GetValues(typeof(APS_4XMO_Motion_Status))
                .Cast<APS_4XMO_Motion_Status>()
                .Select(v => new MaskEntry { Value = (int)v, Display = $"{v} (0x{(int)v:X})" })
                .ToList();

        private static readonly List<MaskEntry> MotionIOStatusBits =
            System.Enum.GetValues(typeof(APS_4XMO_Motion_IO_Status))
                .Cast<APS_4XMO_Motion_IO_Status>()
                .Select(v => new MaskEntry { Value = (int)v, Display = $"{v} (0x{(int)v:X})" })
                .ToList();

        public Script Result { get; private set; }

        public ScriptEditor(Script source)
        {
            InitializeComponent();

            _working = Clone(source);
            _chosenColor = source.ButtonColor;

            _ioEntries = Main.ioList
                .Select(io => new IOEntry { Id = io.shIO_ID, Display = $"{io.shIO_ID}: {io.sIOName}" })
                .ToList();

            txtName.Text       = _working.Name ?? string.Empty;
            chkEnabled.Checked = _working.Enabled;
            btnColor.BackColor = _chosenColor;

            cmbFireMode.DataSource = new[]
            {
                new { Value = ScriptFireMode.RisingEdge, Display = "Rising edge (fire once per trigger)" },
                new { Value = ScriptFireMode.Continuous, Display = "Continuous (fire every tick while true)" }
            }.ToList();
            cmbFireMode.DisplayMember = "Display";
            cmbFireMode.ValueMember   = "Value";
            cmbFireMode.SelectedValue = _working.FireMode;

            BuildActionsGrid();
            BuildAllGroups();
        }

        private void DataError(object sender, DataGridViewDataErrorEventArgs e) { e.ThrowException = false; }

        // ── Actions grid ──────────────────────────────────────────────────────

        private void BuildActionsGrid()
        {
            dgvActions.DataError += DataError;
            dgvActions.Columns.Clear();

            dgvActions.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colActType", HeaderText = "Type", DataPropertyName = "Type",
                DataSource = Enum.GetValues(typeof(ScriptActionType)),
                Width = 148
            });

            var ioCol = new DataGridViewComboBoxColumn
            {
                Name = "colActIO", HeaderText = "IO", DataPropertyName = "IOId",
                DataSource = _ioEntries, DisplayMember = "Display", ValueMember = "Id",
                ValueType = typeof(short), Width = 160
            };
            if (_ioEntries.Count == 0) ioCol.ReadOnly = true;
            dgvActions.Columns.Add(ioCol);

            dgvActions.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colActOn", HeaderText = "ON?", DataPropertyName = "IOOn", Width = 46
            });
            dgvActions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colActAxis", HeaderText = "Axis #", DataPropertyName = "AxisId", Width = 50
            });
            dgvActions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colActPos", HeaderText = "Pos / Dist / Mask", DataPropertyName = "Position", Width = 110
            });
            dgvActions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colActSpd", HeaderText = "Speed", DataPropertyName = "Speed", Width = 70
            });
            dgvActions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colActDelay", HeaderText = "Delay (ms)", DataPropertyName = "DelayMs", Width = 80
            });

            _actionBinding = new BindingList<ScriptAction>(_working.Actions);
            dgvActions.DataSource = _actionBinding;
            dgvActions.CellFormatting += DgvActions_CellFormatting;
            dgvActions.CellBeginEdit  += DgvActions_CellBeginEdit;
            dgvActions.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvActions.IsCurrentCellDirty && dgvActions.CurrentCell is DataGridViewComboBoxCell)
                    dgvActions.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dgvActions.CellValueChanged += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                if (dgvActions.Columns[e.ColumnIndex].Name == "colActType")
                    RefreshActionPosCell(e.RowIndex);
                dgvActions.InvalidateRow(e.RowIndex);
            };
            dgvActions.RowsAdded += (s, e) =>
            {
                for (int i = 0; i < e.RowCount; i++) RefreshActionPosCell(e.RowIndex + i);
            };

            for (int r = 0; r < _actionBinding.Count; r++) RefreshActionPosCell(r);
        }

        private void RefreshActionPosCell(int row)
        {
            if (row < 0 || row >= _actionBinding.Count) return;
            var a = _actionBinding[row];
            int colIdx = dgvActions.Columns["colActPos"].Index;
            var current = dgvActions.Rows[row].Cells[colIdx];

            if (a.Type == ScriptActionType.InjectMotionStatus || a.Type == ScriptActionType.InjectMotionIOStatus)
            {
                var items = a.Type == ScriptActionType.InjectMotionStatus ? MotionStatusBits : MotionIOStatusBits;
                if (!(current is DataGridViewComboBoxCell combo) || combo.DataSource != items)
                {
                    combo = new DataGridViewComboBoxCell
                    {
                        DataSource    = items,
                        DisplayMember = "Display",
                        ValueMember   = "Value",
                        ValueType     = typeof(int),
                        FlatStyle     = FlatStyle.Flat
                    };
                    dgvActions.Rows[row].Cells[colIdx] = combo;
                }
            }
            else
            {
                if (!(current is DataGridViewTextBoxCell))
                    dgvActions.Rows[row].Cells[colIdx] = new DataGridViewTextBoxCell();
            }
        }

        private void DgvActions_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!IsActionCellApplicable(e.RowIndex, e.ColumnIndex)) e.Cancel = true;
        }

        private void DgvActions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex == 0) return;
            if (!IsActionCellApplicable(e.RowIndex, e.ColumnIndex))
            {
                e.CellStyle.BackColor = SystemColors.Control;
                e.CellStyle.ForeColor = SystemColors.GrayText;
                e.Value = "—";
                e.FormattingApplied = true;
            }
        }

        private bool IsActionCellApplicable(int row, int col)
        {
            if (row < 0 || row >= _actionBinding.Count) return true;
            var a = _actionBinding[row];
            var name = dgvActions.Columns[col].Name;
            switch (a.Type)
            {
                case ScriptActionType.IOWrite:
                    return name == "colActType" || name == "colActIO" || name == "colActOn";
                case ScriptActionType.ServoOn:
                    return name == "colActType" || name == "colActAxis" || name == "colActOn";
                case ScriptActionType.MotionAbsolute:
                case ScriptActionType.MotionRelative:
                    return name == "colActType" || name == "colActAxis" || name == "colActPos" || name == "colActSpd";
                case ScriptActionType.MotionVelocity:
                    return name == "colActType" || name == "colActAxis" || name == "colActSpd";
                case ScriptActionType.MotionHome:
                case ScriptActionType.MotionStop:
                case ScriptActionType.MotionEmgStop:
                case ScriptActionType.ResetError:
                    return name == "colActType" || name == "colActAxis";
                case ScriptActionType.InjectMotionStatus:
                case ScriptActionType.InjectMotionIOStatus:
                    return name == "colActType" || name == "colActAxis" || name == "colActPos" || name == "colActOn";
                case ScriptActionType.Delay:
                    return name == "colActType" || name == "colActDelay";
                default:
                    return name == "colActType";
            }
        }

        // ── Trigger condition cell mask ───────────────────────────────────────

        private static bool IsConditionCellApplicable(DataGridView dgv, BindingList<TriggerCondition> binding, int row, int col)
        {
            if (row < 0 || row >= binding.Count) return true;
            if (col < 0) return true;
            var c = binding[row];
            var name = dgv.Columns[col].Name;
            switch (c.Type)
            {
                case TriggerType.AxisPosition:
                    return name == "colCondType" || name == "colCondAxis"
                        || name == "colCondCmp"  || name == "colCondPos" || name == "colCondSrc";
                case TriggerType.IO:
                default:
                    return name == "colCondType" || name == "colCondIO" || name == "colCondExpOn";
            }
        }

        private static void FormatConditionCell(DataGridView dgv, BindingList<TriggerCondition> binding, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgv.Columns[e.ColumnIndex].Name == "colCondType") return;
            if (!IsConditionCellApplicable(dgv, binding, e.RowIndex, e.ColumnIndex))
            {
                e.CellStyle.BackColor = SystemColors.Control;
                e.CellStyle.ForeColor = SystemColors.GrayText;
                e.Value = "—";
                e.FormattingApplied = true;
            }
        }

        // ── Trigger groups ────────────────────────────────────────────────────

        private void BuildAllGroups()
        {
            flowGroups.Controls.Clear();
            _groupBindings.Clear();
            for (int i = 0; i < _working.TriggerGroups.Count; i++)
                AddGroupPanel(_working.TriggerGroups[i]);
        }

        private void AddGroupPanel(TriggerGroup group)
        {
            var box = new GroupBox
            {
                Width = 714,
                Height = 170,
                Margin = new Padding(3, 3, 3, 6)
            };
            int idx = _groupBindings.Count + 1;
            box.Text = $"Group {idx}  (AND)";

            var btnAddRow = new Button
            {
                Text = "+ Row", Location = new System.Drawing.Point(8, 16),
                Size = new System.Drawing.Size(70, 23), UseVisualStyleBackColor = true
            };
            var btnRemoveRow = new Button
            {
                Text = "− Row", Location = new System.Drawing.Point(82, 16),
                Size = new System.Drawing.Size(70, 23), UseVisualStyleBackColor = true
            };
            var btnRemoveGroup = new Button
            {
                Text = "Remove Group", Location = new System.Drawing.Point(590, 16),
                Size = new System.Drawing.Size(110, 23), UseVisualStyleBackColor = true
            };

            var dgv = new DataGridView
            {
                Location = new System.Drawing.Point(8, 45),
                Size = new System.Drawing.Size(696, 115),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                RowHeadersWidth = 30
            };
            dgv.DataError += DataError;

            dgv.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colCondType", HeaderText = "Type", DataPropertyName = "Type",
                DataSource = Enum.GetValues(typeof(TriggerType)), Width = 100
            });

            var ioCol = new DataGridViewComboBoxColumn
            {
                Name = "colCondIO", HeaderText = "IO", DataPropertyName = "ShIOId",
                DataSource = _ioEntries, DisplayMember = "Display", ValueMember = "Id",
                ValueType = typeof(short),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, FillWeight = 60
            };
            if (_ioEntries.Count == 0) ioCol.ReadOnly = true;
            dgv.Columns.Add(ioCol);

            dgv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colCondExpOn", HeaderText = "Exp. ON", DataPropertyName = "ExpectedOn", Width = 60
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCondAxis", HeaderText = "Axis #", DataPropertyName = "AxisId", Width = 50
            });
            dgv.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colCondCmp", HeaderText = "Cmp", DataPropertyName = "Comparator",
                DataSource = Enum.GetValues(typeof(PositionComparator)), Width = 90
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCondPos", HeaderText = "Position", DataPropertyName = "Position", Width = 80
            });
            dgv.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "colCondSrc", HeaderText = "Source", DataPropertyName = "Source",
                DataSource = Enum.GetValues(typeof(PositionSource)), Width = 80
            });

            var binding = new BindingList<TriggerCondition>(group.Conditions);
            dgv.DataSource = binding;
            _groupBindings.Add(binding);

            dgv.CellFormatting += (s, e) => FormatConditionCell(dgv, binding, e);
            dgv.CellBeginEdit  += (s, e) =>
            {
                if (!IsConditionCellApplicable(dgv, binding, e.RowIndex, e.ColumnIndex)) e.Cancel = true;
            };
            dgv.CellValueChanged += (s, e) => { if (e.RowIndex >= 0) dgv.InvalidateRow(e.RowIndex); };

            dgv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgv.IsCurrentCellDirty && dgv.CurrentCell is DataGridViewComboBoxCell)
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            btnAddRow.Click += (s, e) =>
            {
                var cond = new TriggerCondition { ExpectedOn = true };
                if (_ioEntries.Count > 0) cond.ShIOId = _ioEntries[0].Id;
                binding.Add(cond);
            };
            btnRemoveRow.Click += (s, e) =>
            {
                if (dgv.CurrentRow != null && dgv.CurrentRow.Index >= 0 && dgv.CurrentRow.Index < binding.Count)
                    binding.RemoveAt(dgv.CurrentRow.Index);
            };
            btnRemoveGroup.Click += (s, e) =>
            {
                _working.TriggerGroups.Remove(group);
                BuildAllGroups();
            };

            box.Controls.Add(btnAddRow);
            box.Controls.Add(btnRemoveRow);
            box.Controls.Add(btnRemoveGroup);
            box.Controls.Add(dgv);
            flowGroups.Controls.Add(box);
        }

        // ── Toolbar handlers ──────────────────────────────────────────────────

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            var g = new TriggerGroup();
            _working.TriggerGroups.Add(g);
            AddGroupPanel(g);
        }

        private void btnAddAction_Click(object sender, EventArgs e)
        {
            var a = new ScriptAction { Type = ScriptActionType.Delay, DelayMs = 100 };
            _actionBinding.Add(a);
        }

        private void btnRemoveAction_Click(object sender, EventArgs e)
        {
            int idx = dgvActions.CurrentRow?.Index ?? -1;
            if (idx >= 0 && idx < _actionBinding.Count) _actionBinding.RemoveAt(idx);
        }

        private void btnActUp_Click(object sender, EventArgs e)   => MoveRow(_actionBinding, dgvActions, -1);
        private void btnActDown_Click(object sender, EventArgs e) => MoveRow(_actionBinding, dgvActions,  1);

        private static void MoveRow<T>(BindingList<T> list, DataGridView dgv, int delta)
        {
            int idx  = dgv.CurrentRow?.Index ?? -1;
            int dest = idx + delta;
            if (idx < 0 || dest < 0 || dest >= list.Count) return;
            var item = list[idx];
            list.RemoveAt(idx);
            list.Insert(dest, item);
            dgv.ClearSelection();
            if (dest < dgv.Rows.Count) dgv.Rows[dest].Selected = true;
        }

        // ── Color picker ──────────────────────────────────────────────────────

        private void btnColor_Click(object sender, EventArgs e)
        {
            using (var dlg = new ColorDialog { Color = _chosenColor, FullOpen = true })
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    _chosenColor = dlg.Color;
                    btnColor.BackColor = _chosenColor;
                }
            }
        }

        // ── OK ────────────────────────────────────────────────────────────────

        private void btnOK_Click(object sender, EventArgs e)
        {
            dgvActions.EndEdit();
            foreach (Control c in flowGroups.Controls)
            {
                if (!(c is GroupBox gb)) continue;
                foreach (Control gc in gb.Controls)
                    (gc as DataGridView)?.EndEdit();
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(this, "Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            _working.Name            = txtName.Text.Trim();
            _working.Enabled         = chkEnabled.Checked;
            _working.ButtonColor     = _chosenColor;
            if (cmbFireMode.SelectedValue is ScriptFireMode mode)
                _working.FireMode = mode;
            Result = _working;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ── Clone ─────────────────────────────────────────────────────────────

        private static Script Clone(Script s)
        {
            var copy = new Script
            {
                Name            = s.Name,
                Enabled         = s.Enabled,
                FireMode        = s.FireMode,
                ButtonColorArgb = s.ButtonColorArgb
            };
            foreach (var g in s.TriggerGroups)
            {
                var ng = new TriggerGroup();
                foreach (var c in g.Conditions)
                    ng.Conditions.Add(new TriggerCondition
                    {
                        Type       = c.Type,
                        ShIOId     = c.ShIOId,
                        ExpectedOn = c.ExpectedOn,
                        AxisId     = c.AxisId,
                        Comparator = c.Comparator,
                        Position   = c.Position,
                        Source     = c.Source
                    });
                copy.TriggerGroups.Add(ng);
            }
            foreach (var a in s.Actions)
                copy.Actions.Add(new ScriptAction
                {
                    Type = a.Type, IOId = a.IOId, IOOn = a.IOOn, AxisId = a.AxisId,
                    Position = a.Position, Speed = a.Speed, DelayMs = a.DelayMs
                });
            return copy;
        }
    }
}
