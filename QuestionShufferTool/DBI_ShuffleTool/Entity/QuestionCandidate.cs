using System;
using System.Drawing;

namespace DBI_ShuffleTool.Entity
{
    [Serializable]
    class QuestionCandidate
    {
        public QuestionCandidate()
        {
        }

        public QuestionCandidate(string qCandidateName, string qContent)
        {
            QCandidateName = qCandidateName;
            QContent = qContent;
        }

        public string QCandidateName { get; set; }
        public string QContent { get; set; }





    }
}