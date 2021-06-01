namespace ApplicationDev
{
    partial class M1010_PasswordChang
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param> 
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtPerPW = new System.Windows.Forms.TextBox();
            this.btnChangPw = new System.Windows.Forms.Button();
            this.txtNewPw = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "사용자 ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "기존 P/W";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(115, 35);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(274, 25);
            this.txtUserId.TabIndex = 2;
            this.txtUserId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserId_KeyDown);
            // 
            // txtPerPW
            // 
            this.txtPerPW.Location = new System.Drawing.Point(115, 79);
            this.txtPerPW.Name = "txtPerPW";
            this.txtPerPW.PasswordChar = '*';
            this.txtPerPW.Size = new System.Drawing.Size(274, 25);
            this.txtPerPW.TabIndex = 3;
            this.txtPerPW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserId_KeyDown);
            // 
            // btnChangPw
            // 
            this.btnChangPw.Location = new System.Drawing.Point(263, 158);
            this.btnChangPw.Name = "btnChangPw";
            this.btnChangPw.Size = new System.Drawing.Size(126, 56);
            this.btnChangPw.TabIndex = 4;
            this.btnChangPw.Text = "비밀번호 변경";
            this.btnChangPw.UseVisualStyleBackColor = true;
            this.btnChangPw.Click += new System.EventHandler(this.btnChangPw_Click);
            // 
            // txtNewPw
            // 
            this.txtNewPw.Location = new System.Drawing.Point(115, 117);
            this.txtNewPw.Name = "txtNewPw";
            this.txtNewPw.PasswordChar = '*';
            this.txtNewPw.Size = new System.Drawing.Size(274, 25);
            this.txtNewPw.TabIndex = 6;
            this.txtNewPw.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserId_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "변경 P/W";
            // 
            // M1010_PasswordChang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 226);
            this.Controls.Add(this.txtNewPw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnChangPw);
            this.Controls.Add(this.txtPerPW);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "M1010_PasswordChang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PASSWORD CHANG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtPerPW;
        private System.Windows.Forms.Button btnChangPw;
        private System.Windows.Forms.TextBox txtNewPw;
        private System.Windows.Forms.Label label3;
    }
}

