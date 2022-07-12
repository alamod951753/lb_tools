using System.Drawing;
using ToolsDev.LBTW.CardTools.Components;

namespace ToolsDev.LBTW.CardTools.Forms
{
    partial class LBTWCardTools
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LBTWCardTools));
            this.lbProcessDate = new System.Windows.Forms.Label();
            this.tbProcessDate = new System.Windows.Forms.TextBox();
            this.tbClearDate = new System.Windows.Forms.TextBox();
            this.lbClearDate = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox_Env = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnChooseDate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSeq = new System.Windows.Forms.TextBox();
            this.lbSeq = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseLogFile = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbOutput_Tab3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_stg = new System.Windows.Forms.RadioButton();
            this.radioButton_uat = new System.Windows.Forms.RadioButton();
            this.btnImportAuth = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tbBatchSize_tab3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox_Env.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProcessDate
            // 
            this.lbProcessDate.AutoSize = true;
            this.lbProcessDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProcessDate.Location = new System.Drawing.Point(13, 17);
            this.lbProcessDate.Name = "lbProcessDate";
            this.lbProcessDate.Size = new System.Drawing.Size(127, 16);
            this.lbProcessDate.TabIndex = 50;
            this.lbProcessDate.Text = "處理日 (yyMMdd)";
            // 
            // tbProcessDate
            // 
            this.tbProcessDate.AcceptsTab = true;
            this.tbProcessDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProcessDate.Location = new System.Drawing.Point(146, 14);
            this.tbProcessDate.MaxLength = 6;
            this.tbProcessDate.Name = "tbProcessDate";
            this.tbProcessDate.Size = new System.Drawing.Size(113, 23);
            this.tbProcessDate.TabIndex = 1;
            // 
            // tbClearDate
            // 
            this.tbClearDate.AcceptsTab = true;
            this.tbClearDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbClearDate.Location = new System.Drawing.Point(146, 46);
            this.tbClearDate.MaxLength = 6;
            this.tbClearDate.Name = "tbClearDate";
            this.tbClearDate.Size = new System.Drawing.Size(113, 23);
            this.tbClearDate.TabIndex = 2;
            // 
            // lbClearDate
            // 
            this.lbClearDate.AutoSize = true;
            this.lbClearDate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClearDate.Location = new System.Drawing.Point(13, 49);
            this.lbClearDate.Name = "lbClearDate";
            this.lbClearDate.Size = new System.Drawing.Size(127, 16);
            this.lbClearDate.TabIndex = 51;
            this.lbClearDate.Text = "清算日 (yyMMdd)";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Title = "File";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseFile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseFile.Location = new System.Drawing.Point(278, 53);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(128, 24);
            this.btnBrowseFile.TabIndex = 4;
            this.btnBrowseFile.Text = "Browse";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.BtnBrowseFile_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.AllowDrop = true;
            this.tbOutput.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tbOutput.Location = new System.Drawing.Point(16, 361);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(520, 87);
            this.tbOutput.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 53;
            this.label1.Text = "Output:";
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(76, 114);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(128, 24);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::ToolsDev.LBTW.CardTools.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(201, 366);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 54;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 484);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox_Env);
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.tbSeq);
            this.tabPage1.Controls.Add(this.lbSeq);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnBrowseFile);
            this.tabPage1.Controls.Add(this.lbClearDate);
            this.tabPage1.Controls.Add(this.tbClearDate);
            this.tabPage1.Controls.Add(this.lbProcessDate);
            this.tabPage1.Controls.Add(this.tbProcessDate);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.tbOutput);
            this.tabPage1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(552, 458);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "NCCC 帳單產生器";
            // 
            // groupBox_Env
            // 
            this.groupBox_Env.Controls.Add(this.radioButton1);
            this.groupBox_Env.Controls.Add(this.radioButton2);
            this.groupBox_Env.Location = new System.Drawing.Point(278, 114);
            this.groupBox_Env.Name = "groupBox_Env";
            this.groupBox_Env.Size = new System.Drawing.Size(159, 41);
            this.groupBox_Env.TabIndex = 62;
            this.groupBox_Env.TabStop = false;
            this.groupBox_Env.Text = "Environment";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(92, 17);
            this.radioButton1.TabIndex = 60;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Local (STG)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(102, 18);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 17);
            this.radioButton2.TabIndex = 61;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "UAT";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btnChooseDate);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(278, 161);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(134, 30);
            this.flowLayoutPanel1.TabIndex = 58;
            // 
            // btnChooseDate
            // 
            this.btnChooseDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChooseDate.Location = new System.Drawing.Point(3, 3);
            this.btnChooseDate.Name = "btnChooseDate";
            this.btnChooseDate.Size = new System.Drawing.Size(128, 24);
            this.btnChooseDate.TabIndex = 59;
            this.btnChooseDate.Text = "Submit";
            this.btnChooseDate.UseVisualStyleBackColor = true;
            this.btnChooseDate.Click += new System.EventHandler(this.BtnChooseDate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(275, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 28);
            this.label4.TabIndex = 57;
            this.label4.Text = "Or\r\n(2) Choose DB table AuthHist Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(275, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 14);
            this.label3.TabIndex = 56;
            this.label3.Text = "(1) Upload Excel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(275, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 55;
            this.label2.Text = "Data Source";
            // 
            // tbSeq
            // 
            this.tbSeq.AcceptsTab = true;
            this.tbSeq.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSeq.Location = new System.Drawing.Point(146, 78);
            this.tbSeq.MaxLength = 2;
            this.tbSeq.Name = "tbSeq";
            this.tbSeq.Size = new System.Drawing.Size(113, 23);
            this.tbSeq.TabIndex = 3;
            // 
            // lbSeq
            // 
            this.lbSeq.AutoSize = true;
            this.lbSeq.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSeq.Location = new System.Drawing.Point(13, 81);
            this.lbSeq.Name = "lbSeq";
            this.lbSeq.Size = new System.Drawing.Size(128, 16);
            this.lbSeq.TabIndex = 52;
            this.lbSeq.Text = "檔案序號 (01-99)";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.btnBrowseLogFile);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(552, 458);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Upload Log File";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 16);
            this.label6.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(309, 16);
            this.label5.TabIndex = 51;
            this.label5.Text = "Upload Log File To DB ( CARD_HIST.LogData )";
            // 
            // btnBrowseLogFile
            // 
            this.btnBrowseLogFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseLogFile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowseLogFile.Location = new System.Drawing.Point(16, 39);
            this.btnBrowseLogFile.Name = "btnBrowseLogFile";
            this.btnBrowseLogFile.Size = new System.Drawing.Size(128, 24);
            this.btnBrowseLogFile.TabIndex = 5;
            this.btnBrowseLogFile.Text = "Browse";
            this.btnBrowseLogFile.UseVisualStyleBackColor = true;
            this.btnBrowseLogFile.Click += new System.EventHandler(this.BtnBrowseLogFile_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.tbBatchSize_tab3);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.tbOutput_Tab3);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.btnImportAuth);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(552, 458);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Import Auth Today";
            // 
            // tbOutput_Tab3
            // 
            this.tbOutput_Tab3.AllowDrop = true;
            this.tbOutput_Tab3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput_Tab3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tbOutput_Tab3.Location = new System.Drawing.Point(16, 150);
            this.tbOutput_Tab3.Multiline = true;
            this.tbOutput_Tab3.Name = "tbOutput_Tab3";
            this.tbOutput_Tab3.ReadOnly = true;
            this.tbOutput_Tab3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput_Tab3.Size = new System.Drawing.Size(518, 294);
            this.tbOutput_Tab3.TabIndex = 69;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 129);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 14);
            this.label7.TabIndex = 68;
            this.label7.Text = "Output:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_stg);
            this.groupBox1.Controls.Add(this.radioButton_uat);
            this.groupBox1.Location = new System.Drawing.Point(16, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 41);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Environment";
            // 
            // radioButton_stg
            // 
            this.radioButton_stg.AutoSize = true;
            this.radioButton_stg.Location = new System.Drawing.Point(7, 18);
            this.radioButton_stg.Name = "radioButton_stg";
            this.radioButton_stg.Size = new System.Drawing.Size(92, 17);
            this.radioButton_stg.TabIndex = 60;
            this.radioButton_stg.TabStop = true;
            this.radioButton_stg.Text = "Local (STG)";
            this.radioButton_stg.UseVisualStyleBackColor = true;
            // 
            // radioButton_uat
            // 
            this.radioButton_uat.AutoSize = true;
            this.radioButton_uat.Location = new System.Drawing.Point(119, 18);
            this.radioButton_uat.Name = "radioButton_uat";
            this.radioButton_uat.Size = new System.Drawing.Size(47, 17);
            this.radioButton_uat.TabIndex = 61;
            this.radioButton_uat.TabStop = true;
            this.radioButton_uat.Text = "UAT";
            this.radioButton_uat.UseVisualStyleBackColor = true;
            // 
            // btnImportAuth
            // 
            this.btnImportAuth.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportAuth.Location = new System.Drawing.Point(16, 90);
            this.btnImportAuth.Name = "btnImportAuth";
            this.btnImportAuth.Size = new System.Drawing.Size(150, 23);
            this.btnImportAuth.TabIndex = 66;
            this.btnImportAuth.Text = "Import Auth Data";
            this.btnImportAuth.UseVisualStyleBackColor = true;
            this.btnImportAuth.Click += new System.EventHandler(this.BtnImportAuth_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 14);
            this.label8.TabIndex = 70;
            this.label8.Text = "Batch Size";
            // 
            // tbBatchSize_tab3
            // 
            this.tbBatchSize_tab3.AcceptsTab = true;
            this.tbBatchSize_tab3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBatchSize_tab3.Location = new System.Drawing.Point(94, 13);
            this.tbBatchSize_tab3.MaxLength = 6;
            this.tbBatchSize_tab3.Name = "tbBatchSize_tab3";
            this.tbBatchSize_tab3.Size = new System.Drawing.Size(113, 23);
            this.tbBatchSize_tab3.TabIndex = 71;
            this.tbBatchSize_tab3.Text = "1000";
            // 
            // LBTWCardTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 506);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LBTWCardTools";
            this.Text = "LBTW Card Tools";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox_Env.ResumeLayout(false);
            this.groupBox_Env.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label lbProcessDate;
        private System.Windows.Forms.TextBox tbProcessDate;
        private System.Windows.Forms.TextBox tbClearDate;
        private System.Windows.Forms.Label lbClearDate;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lbSeq;
        private System.Windows.Forms.TextBox tbSeq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnChooseDate;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnBrowseLogFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox_Env;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbOutput_Tab3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_stg;
        private System.Windows.Forms.RadioButton radioButton_uat;
        private System.Windows.Forms.Button btnImportAuth;
        private System.Windows.Forms.TextBox tbBatchSize_tab3;
        private System.Windows.Forms.Label label8;
    }
}