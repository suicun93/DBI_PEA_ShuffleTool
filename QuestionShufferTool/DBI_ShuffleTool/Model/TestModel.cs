using DBI_ShuffleTool.Entity.Paper;
using DBI_ShuffleTool.Entity.Question;
using DBI_ShuffleTool.Utils;
using DBI_ShuffleTool.Utils.Office;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace DBI_ShuffleTool.Model
{
    class TestModel
    {
        public string Path { get; set; }
        public ShuffleExamModel Sem { get; set; }


        public void CreateTests()
        {
            Path = FileUtils.CreateNewDirectory(Path, "DBI_Exam");
            SerializableUtils.WriteJson(Sem.PaperSet, Path + @"\PaperSet.dat");
            Process.Start(Path);
            try
            {
                foreach (Paper ei in Sem.PaperSet.Papers)
                {
                    //Create word file
                    ExportDocUtils.ExportDoc(ei, Path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int MaxNumberOfTests(List<Question> questionsBank)
        {
            int count = 1;
            foreach (Question question in questionsBank)
            {
                if (question == null || question.Candidates.Count == 0)
                {
                    continue;
                }
                count *= question.Candidates.Count;
            }
            if ((count) < 1) count = 1;
            return count;
        }
    }
}
