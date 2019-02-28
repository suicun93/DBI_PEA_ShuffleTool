using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBI_ShuffleTool.Entity
{
    class TestItem
    {
        public TestItem()
        {
        }
        public string PaperNo { get; set; }
        public List<CandidateSimple> ExamQuestionsList { get; set; }
    }
}
