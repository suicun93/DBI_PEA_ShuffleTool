using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class Question
    {
        public Question(int questionNumber, List<Candidate> candidates)
        {
            QuestionId = questionNumber;
            Candidates = candidates;
        }

        public int QuestionId { get; set; }
        public double Point { get; set; }
        public List<Candidate> Candidates { get; set; }
    }
}
