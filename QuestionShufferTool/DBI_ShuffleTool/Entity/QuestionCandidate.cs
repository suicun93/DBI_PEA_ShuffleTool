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

        public QuestionCandidate(string qCandidateName, string qContent, Bitmap image)
        {
            QCandidateName = qCandidateName;
            QContent = qContent;
            Image = image;
        }

        private string QCandidateName { get; set; }
        private string QContent { get; set; }
        private Bitmap Image { get; set; }





    }
}