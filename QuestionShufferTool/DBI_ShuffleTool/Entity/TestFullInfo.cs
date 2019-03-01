﻿using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class TestFullInfo
    {
        public TestFullInfo(string examItemCode, List<Candidate> examQuestionsList)
        {
            PaperNo = examItemCode;
            ExamQuestionsList = examQuestionsList;
        }
        public TestFullInfo()
        {
        }
        public string PaperNo { get; set; }
        public List<Candidate> ExamQuestionsList { get; set; }
    }
}