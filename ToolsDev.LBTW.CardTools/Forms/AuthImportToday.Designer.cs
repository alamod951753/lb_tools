
namespace ToolsDev.LBTW.CardTools.Forms
{
    partial class AuthImportToday
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox_Env = new System.Windows.Forms.GroupBox();
            this.radioButton_stg = new System.Windows.Forms.RadioButton();
            this.radioButton_uat = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.groupBox_Env.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(14, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Import Auth Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // groupBox_Env
            // 
            this.groupBox_Env.Controls.Add(this.radioButton_stg);
            this.groupBox_Env.Controls.Add(this.radioButton_uat);
            this.groupBox_Env.Location = new System.Drawing.Point(14, 12);
            this.groupBox_Env.Name = "groupBox_Env";
            this.groupBox_Env.Size = new System.Drawing.Size(185, 41);
            this.groupBox_Env.TabIndex = 63;
            this.groupBox_Env.TabStop = false;
            this.groupBox_Env.Text = "Environment";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 64;
            this.label1.Text = "Output:";
            // 
            // tbOutput
            // 
            this.tbOutput.AllowDrop = true;
            this.tbOutput.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.tbOutput.Location = new System.Drawing.Point(12, 138);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ReadOnly = true;
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(430, 141);
            this.tbOutput.TabIndex = 65;
            // 
            // AuthImportToday
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 291);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox_Env);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.Name = "AuthImportToday";
            this.Text = "AuthImportToday";
            this.groupBox_Env.ResumeLayout(false);
            this.groupBox_Env.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox_Env;
        private System.Windows.Forms.RadioButton radioButton_stg;
        private System.Windows.Forms.RadioButton radioButton_uat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOutput;
    }
}