﻿using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class Candidate
    {
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public string QuestionType { get; set; }
        public string ImageData { get; set; }
        public double Point { get; set; }
        public List<Requirement> Requirements { get; set; }
    }
}