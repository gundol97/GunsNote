using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note
{
    public partial class RenameNoteName : Form
    {
        internal string Rename = "";
        private bool IsKorean = true;
        public RenameNoteName()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 번역
        /// </summary>
        /// <param name="IsKorean"></param>
        public RenameNoteName(bool IsKorean)
        {
            InitializeComponent();
            if (IsKorean)
            {
                BT_Apply.Text = ko.Apply;
                BT_Cancel.Text = ko.Cancel;
            }
            else
            {
                BT_Apply.Text = en.Apply;
                BT_Cancel.Text = en.Cancel;
            }
        }

        /// <summary>
        /// 적용 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Apply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TB_Rename.Text))
            {
                string msg = "";
                if (IsKorean) msg = ko.PleaseEnterAvalue_;
                else msg = en.PleaseEnterAvalue_;
                MessageBox.Show(msg);
            }
            else
            {
                Rename = TB_Rename.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// 취소 버트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
