using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class ExamForDoc
    {
        public ExamForDoc(string examItemCode, List<Candidate> examQuestionsList)
        {
            PaperNo = examItemCode;
            ExamQuestionsList = examQuestionsList;
        }
        public ExamForDoc()
        {
        }
        public string PaperNo { get; set; }
        public List<Candidate> ExamQuestionsList { get; set; }
    }
}
