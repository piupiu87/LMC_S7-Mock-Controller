namespace AdlinkMockController
{
    partial class Main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageIOControl2 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblScriptStatus = new System.Windows.Forms.Label();
            this.btnLoadScripts = new System.Windows.Forms.Button();
            this.btnSaveScripts = new System.Windows.Forms.Button();
            this.btnDeleteScript = new System.Windows.Forms.Button();
            this.btnEditScript = new System.Windows.Forms.Button();
            this.dataGridViewScripts = new System.Windows.Forms.DataGridView();
            this.btnNewScript = new System.Windows.Forms.Button();
            this.tabPageMotion = new System.Windows.Forms.TabPage();
            this.grpAxis = new System.Windows.Forms.GroupBox();
            this.btnServo = new System.Windows.Forms.Button();
            this.btnResetErr = new System.Windows.Forms.Button();
            this.lblPosHdr = new System.Windows.Forms.Label();
            this.lblPosVal = new System.Windows.Forms.Label();
            this.lblCmdHdr = new System.Windows.Forms.Label();
            this.lblCmdVal = new System.Windows.Forms.Label();
            this.pnlMotion = new System.Windows.Forms.Panel();
            this.tblMotionAxes = new System.Windows.Forms.TableLayoutPanel();
            this.tmrUpdateStatus = new System.Windows.Forms.Timer(this.components);
            this.lblJsonFilePath = new System.Windows.Forms.Label();
            this.btnSetJsonFilePath = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageIOControl2.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScripts)).BeginInit();
            this.tabPageMotion.SuspendLayout();
            this.grpAxis.SuspendLayout();
            this.pnlMotion.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageIOControl2);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPageMotion);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1276, 798);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageIOControl2
            // 
            this.tabPageIOControl2.AutoScroll = true;
            this.tabPageIOControl2.Controls.Add(this.tabControl3);
            this.tabPageIOControl2.Controls.Add(this.tabControl2);
            this.tabPageIOControl2.Location = new System.Drawing.Point(4, 22);
            this.tabPageIOControl2.Name = "tabPageIOControl2";
            this.tabPageIOControl2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIOControl2.Size = new System.Drawing.Size(1268, 772);
            this.tabPageIOControl2.TabIndex = 0;
            this.tabPageIOControl2.Text = "IO Control";
            this.tabPageIOControl2.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPageInput);
            this.tabControl3.Controls.Add(this.tabPageOutput);
            this.tabControl3.Location = new System.Drawing.Point(6, 6);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1259, 760);
            this.tabControl3.TabIndex = 3;
            // 
            // tabPageInput
            // 
            this.tabPageInput.Controls.Add(this.button1);
            this.tabPageInput.Controls.Add(this.label1);
            this.tabPageInput.Location = new System.Drawing.Point(4, 22);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInput.Size = new System.Drawing.Size(1251, 734);
            this.tabPageInput.TabIndex = 0;
            this.tabPageInput.Text = "Input";
            this.tabPageInput.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(64, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(1251, 734);
            this.tabPageOutput.TabIndex = 1;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(137, 149);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(8, 8);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(0, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(0, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblScriptStatus);
            this.tabPage2.Controls.Add(this.btnLoadScripts);
            this.tabPage2.Controls.Add(this.btnSaveScripts);
            this.tabPage2.Controls.Add(this.btnDeleteScript);
            this.tabPage2.Controls.Add(this.btnEditScript);
            this.tabPage2.Controls.Add(this.dataGridViewScripts);
            this.tabPage2.Controls.Add(this.btnNewScript);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1268, 772);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Scripts";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblScriptStatus
            // 
            this.lblScriptStatus.AutoSize = true;
            this.lblScriptStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblScriptStatus.Location = new System.Drawing.Point(510, 14);
            this.lblScriptStatus.Name = "lblScriptStatus";
            this.lblScriptStatus.Size = new System.Drawing.Size(0, 13);
            this.lblScriptStatus.TabIndex = 10;
            // 
            // btnLoadScripts
            // 
            this.btnLoadScripts.Location = new System.Drawing.Point(396, 6);
            this.btnLoadScripts.Name = "btnLoadScripts";
            this.btnLoadScripts.Size = new System.Drawing.Size(100, 31);
            this.btnLoadScripts.TabIndex = 9;
            this.btnLoadScripts.Text = "Load Scripts";
            this.btnLoadScripts.UseVisualStyleBackColor = true;
            this.btnLoadScripts.Click += new System.EventHandler(this.btnLoadScripts_Click);
            // 
            // btnSaveScripts
            // 
            this.btnSaveScripts.Location = new System.Drawing.Point(290, 6);
            this.btnSaveScripts.Name = "btnSaveScripts";
            this.btnSaveScripts.Size = new System.Drawing.Size(100, 31);
            this.btnSaveScripts.TabIndex = 8;
            this.btnSaveScripts.Text = "Save Scripts";
            this.btnSaveScripts.UseVisualStyleBackColor = true;
            this.btnSaveScripts.Click += new System.EventHandler(this.btnSaveScripts_Click);
            // 
            // btnDeleteScript
            // 
            this.btnDeleteScript.Location = new System.Drawing.Point(180, 6);
            this.btnDeleteScript.Name = "btnDeleteScript";
            this.btnDeleteScript.Size = new System.Drawing.Size(81, 31);
            this.btnDeleteScript.TabIndex = 7;
            this.btnDeleteScript.Text = "Delete";
            this.btnDeleteScript.UseVisualStyleBackColor = true;
            this.btnDeleteScript.Click += new System.EventHandler(this.btnDeleteScript_Click);
            // 
            // btnEditScript
            // 
            this.btnEditScript.Location = new System.Drawing.Point(93, 6);
            this.btnEditScript.Name = "btnEditScript";
            this.btnEditScript.Size = new System.Drawing.Size(81, 31);
            this.btnEditScript.TabIndex = 6;
            this.btnEditScript.Text = "Edit";
            this.btnEditScript.UseVisualStyleBackColor = true;
            this.btnEditScript.Click += new System.EventHandler(this.btnEditScript_Click);
            // 
            // dataGridViewScripts
            // 
            this.dataGridViewScripts.AllowUserToAddRows = false;
            this.dataGridViewScripts.AllowUserToDeleteRows = false;
            this.dataGridViewScripts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewScripts.Location = new System.Drawing.Point(6, 43);
            this.dataGridViewScripts.MultiSelect = false;
            this.dataGridViewScripts.Name = "dataGridViewScripts";
            this.dataGridViewScripts.RowHeadersWidth = 30;
            this.dataGridViewScripts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewScripts.Size = new System.Drawing.Size(1255, 400);
            this.dataGridViewScripts.TabIndex = 11;
            this.dataGridViewScripts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewScripts_CellClick);
            this.dataGridViewScripts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewScripts_CellDoubleClick);
            // 
            // btnNewScript
            // 
            this.btnNewScript.Location = new System.Drawing.Point(6, 6);
            this.btnNewScript.Name = "btnNewScript";
            this.btnNewScript.Size = new System.Drawing.Size(81, 31);
            this.btnNewScript.TabIndex = 1;
            this.btnNewScript.Text = "New Script";
            this.btnNewScript.UseVisualStyleBackColor = true;
            this.btnNewScript.Click += new System.EventHandler(this.btnNewScript_Click);
            // 
            // tabPageMotion
            // 
            this.tabPageMotion.Controls.Add(this.grpAxis);
            this.tabPageMotion.Controls.Add(this.pnlMotion);
            this.tabPageMotion.Location = new System.Drawing.Point(4, 22);
            this.tabPageMotion.Name = "tabPageMotion";
            this.tabPageMotion.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotion.Size = new System.Drawing.Size(1268, 772);
            this.tabPageMotion.TabIndex = 3;
            this.tabPageMotion.Text = "Motion Control";
            this.tabPageMotion.UseVisualStyleBackColor = true;
            // 
            // grpAxis
            // 
            this.grpAxis.Controls.Add(this.btnServo);
            this.grpAxis.Controls.Add(this.btnResetErr);
            this.grpAxis.Controls.Add(this.lblPosHdr);
            this.grpAxis.Controls.Add(this.lblPosVal);
            this.grpAxis.Controls.Add(this.lblCmdHdr);
            this.grpAxis.Controls.Add(this.lblCmdVal);
            this.grpAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpAxis.Location = new System.Drawing.Point(6, 6);
            this.grpAxis.Name = "grpAxis";
            this.grpAxis.Size = new System.Drawing.Size(300, 90);
            this.grpAxis.TabIndex = 0;
            this.grpAxis.TabStop = false;
            this.grpAxis.Text = "Axis";
            this.grpAxis.Visible = false;
            // 
            // btnServo
            // 
            this.btnServo.BackColor = System.Drawing.Color.Red;
            this.btnServo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnServo.Location = new System.Drawing.Point(8, 22);
            this.btnServo.Name = "btnServo";
            this.btnServo.Size = new System.Drawing.Size(110, 28);
            this.btnServo.TabIndex = 0;
            this.btnServo.Text = "SERVO OFF";
            this.btnServo.UseVisualStyleBackColor = false;
            // 
            // btnResetErr
            // 
            this.btnResetErr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnResetErr.Location = new System.Drawing.Point(128, 22);
            this.btnResetErr.Name = "btnResetErr";
            this.btnResetErr.Size = new System.Drawing.Size(110, 28);
            this.btnResetErr.TabIndex = 1;
            this.btnResetErr.Text = "Reset Err";
            this.btnResetErr.UseVisualStyleBackColor = true;
            // 
            // lblPosHdr
            // 
            this.lblPosHdr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblPosHdr.Location = new System.Drawing.Point(8, 60);
            this.lblPosHdr.Name = "lblPosHdr";
            this.lblPosHdr.Size = new System.Drawing.Size(30, 16);
            this.lblPosHdr.TabIndex = 2;
            this.lblPosHdr.Text = "Pos:";
            // 
            // lblPosVal
            // 
            this.lblPosVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblPosVal.Location = new System.Drawing.Point(40, 60);
            this.lblPosVal.Name = "lblPosVal";
            this.lblPosVal.Size = new System.Drawing.Size(100, 16);
            this.lblPosVal.TabIndex = 3;
            this.lblPosVal.Text = "0";
            // 
            // lblCmdHdr
            // 
            this.lblCmdHdr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCmdHdr.Location = new System.Drawing.Point(148, 60);
            this.lblCmdHdr.Name = "lblCmdHdr";
            this.lblCmdHdr.Size = new System.Drawing.Size(35, 16);
            this.lblCmdHdr.TabIndex = 4;
            this.lblCmdHdr.Text = "Cmd:";
            // 
            // lblCmdVal
            // 
            this.lblCmdVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCmdVal.Location = new System.Drawing.Point(185, 60);
            this.lblCmdVal.Name = "lblCmdVal";
            this.lblCmdVal.Size = new System.Drawing.Size(100, 16);
            this.lblCmdVal.TabIndex = 5;
            this.lblCmdVal.Text = "0";
            // 
            // pnlMotion
            // 
            this.pnlMotion.AutoScroll = true;
            this.pnlMotion.Controls.Add(this.tblMotionAxes);
            this.pnlMotion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMotion.Location = new System.Drawing.Point(3, 3);
            this.pnlMotion.Name = "pnlMotion";
            this.pnlMotion.Size = new System.Drawing.Size(1262, 766);
            this.pnlMotion.TabIndex = 0;
            // 
            // tblMotionAxes
            // 
            this.tblMotionAxes.AutoSize = true;
            this.tblMotionAxes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblMotionAxes.ColumnCount = 2;
            this.tblMotionAxes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMotionAxes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMotionAxes.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblMotionAxes.Location = new System.Drawing.Point(0, 0);
            this.tblMotionAxes.Name = "tblMotionAxes";
            this.tblMotionAxes.Padding = new System.Windows.Forms.Padding(4);
            this.tblMotionAxes.RowCount = 1;
            this.tblMotionAxes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMotionAxes.Size = new System.Drawing.Size(1262, 28);
            this.tblMotionAxes.TabIndex = 0;
            // 
            // tmrUpdateStatus
            // 
            this.tmrUpdateStatus.Tick += new System.EventHandler(this.tmrUpdateStatus_Tick);
            // 
            // lblJsonFilePath
            // 
            this.lblJsonFilePath.Location = new System.Drawing.Point(622, 9);
            this.lblJsonFilePath.Name = "lblJsonFilePath";
            this.lblJsonFilePath.Size = new System.Drawing.Size(655, 20);
            this.lblJsonFilePath.TabIndex = 4;
            this.lblJsonFilePath.Text = "label2";
            // 
            // btnSetJsonFilePath
            // 
            this.btnSetJsonFilePath.Location = new System.Drawing.Point(456, 2);
            this.btnSetJsonFilePath.Name = "btnSetJsonFilePath";
            this.btnSetJsonFilePath.Size = new System.Drawing.Size(162, 27);
            this.btnSetJsonFilePath.TabIndex = 5;
            this.btnSetJsonFilePath.Text = "Set Mock State Json File Path";
            this.btnSetJsonFilePath.UseVisualStyleBackColor = true;
            this.btnSetJsonFilePath.Click += new System.EventHandler(this.btnSetJsonFilePath_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 815);
            this.Controls.Add(this.lblJsonFilePath);
            this.Controls.Add(this.btnSetJsonFilePath);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Adlink Mock Controller";
            this.tabControl1.ResumeLayout(false);
            this.tabPageIOControl2.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewScripts)).EndInit();
            this.tabPageMotion.ResumeLayout(false);
            this.grpAxis.ResumeLayout(false);
            this.pnlMotion.ResumeLayout(false);
            this.pnlMotion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageIOControl2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPageInput;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.Timer tmrUpdateStatus;
        private System.Windows.Forms.Label lblJsonFilePath;
        private System.Windows.Forms.Button btnSetJsonFilePath;
        private System.Windows.Forms.Button btnNewScript;
        private System.Windows.Forms.Button btnEditScript;
        private System.Windows.Forms.Button btnDeleteScript;
        private System.Windows.Forms.Button btnSaveScripts;
        private System.Windows.Forms.Button btnLoadScripts;
        private System.Windows.Forms.DataGridView dataGridViewScripts;
        private System.Windows.Forms.Label lblScriptStatus;
        // Motion tab
        private System.Windows.Forms.TabPage tabPageMotion;
        // Axis template
        private System.Windows.Forms.GroupBox grpAxis;
        private System.Windows.Forms.Button btnServo;
        private System.Windows.Forms.Button btnResetErr;
        private System.Windows.Forms.Label lblPosHdr;
        private System.Windows.Forms.Label lblPosVal;
        private System.Windows.Forms.Label lblCmdHdr;
        private System.Windows.Forms.Label lblCmdVal;
        private System.Windows.Forms.Panel pnlMotion;
        private System.Windows.Forms.TableLayoutPanel tblMotionAxes;
    }
}
