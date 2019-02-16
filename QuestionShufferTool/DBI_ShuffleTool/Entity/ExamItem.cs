using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class ExamItem
    {
        public ExamItem(string examItemCode, List<QuestionCandidate> examQuestionsList)
        {
            ExamItemCode = examItemCode;
            ExamQuestionsList = examQuestionsList;
        }
        public ExamItem()
        {
        }
        public String ExamItemCode { get; set; }
        public List<QuestionCandidate> ExamQuestionsList { get; set; }
    }
}
