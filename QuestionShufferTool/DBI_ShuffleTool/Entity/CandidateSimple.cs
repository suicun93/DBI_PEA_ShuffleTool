using System.Collections.Generic;
using DBI_ShuffleTool.Entity;

namespace DBI_ShuffleTool.Entity
{
    class CandidateSimple
    {
        
        public string CandidateId { get; set; }
        public string QuestionId { get; set; }
        public string QuestionRequirement { get; set; }
        public Candidate.QuestionTypes QuestionType { get; set; }

        public string Solution { get; set; }
        public string TestQuery { get; set; }

        public bool RequireSort { get; set; }

        public string DBName { get; set; }

        public double Point { get; set; }

        public CandidateSimple()
        {
            QuestionType = Candidate.QuestionTypes.Select;
        }
    }
}
