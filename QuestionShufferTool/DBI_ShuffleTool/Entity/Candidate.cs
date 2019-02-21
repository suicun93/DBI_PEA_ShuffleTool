using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class Candidate
    {
        public enum QuestionTypes
        {
            Query = 1,
            Procedure = 2,
            Trigger = 3
        }

        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public string ImageData { get; set; }
        public List<Requirement> Requirements { get; set; }
        public double Point { get; set; }
    }
}