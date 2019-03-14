using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity.Question
{
    [Serializable]
    class QuestionSet
    {
        public List<Question> QuestionList { get; set; }
        public List<string> DBScriptList { get; set; }
    }
}
