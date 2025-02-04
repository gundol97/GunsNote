namespace Note
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.불러오기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.저장ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.다른이름으로저ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.koreanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TC = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.languToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.불러오기ToolStripMenuItem,
            this.저장ToolStripMenuItem,
            this.다른이름으로저ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem.Text = "파일";
            // 
            // 불러오기ToolStripMenuItem
            // 
            this.불러오기ToolStripMenuItem.Name = "불러오기ToolStripMenuItem";
            this.불러오기ToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.불러오기ToolStripMenuItem.Text = "열기 (Ctrl + O)";
            this.불러오기ToolStripMenuItem.Click += new System.EventHandler(this.불러오기ToolStripMenuItem_Click);
            // 
            // 저장ToolStripMenuItem
            // 
            this.저장ToolStripMenuItem.Name = "저장ToolStripMenuItem";
            this.저장ToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.저장ToolStripMenuItem.Text = "저장 (Ctrl + S)";
            this.저장ToolStripMenuItem.Click += new System.EventHandler(this.저장ToolStripMenuItem_Click);
            // 
            // 다른이름으로저ToolStripMenuItem
            // 
            this.다른이름으로저ToolStripMenuItem.Name = "다른이름으로저ToolStripMenuItem";
            this.다른이름으로저ToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.다른이름으로저ToolStripMenuItem.Text = "다른 이름으로 저장 (Ctrl + Shift + S)";
            this.다른이름으로저ToolStripMenuItem.Click += new System.EventHandler(this.다른이름으로저ToolStripMenuItem_Click);
            // 
            // languToolStripMenuItem
            // 
            this.languToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.koreanToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.languToolStripMenuItem.Name = "languToolStripMenuItem";
            this.languToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.languToolStripMenuItem.Text = "Language";
            // 
            // koreanToolStripMenuItem
            // 
            this.koreanToolStripMenuItem.Name = "koreanToolStripMenuItem";
            this.koreanToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.koreanToolStripMenuItem.Text = "Korean";
            this.koreanToolStripMenuItem.Click += new System.EventHandler(this.koreanToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // TC
            // 
            this.TC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TC.Location = new System.Drawing.Point(0, 27);
            this.TC.Name = "TC";
            this.TC.SelectedIndex = 0;
            this.TC.Size = new System.Drawing.Size(800, 422);
            this.TC.TabIndex = 2;
            this.TC.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TC_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TC);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "GunsNote";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem languToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem koreanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.TabControl TC;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 불러오기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 저장ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 다른이름으로저ToolStripMenuItem;
    }
}

