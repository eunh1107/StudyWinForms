namespace ApplicationDev_Do
{
    partial class FM_PassWord
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPerPw = new System.Windows.Forms.TextBox();
            this.txtChPw = new System.Windows.Forms.TextBox();
            this.btnChPw = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "사용자 ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "이전 PW";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "변경 PW";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(126, 36);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(330, 25);
            this.txtUserID.TabIndex = 3;
            // 
            // txtPerPw
            // 
            this.txtPerPw.Location = new System.Drawing.Point(126, 77);
            this.txtPerPw.Name = "txtPerPw";
            this.txtPerPw.Size = new System.Drawing.Size(330, 25);
            this.txtPerPw.TabIndex = 4;
            // 
            // txtChPw
            // 
            this.txtChPw.Location = new System.Drawing.Point(126, 119);
            this.txtChPw.Name = "txtChPw";
            this.txtChPw.Size = new System.Drawing.Size(330, 25);
            this.txtChPw.TabIndex = 5;
            // 
            // btnChPw
            // 
            this.btnChPw.Location = new System.Drawing.Point(228, 166);
            this.btnChPw.Name = "btnChPw";
            this.btnChPw.Size = new System.Drawing.Size(108, 47);
            this.btnChPw.TabIndex = 6;
            this.btnChPw.Text = "변경등록";
            this.btnChPw.UseVisualStyleBackColor = true;
            this.btnChPw.Click += new System.EventHandler(this.btnChPw_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(348, 166);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 47);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FM_PassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 228);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnChPw);
            this.Controls.Add(this.txtChPw);
            this.Controls.Add(this.txtPerPw);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FM_PassWord";
            this.Text = "비밀번호 변경";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPerPw;
        private System.Windows.Forms.TextBox txtChPw;
        private System.Windows.Forms.Button btnChPw;
        private System.Windows.Forms.Button btnClose;
    }
}