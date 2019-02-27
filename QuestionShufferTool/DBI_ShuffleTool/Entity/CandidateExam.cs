using System.Collections.Generic;
using DBI_ShuffleTool.Entity;

namespace DBI_ShuffleTool.Entity
{
    class CandidateExam
    {
        public string CandidateId { get; set; }
        public string QuestionId { get; set; }
        public Candidate.QuestionTypes QuestionType { get; set; }

        public string Solution { get; set; }
        public string ActivateQuery { get; set; }
        public List<Requirement> Requirements { get; set; }
        public double Point { get; set; }

        public CandidateExam()
        {
            QuestionType = Candidate.QuestionTypes.Select;
            Requirements = new List<Requirement>();
        }
    }
}
