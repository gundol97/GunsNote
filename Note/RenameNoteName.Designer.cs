namespace Note
{
    partial class RenameNoteName
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
            this.TB_Rename = new System.Windows.Forms.TextBox();
            this.BT_Apply = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TB_Rename
            // 
            this.TB_Rename.Location = new System.Drawing.Point(12, 53);
            this.TB_Rename.Name = "TB_Rename";
            this.TB_Rename.Size = new System.Drawing.Size(402, 21);
            this.TB_Rename.TabIndex = 0;
            // 
            // BT_Apply
            // 
            this.BT_Apply.Location = new System.Drawing.Point(12, 99);
            this.BT_Apply.Name = "BT_Apply";
            this.BT_Apply.Size = new System.Drawing.Size(182, 37);
            this.BT_Apply.TabIndex = 1;
            this.BT_Apply.Text = "Apply";
            this.BT_Apply.UseVisualStyleBackColor = true;
            this.BT_Apply.Click += new System.EventHandler(this.BT_Apply_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Location = new System.Drawing.Point(232, 99);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(182, 37);
            this.BT_Cancel.TabIndex = 2;
            this.BT_Cancel.Text = "Cancel";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // RenameNoteName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 148);
            this.ControlBox = false;
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_Apply);
            this.Controls.Add(this.TB_Rename);
            this.Name = "RenameNoteName";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_Rename;
        private System.Windows.Forms.Button BT_Apply;
        private System.Windows.Forms.Button BT_Cancel;
    }
}