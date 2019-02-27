using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class Candidate
    {
        public enum QuestionTypes
        {
            Select = 1,
            Procedure = 2,
            Trigger = 3,
            Schema = 4,
            DML = 5
        }

        public string CandidateId { get; set; }
        public string QuestionId { get; set; }
        public string Content { get; set; }
        public QuestionTypes QuestionType { get; set; }
        public List<string> Images { get; set; }

        public string Solution { get; set; }
        public string ActivateQuery { get; set; }
        public List<Requirement> Requirements { get; set; }
        public double Point { get; set; }

        public Candidate()
        {
            QuestionType = QuestionTypes.Select;
            Requirements = new List<Requirement>();
            Images = new List<string>();
        }

    }
}