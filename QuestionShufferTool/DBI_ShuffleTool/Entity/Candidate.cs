using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class Candidate
    {

        public String CandidateId { get; set; }
        public String QuestionId { get; set; }
        public String Content { get; set; }
        public String QuestionType { get; set; }
        public String ImageData { get; set; }
        public double Point { get; set; }
        public List<Requirement> Requirements { get; set; }


    }
}