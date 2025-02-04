using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Threading;

namespace Note
{
    public partial class Form1 : Form
    {
        #region 초기화 관련
        private ContextMenuStrip contextMenuStrip;
        bool IsKorean = true;
        public Form1()
        {
            InitializeComponent();
            InitializeTabControl();
            Load_Note();
            this.KeyPreview = true;
            if (TC.TabPages.Count == 0) // 저장되지 않은 기본 상태인 경우
            {
                AddNote();
            }
        }

        #region 저장 및 불러오기
        /// <summary>
        /// json 파일이 있다면 불러오기
        /// </summary>
        private void Load_Note()
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GunsNote.json");

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var notes = JsonSerializer.Deserialize<List<NoteData>>(json);

                    if (notes != null)
                    {
                        foreach (var note in notes)
                        {
                            TabPage tab = new TabPage(note.TabTitle);
                            CustomRichTextBox richTextBox = new CustomRichTextBox()
                            {
                                Rtf = note.Content, // RTF 데이터로 복원
                                Dock = DockStyle.Fill
                            };
                            tab.Controls.Add(richTextBox);
                            TC.TabPages.Add(tab);
                            FilePaths.Add("");
                        }
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 현재 노트 내용 json 파일에 저장
        /// </summary>
        private void SaveNotes()
        {
            try
            {
                List<NoteData> notes = new List<NoteData>();
                foreach (TabPage tab in TC.TabPages)
                {
                    var richTextBox = tab.Controls.OfType<RichTextBox>().FirstOrDefault();
                    if (richTextBox != null)
                    {
                        notes.Add(new NoteData
                        {
                            Content = richTextBox.Rtf, // RTF 데이터로 저장
                            TabTitle = tab.Text
                        });
                    }
                }
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GunsNote.json");
                File.WriteAllText(filePath, JsonSerializer.Serialize(notes));
            }
            catch { };
        }
        /// <summary>
        /// 폼 닫힐 때 현재 내용 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveNotes();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void InitializeTabControl()
        {
            // ContextMenuStrip 설정
            contextMenuStrip = new ContextMenuStrip();
            if (IsKorean)
            {
                contextMenuStrip.Items.Add(ko.AddNote, null, (s, e) => AddNote());
                //contextMenuStrip.Items.Add(ko.RenameNote, null, (s, e) => RenameNote());
                contextMenuStrip.Items.Add(ko.DeleteNote, null, (s, e) => DeleteNote());
            }
            else
            {
                contextMenuStrip.Items.Add(en.AddNote, null, (s, e) => AddNote());
                //contextMenuStrip.Items.Add(en.RenameNote, null, (s, e) => RenameNote());
                contextMenuStrip.Items.Add(en.DeleteNote, null, (s, e) => DeleteNote());

            }
        }
        #endregion
        #region 번역

        /// <summary>
        /// 영어 설정 선택 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsKorean = false;
            Name = Note.en.Note;
            InitializeTabControl();
            for (int i = 0; i < TC.TabCount; i++)
            {
                foreach (Control control in TC.TabPages[i].Controls)
                {
                    if (control is CustomRichTextBox customRTB)
                    {
                        customRTB.SetLanguage(false);
                    }
                }
            }
            fileToolStripMenuItem.Text = en.File;
            fileToolStripMenuItem.DropDownItems[0].Text = en.Open;
            fileToolStripMenuItem.DropDownItems[1].Text = en.Save;
            fileToolStripMenuItem.DropDownItems[2].Text = en.SaveAs;

        }

        /// <summary>
        /// 한국어  설정 선택 시
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void koreanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IsKorean = true;
            Name = Note.ko.Note;
            InitializeTabControl();
            for (int i = 0; i < TC.TabCount; i++)
            {
                foreach (Control control in TC.TabPages[i].Controls)
                {
                    if (control is CustomRichTextBox customRTB)
                    {
                        customRTB.SetLanguage(true);
                    }
                }
            }
            fileToolStripMenuItem.Text = ko.File;
            fileToolStripMenuItem.DropDownItems[0].Text = ko.Open;
            fileToolStripMenuItem.DropDownItems[1].Text = ko.Save;
            fileToolStripMenuItem.DropDownItems[2].Text = ko.SaveAs;
        }
        #endregion

        /// <summary>
        /// 노트 추가
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TC_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 클릭 위치에서 TabPage 찾기
                TabPage clickedTab = GetTabAtPoint(e.Location);

                if (clickedTab != null)
                {
                    // 클릭된 탭 선택
                    TC.SelectedTab = clickedTab;
                    contextMenuStrip.Show(TC, e.Location);
                }
            }
        }
        /// <summary>
        /// 몇번째 노트를 선택했는지 찾기
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private TabPage GetTabAtPoint(Point point)
        {
            for (int i = 0; i < TC.TabCount; i++)
            {
                if (TC.GetTabRect(i).Contains(point))
                {
                    return TC.TabPages[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 노트 추가
        /// </summary>
        private void AddNote()
        {
            // 새 TabPage 생성
            string TabPageName;
            if (IsKorean) TabPageName = ko.Untitled;
            else TabPageName = en.Untitled;
            TabPage newTabPage = new TabPage(TabPageName);

            // 커스텀 RichTextBox 생성
            CustomRichTextBox CRTB = new CustomRichTextBox();
            CRTB.SetLanguage(IsKorean);
            // TabPage에 RichTextBox 추가
            newTabPage.Controls.Add(CRTB);

            // TabControl에 TabPage 추가
            TC.TabPages.Add(newTabPage);

            // 새로 추가된 탭으로 전환
            TC.SelectedTab = newTabPage;
            FilePaths.Add("");
        }

        /// <summary>
        /// 노트 이름 바꾸기
        /// </summary>
        private void RenameNote()
        {
            if (TC.SelectedTab != null)
            {
                using (RenameNoteName renameNote = new RenameNoteName(IsKorean))
                {
                    if(renameNote.ShowDialog() == DialogResult.OK)
                    {
                        TC.SelectedTab.Text = renameNote.Rename;
                    }
                    else
                    {

                    }
                }
            }

        }

        /// <summary>
        /// 선택한 노트 삭제하기
        /// </summary>
        private void DeleteNote()
        {
            if (TC.SelectedTab != null)
            {
                if (TC.Controls.Count == 1)
                {
                    if (IsKorean)
                    {
                        MessageBox.Show(ko.msg_ThereMustBeAtLeastOneNote);
                    }
                    else
                    {
                        MessageBox.Show(en.msg_ThereMustBeAtLeastOneNote);
                    }
                }
                else
                {
                    FilePaths.RemoveAt(TC.SelectedIndex);
                    TC.TabPages.Remove(TC.SelectedTab);
                }
            }
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void 다른이름으로저ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }
        //string currentFilePath = null;
        List<string> FilePaths = new List<string>();
        // 현재 활성화된 RichTextBox 가져오기
        private RichTextBox GetCurrentRichTextBox()
        {
            if (TC.SelectedTab?.Controls.Count > 0)
            {
                return TC.SelectedTab.Controls[0] as RichTextBox;
            }
            return null;
        }

        // 저장 기능
        private void SaveFile()
        {
            var richTextBox = GetCurrentRichTextBox();
            if (richTextBox == null)
            {
                string ThereIsNoContentToSave = "", Error = "";
                if (IsKorean)
                {
                    ThereIsNoContentToSave = ko.ThereIsNoContentToSave_;
                    Error = ko.Error;
                }
                else
                {
                    ThereIsNoContentToSave = en.ThereIsNoContentToSave_;
                    Error = en.Error;
                }
                MessageBox.Show(ThereIsNoContentToSave, Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string currentFilePath = FilePaths[TC.SelectedIndex];
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveAsFile(); // 파일 경로가 없으면 '다른 이름으로 저장'
            }
            else
            {
                string TheFileHasBeenSaved = "", SaveCompleted = "", AnErrorOccurredWhileSavingTheFile = "", Error = "";
                if (IsKorean)
                {
                    TheFileHasBeenSaved = ko.TheFileHasBeenSaved_;
                    SaveCompleted = ko.SaveCompleted;
                    AnErrorOccurredWhileSavingTheFile = ko.AnErrorOccurredWhileSavingTheFile_;
                    Error = ko.Error;
                }
                else
                {
                    TheFileHasBeenSaved = en.TheFileHasBeenSaved_;
                    SaveCompleted = en.SaveCompleted;
                    AnErrorOccurredWhileSavingTheFile = en.AnErrorOccurredWhileSavingTheFile_;
                    Error = en.Error;
                }
                try
                {
                    File.WriteAllText(currentFilePath, richTextBox.Text);
                    MessageBox.Show(TheFileHasBeenSaved, SaveCompleted, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{AnErrorOccurredWhileSavingTheFile}: {ex.Message}", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 다른 이름으로 저장 기능
        private void SaveAsFile()
        {
            var richTextBox = GetCurrentRichTextBox();
            if (richTextBox == null)
            {
                string ThereIsNoContentToSave = "", Error = "";
                if (IsKorean)
                {
                    ThereIsNoContentToSave = ko.ThereIsNoContentToSave_;
                    Error = ko.Error;
                }
                else
                {
                    ThereIsNoContentToSave = en.ThereIsNoContentToSave_;
                    Error = en.Error;
                }
                MessageBox.Show(ThereIsNoContentToSave, Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string TextFile = "", SaveAs = "";
                if (IsKorean)
                {
                    TextFile = ko.TextFile;
                    SaveAs = ko.SaveAsText;
                }
                else
                {
                    TextFile = en.TextFile;
                    SaveAs = en.SaveAsText;
                }
                saveFileDialog.Filter = $"{TextFile} (*.txt)|*.txt";
                saveFileDialog.Title = SaveAs;
         
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string TheFileHasBeenSaved = "", SaveCompleted = "", AnErrorOccurredWhileSavingTheFile = "", Error = "";
                    if (IsKorean)
                    {
                        TheFileHasBeenSaved = ko.TheFileHasBeenSaved_;
                        SaveCompleted = ko.SaveCompleted;
                        AnErrorOccurredWhileSavingTheFile = ko.AnErrorOccurredWhileSavingTheFile_;
                        Error = ko.Error;
                    }
                    else
                    {
                        TheFileHasBeenSaved = en.TheFileHasBeenSaved_;
                        SaveCompleted = en.SaveCompleted;
                        AnErrorOccurredWhileSavingTheFile = en.AnErrorOccurredWhileSavingTheFile_;
                        Error = en.Error;
                    }
                    try
                    {
                        string currentFilePath = FilePaths[TC.SelectedIndex] = saveFileDialog.FileName;
                        File.WriteAllText(currentFilePath, richTextBox.Text);
                        TC.SelectedTab.Text = Path.GetFileName(currentFilePath);
                        MessageBox.Show(TheFileHasBeenSaved, SaveCompleted, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{AnErrorOccurredWhileSavingTheFile}: {ex.Message}", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        /// <summary>
        /// 파일 열기
        /// </summary>
        internal void OpenFile()
        {
            string TextFiles = "", OpenTextFile = "";
            if (IsKorean)
            {
                TextFiles = ko.TextFiles;
                OpenTextFile = ko.OpenTextFile;
            }
            else
            {
                TextFiles = en.TextFiles;
                OpenTextFile = en.OpenTextFile;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = $"{TextFiles} (*.txt)|*.txt",
                Title = OpenTextFile
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {   
                    // 새 TabPage 생성
                    string TabPageName;
                    if (IsKorean) TabPageName = ko.Untitled;
                    else TabPageName = en.Untitled;
                    TabPage newTabPage = new TabPage(TabPageName);

                    // 커스텀 RichTextBox 생성
                    CustomRichTextBox CRTB = new CustomRichTextBox();
                    CRTB.SetLanguage(IsKorean);
                    newTabPage.Text = Path.GetFileName(openFileDialog.FileName);
                    CRTB.Text = File.ReadAllText(openFileDialog.FileName);
                    // TabPage에 RichTextBox 추가
                    newTabPage.Controls.Add(CRTB);

                    // TabControl에 TabPage 추가
                    TC.TabPages.Add(newTabPage);
                    FilePaths.Add(openFileDialog.FileName);
                    // 새로 추가된 탭으로 전환
                    TC.SelectedTab = newTabPage;
                }
                catch (Exception ex)
                {
                    string ErrorReadingFile = "", Error = "";
                    if (IsKorean)
                    {
                        ErrorReadingFile = ko.ErrorReadingFile;
                        Error = ko.Error;
                    }
                    else
                    {
                        ErrorReadingFile = en.ErrorReadingFile;
                        Error = en.Error;
                    }
                    MessageBox.Show($"{ErrorReadingFile}: {ex.Message}", Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 키 입력 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O) // 밑줄
            {
                OpenFile();
                e.Handled = true;
            }
            else if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.S) // 저장
            {
                SaveAsFile();
                e.Handled = true;
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S) // 다른 이름 저장
            {
                SaveFile();
                e.Handled = true;
            }
        }
    }
}
