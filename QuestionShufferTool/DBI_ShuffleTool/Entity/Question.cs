using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class Question
    {
        public Question(string questionNumber, List<QuestionCandidate> listQCandidate)
        {
            QuestionNumber = questionNumber;
            ListQCandidate = listQCandidate;
        }

        public String QuestionNumber { get; set; }
        public List<QuestionCandidate> ListQCandidate { get; set; }
    }
}
