namespace AdlinkMockController
{
    partial class ScriptEditor
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.lblColorHdr = new System.Windows.Forms.Label();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblFireMode = new System.Windows.Forms.Label();
            this.cmbFireMode = new System.Windows.Forms.ComboBox();
            this.lblTriggers = new System.Windows.Forms.Label();
            this.lblTriggersHint = new System.Windows.Forms.Label();
            this.panelGroups = new System.Windows.Forms.Panel();
            this.flowGroups = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.lblActions = new System.Windows.Forms.Label();
            this.btnAddAction = new System.Windows.Forms.Button();
            this.btnRemoveAction = new System.Windows.Forms.Button();
            this.btnActUp = new System.Windows.Forms.Button();
            this.btnActDown = new System.Windows.Forms.Button();
            this.dgvActions = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).BeginInit();
            this.SuspendLayout();
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Text = "Name:";
            //
            // txtName
            //
            this.txtName.Location = new System.Drawing.Point(56, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(220, 20);
            //
            // chkEnabled
            //
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(286, 14);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Text = "Enabled";
            //
            // lblColorHdr
            //
            this.lblColorHdr.AutoSize = false;
            this.lblColorHdr.Location = new System.Drawing.Point(368, 15);
            this.lblColorHdr.Name = "lblColorHdr";
            this.lblColorHdr.Size = new System.Drawing.Size(36, 16);
            this.lblColorHdr.Text = "Color:";
            //
            // btnColor
            //
            this.btnColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColor.Location = new System.Drawing.Point(407, 9);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(50, 24);
            this.btnColor.TabIndex = 2;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            //
            // lblFireMode
            //
            this.lblFireMode.AutoSize = true;
            this.lblFireMode.Location = new System.Drawing.Point(475, 15);
            this.lblFireMode.Name = "lblFireMode";
            this.lblFireMode.Text = "Mode:";
            //
            // cmbFireMode
            //
            this.cmbFireMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFireMode.Location = new System.Drawing.Point(515, 12);
            this.cmbFireMode.Name = "cmbFireMode";
            this.cmbFireMode.Size = new System.Drawing.Size(160, 21);
            //
            // lblTriggers
            //
            this.lblTriggers.AutoSize = true;
            this.lblTriggers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTriggers.Location = new System.Drawing.Point(12, 45);
            this.lblTriggers.Name = "lblTriggers";
            this.lblTriggers.Text = "Triggers";
            //
            // lblTriggersHint
            //
            this.lblTriggersHint.AutoSize = true;
            this.lblTriggersHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTriggersHint.Location = new System.Drawing.Point(72, 45);
            this.lblTriggersHint.Name = "lblTriggersHint";
            this.lblTriggersHint.Text = "Groups are OR\'d; conditions within a group are AND\'d.";
            //
            // panelGroups
            //
            this.panelGroups.AutoScroll = true;
            this.panelGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGroups.Controls.Add(this.flowGroups);
            this.panelGroups.Location = new System.Drawing.Point(12, 65);
            this.panelGroups.Name = "panelGroups";
            this.panelGroups.Size = new System.Drawing.Size(736, 240);
            //
            // flowGroups
            //
            this.flowGroups.AutoSize = true;
            this.flowGroups.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowGroups.Location = new System.Drawing.Point(0, 0);
            this.flowGroups.Name = "flowGroups";
            this.flowGroups.Size = new System.Drawing.Size(716, 0);
            this.flowGroups.WrapContents = false;
            //
            // btnAddGroup
            //
            this.btnAddGroup.Location = new System.Drawing.Point(12, 311);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(140, 25);
            this.btnAddGroup.Text = "+ Add Group (OR)";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            //
            // lblActions
            //
            this.lblActions.AutoSize = true;
            this.lblActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblActions.Location = new System.Drawing.Point(12, 348);
            this.lblActions.Name = "lblActions";
            this.lblActions.Text = "Actions";
            //
            // btnAddAction
            //
            this.btnAddAction.Location = new System.Drawing.Point(12, 366);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(100, 25);
            this.btnAddAction.Text = "+ Add Row";
            this.btnAddAction.UseVisualStyleBackColor = true;
            this.btnAddAction.Click += new System.EventHandler(this.btnAddAction_Click);
            //
            // btnRemoveAction
            //
            this.btnRemoveAction.Location = new System.Drawing.Point(118, 366);
            this.btnRemoveAction.Name = "btnRemoveAction";
            this.btnRemoveAction.Size = new System.Drawing.Size(120, 25);
            this.btnRemoveAction.Text = "− Remove Row";
            this.btnRemoveAction.UseVisualStyleBackColor = true;
            this.btnRemoveAction.Click += new System.EventHandler(this.btnRemoveAction_Click);
            //
            // btnActUp
            //
            this.btnActUp.Location = new System.Drawing.Point(248, 366);
            this.btnActUp.Name = "btnActUp";
            this.btnActUp.Size = new System.Drawing.Size(65, 25);
            this.btnActUp.Text = "▲ Up";
            this.btnActUp.UseVisualStyleBackColor = true;
            this.btnActUp.Click += new System.EventHandler(this.btnActUp_Click);
            //
            // btnActDown
            //
            this.btnActDown.Location = new System.Drawing.Point(319, 366);
            this.btnActDown.Name = "btnActDown";
            this.btnActDown.Size = new System.Drawing.Size(65, 25);
            this.btnActDown.Text = "▼ Down";
            this.btnActDown.UseVisualStyleBackColor = true;
            this.btnActDown.Click += new System.EventHandler(this.btnActDown_Click);
            //
            // dgvActions
            //
            this.dgvActions.AllowUserToAddRows = false;
            this.dgvActions.AllowUserToDeleteRows = false;
            this.dgvActions.AutoGenerateColumns = false;
            this.dgvActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActions.Location = new System.Drawing.Point(12, 397);
            this.dgvActions.Name = "dgvActions";
            this.dgvActions.RowHeadersWidth = 30;
            this.dgvActions.Size = new System.Drawing.Size(736, 180);
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(592, 590);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(673, 590);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // ScriptEditor
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(760, 630);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.lblColorHdr);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.lblFireMode);
            this.Controls.Add(this.cmbFireMode);
            this.Controls.Add(this.lblTriggers);
            this.Controls.Add(this.lblTriggersHint);
            this.Controls.Add(this.panelGroups);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.lblActions);
            this.Controls.Add(this.btnAddAction);
            this.Controls.Add(this.btnRemoveAction);
            this.Controls.Add(this.btnActUp);
            this.Controls.Add(this.btnActDown);
            this.Controls.Add(this.dgvActions);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScriptEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Script";
            this.panelGroups.ResumeLayout(false);
            this.panelGroups.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.Label lblColorHdr;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label lblFireMode;
        private System.Windows.Forms.ComboBox cmbFireMode;
        private System.Windows.Forms.Label lblTriggers;
        private System.Windows.Forms.Label lblTriggersHint;
        private System.Windows.Forms.Panel panelGroups;
        private System.Windows.Forms.FlowLayoutPanel flowGroups;
        private System.Windows.Forms.Button btnAddGroup;
        private System.Windows.Forms.Label lblActions;
        private System.Windows.Forms.Button btnAddAction;
        private System.Windows.Forms.Button btnRemoveAction;
        private System.Windows.Forms.Button btnActUp;
        private System.Windows.Forms.Button btnActDown;
        private System.Windows.Forms.DataGridView dgvActions;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
