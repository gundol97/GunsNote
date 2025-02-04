using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note
{
    internal class CustomRichTextBox : RichTextBox
    {
        private ContextMenuStrip contextMenuStrip; 
        public CustomRichTextBox()
        {
            this.Dock = DockStyle.Fill;
            this.KeyDown += CustomRichTextBox_KeyDown;
            InitializeContextMenu();
        }
        #region context 메뉴
        /// <summary>
        /// 우클릭시 나타나는 context 메뉴 초기화
        /// </summary>
        private void InitializeContextMenu()
        {
            contextMenuStrip = new ContextMenuStrip();

            // 메뉴 항목 추가
            contextMenuStrip.Items.Add($"{ko.Bold} (Ctrl + B)", null, (s, e) => ToggleBold());
            contextMenuStrip.Items.Add($"{ko.ChangeColor} (Ctrl + Shift + C)", null, (s, e) => ChangeSelectedTextColor());
            contextMenuStrip.Items.Add($"{ko.ChangeBackGroundColor} (Ctrl + Shift + H)", null, (s, e) => ChangeSelectedTextColor());
            contextMenuStrip.Items.Add($"{ko.UnderLine} (Ctrl + U)", null, (s, e) => ToggleUnderline());
            contextMenuStrip.Items.Add($"{ko.Italic} (Ctrl + L)", null, (s, e) => ToggleItalic());
            contextMenuStrip.Items.Add($"{ko.IncreaseFontSize} (Ctrl + Shift + ↑)", null, (s, e) => IncreaseFontSize());
            contextMenuStrip.Items.Add($"{ko.DecreaseFontSize} (Ctrl + Shift + ↓)", null, (s, e) => DecreaseFontSize());

            // ContextMenuStrip 설정
            this.ContextMenuStrip = contextMenuStrip;
        }
        /// <summary>
        /// 선택된 문자열이 없다면 context 메뉴 숨기기
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // 우클릭인지 확인
            if (e.Button == MouseButtons.Right)
            {
                // 선택된 텍스트가 없는 경우 컨텍스트 메뉴를 숨김
                if (string.IsNullOrEmpty(this.SelectedText))
                {
                    this.ContextMenuStrip = null; // 컨텍스트 메뉴를 해제
                }
                else
                {
                    this.ContextMenuStrip = contextMenuStrip; // 컨텍스트 메뉴를 활성화
                }
            }
        }
        /// <summary>
        /// context 메뉴 아이템 언어 설정
        /// </summary>
        /// <param name="iskorean"></param>
        public void SetLanguage(bool iskorean)
        {
            // 언어에 따라 메뉴 텍스트 변경
            if (iskorean)
            {
                contextMenuStrip.Items[0].Text = $"{ko.Bold} (Ctrl + B)";
                contextMenuStrip.Items[1].Text = $"{ko.ChangeColor} (Ctrl + Shift + C)";
                contextMenuStrip.Items[2].Text = $"{ko.ChangeBackGroundColor} (Ctrl + Shift + H)";
                contextMenuStrip.Items[3].Text = $"{ko.UnderLine} (Ctrl + U)";
                contextMenuStrip.Items[4].Text = $"{ko.Italic} (Ctrl + L)";
                contextMenuStrip.Items[5].Text = $"{ko.IncreaseFontSize} (Ctrl + Shift + ↑)";
                contextMenuStrip.Items[6].Text = $"{ko.DecreaseFontSize} (Ctrl + Shift + ↓)";
            }
            else
            {
                contextMenuStrip.Items[0].Text = $"{en.Bold} (Ctrl + B)";
                contextMenuStrip.Items[1].Text = $"{en.ChangeColor} (Ctrl + Shift + C)";
                contextMenuStrip.Items[2].Text = $"{en.ChangeBackGroundColor} (Ctrl + Shift + H)";
                contextMenuStrip.Items[3].Text = $"{en.UnderLine} (Ctrl + U)";
                contextMenuStrip.Items[4].Text = $"{en.Italic} (Ctrl + L)";
                contextMenuStrip.Items[5].Text = $"{en.IncreaseFontSize} (Ctrl + Shift + ↑)";
                contextMenuStrip.Items[6].Text = $"{en.DecreaseFontSize} (Ctrl + Shift + ↓)";
            }
        }
        #endregion

        #region 선택한 글자 굵게
        /// <summary>
        /// ctrl + B 로 선택한 글자 굵게
        /// </summary>
        private void ToggleBold()
        {
            if (!string.IsNullOrEmpty(this.SelectedText))
            {
                // 현재 선택된 텍스트의 폰트 가져오기
                Font currentFont = this.SelectionFont ?? this.Font;
                FontStyle newFontStyle = currentFont.Style;

                // 현재 굵게 설정되어 있으면 일반 스타일로, 아니면 굵게 변경
                if (currentFont.Style.HasFlag(FontStyle.Bold))
                {
                    newFontStyle &= ~FontStyle.Bold; // 굵게 해제
                }
                else
                {
                    newFontStyle |= FontStyle.Bold; // 굵게 설정
                }

                // 선택된 텍스트의 폰트 변경
                this.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }
        #endregion

        #region ctrl + shift + c로 선택한 글자 색상 변경
        /// <summary>
        /// 색깔 변경 텍스트
        /// </summary>
        private void ChangeSelectedTextColor()
        {
            // 선택된 텍스트 확인
            if (this.SelectedText.Length > 0)
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        this.SelectionColor = colorDialog.Color;
                    }
                }
            }
        }
        #endregion
        #region ctrl + shift + h로 선택한 글자 배경 색상 변경
        private void ChangeSelectionBackColor()
        {
            // 선택된 텍스트 확인
            if (this.SelectedText.Length > 0)
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 선택 영역의 배경색 변경
                        this.SelectionBackColor = colorDialog.Color;
                    }
                }
            }
            
        }
        #endregion
        #region 선택한 텍스트 영역 기울이기
        /// <summary>
        /// 선택한 텍스트 영역 기울이기
        /// </summary>
        private void ToggleItalic()
        {
            if (this.SelectionLength > 0)
            {
                Font currentFont = this.SelectionFont ?? this.Font;
                FontStyle newStyle = currentFont.Style;

                // 기울임 스타일 토글
                if (currentFont.Italic)
                    newStyle &= ~FontStyle.Italic; // 기울임 제거
                else
                    newStyle |= FontStyle.Italic; // 기울임 추가

                this.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }
        #endregion
        #region 선택한 텍스트 영역 밑줄
        /// <summary>
        /// 선택한 텍스트 영역 밑줄
        /// </summary>
        private void ToggleUnderline()
        {
            if (this.SelectionLength > 0)
            {
                Font currentFont = this.SelectionFont ?? this.Font;
                FontStyle newStyle = currentFont.Style;

                if ((currentFont.Style & FontStyle.Underline) == FontStyle.Underline)
                    newStyle &= ~FontStyle.Underline; // 밑줄 제거
                else
                    newStyle |= FontStyle.Underline; // 밑줄 추가

                // 선택된 텍스트에 새 스타일 적용
                this.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);

            }
        }
        #endregion

        #region 선택한 영역 글자 키우기
        /// <summary>
        /// 선택한 영역 글자 키우기
        /// </summary>
        private void IncreaseFontSize()
        {
            try
            {
                if (this.SelectionLength > 0)
                {
                    // 선택된 텍스트의 글꼴 크기 증가
                    Font currentFont = this.SelectionFont;
                    this.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size + 1, currentFont.Style);
                }
            }
            catch
            { }
        }
        #endregion

        #region 선택한 영역 글자 줄이기
        /// <summary>
        /// 선택한 영역 글자 줄이기
        /// </summary>
        private void DecreaseFontSize()
        {
            try
            {
                if (this.SelectionLength > 0)
                {
                    // 선택된 텍스트의 글꼴 크기 증가
                    Font currentFont = this.SelectionFont;
                    this.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size - 1, currentFont.Style);
                }
            }
            catch
            { }
        }
        #endregion

    
        /// <summary>
        /// 단축키 입력 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomRichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.B) // 굵게
            {
                ToggleBold();
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.C) // 색상 변경
            {
                ChangeSelectedTextColor();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.U) // 밑줄
            {
                ToggleUnderline();
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.L) // 기울이기
            {
                ToggleItalic();
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.Up) // 글자 크기 키우기
            {
                IncreaseFontSize();
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.Down) // 글자 크기 줄이기
            {
                DecreaseFontSize();
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.H) // Ctrl + Shift + B 단축키로 배경색 변경
            {
                ChangeSelectionBackColor();
                e.Handled = true;
            }
        }
      
    }
}