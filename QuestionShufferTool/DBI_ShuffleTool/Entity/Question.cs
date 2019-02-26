using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class Question
    {
        public string QuestionId { get; set; }
        public double Point { get; set; }
        public List<Candidate> Candidates { get; set; }
    }
}
