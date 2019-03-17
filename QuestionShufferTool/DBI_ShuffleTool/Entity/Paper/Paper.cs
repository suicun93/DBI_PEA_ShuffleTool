using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity.Paper
{
    [Serializable]
    class Paper
    {
        public Paper(string paperNo, List<Candidate> candidateSet)
        {
            PaperNo = paperNo;
            CandidateSet = candidateSet;
        }
        public Paper()
        {
        }
        public string PaperNo { get; set; }
        public List<Candidate> CandidateSet { get; set; }
    }
}
