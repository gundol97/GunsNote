using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note
{
    internal class NoteData
    {
        public string Content { get; set; } = string.Empty; // RTF 데이터 저장
        public string TabTitle { get; set; } = "New Tab"; // 탭 제목
    }
}
