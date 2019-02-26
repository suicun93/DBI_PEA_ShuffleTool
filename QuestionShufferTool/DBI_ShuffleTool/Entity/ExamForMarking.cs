using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBI_ShuffleTool.Entity
{
    class ExamForMarking
    {
        public ExamForMarking()
        {
        }
        public string PaperNo { get; set; }
        public List<CandidateExam> ExamQuestionsList { get; set; }
    }
}
