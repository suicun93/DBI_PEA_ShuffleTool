using System;
using System.Collections.Generic;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class QuestionsBank
    {
        public QuestionsBank(List<Question> qBank)
        {
            QBank = qBank;
        }

        public List<Question> QBank { get; set; }


    }
}
