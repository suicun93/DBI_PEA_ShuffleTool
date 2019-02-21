using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class ExamItem
    {
        public ExamItem(string examItemCode, List<Candidate> examQuestionsList)
        {
            PaperNo = examItemCode;
            ExamQuestionsList = examQuestionsList;
        }
        public ExamItem()
        {
        }
        public String PaperNo { get; set; }
        public List<Candidate> ExamQuestionsList { get; set; }
    }
}
