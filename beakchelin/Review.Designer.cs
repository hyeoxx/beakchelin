namespace beakchelin
{
    partial class Review
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.NickName = new System.Windows.Forms.Label();
            this.Score = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Comment = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // NickName
            // 
            this.NickName.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.NickName.Location = new System.Drawing.Point(77, 11);
            this.NickName.Name = "NickName";
            this.NickName.Size = new System.Drawing.Size(187, 22);
            this.NickName.TabIndex = 16;
            this.NickName.Text = "닉네임";
            // 
            // Score
            // 
            this.Score.AutoSize = true;
            this.Score.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Score.Location = new System.Drawing.Point(79, 33);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(39, 14);
            this.Score.TabIndex = 17;
            this.Score.Text = "별점 : ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Silver;
            this.label15.Location = new System.Drawing.Point(7, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(257, 12);
            this.label15.TabIndex = 19;
            this.label15.Text = "─────────────────────";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::beakchelin.Properties.Resources.user;
            this.pictureBox1.Location = new System.Drawing.Point(7, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // Comment
            // 
            this.Comment.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Comment.Location = new System.Drawing.Point(79, 53);
            this.Comment.Name = "Comment";
            this.Comment.Size = new System.Drawing.Size(185, 67);
            this.Comment.TabIndex = 21;
            this.Comment.Text = "내용";
            // 
            // Review
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.Comment);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.NickName);
            this.Name = "Review";
            this.Size = new System.Drawing.Size(271, 131);
            this.Load += new System.EventHandler(this.addReview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label NickName;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label Comment;
    }
}
