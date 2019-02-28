using System.Collections.Generic;
using DBI_ShuffleTool.Entity;

namespace DBI_ShuffleTool.Entity
{
    class CandidateExam
    {
        public string CandidateId { get; set; }
        public string QuestionId { get; set; }
        public string Content { get; set; }
        public Candidate.QuestionTypes QuestionType { get; set; }
        public List<string> Images { get; set; }

        public string Solution { get; set; }
        public string ActivateQuery { get; set; }

        public bool ResultSet { get; set; }
        public bool RequireSort { get; set; }

        public bool Effect { get; set; }
        public string CheckEffectQuery { get; set; }

        public string DBName { get; set; }
        public double Point { get; set; }

        //public List<Requirement> Requirements { get; set; }

        public CandidateExam()
        {
            QuestionType = Candidate.QuestionTypes.Select;
            Images = new List<string>();
        }
    }
}
