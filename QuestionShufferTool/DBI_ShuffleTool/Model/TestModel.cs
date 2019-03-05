using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.Utils;
using DBI_ShuffleTool.Utils.Office;
using System;
using System.Collections.Generic;
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
            try
            {
                foreach (TestFullInfo ei in Sem.EiListDoc)
                {
                    //Create word file
                    TestThreadEntity appInfo = new TestThreadEntity();
                    appInfo.Path = Path;
                    appInfo.TestItem = ei;
                    Thread newThread = new Thread(ExportDocUtils.ExportDoc);
                    newThread.Start(appInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            SerializableUtils.Serialize(Sem.EiListMarking, Path);
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
