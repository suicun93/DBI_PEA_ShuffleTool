using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.Model;
using DBI_ShuffleTool.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DBI_ShuffleTool.UI
{
    public partial class ShuffleTool : Form
    {
        ShuffleExamModel sem;
        QuestionsBank qb;
        String outputPath;

        public ShuffleTool()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            String inputPath = FileUtils.GetFileLocation();
            txtLocationFolderInput.Text = inputPath;
            //Reading data
            qb = new QuestionsBank(JsonUtils.DeserializeJson(inputPath));
            //

        }

        private void btnCreateTests_Click(object sender, EventArgs e)
        {
            

            //Create Test
            int numOfPage = Convert.ToInt32(txtNumberOfTest.Value);
            sem = new ShuffleExamModel(qb, numOfPage);

            outputPath = FileUtils.SaveFileToLocation();
            DocUtils.exportDoc(outputPath, sem.GetExamItemsList());


            if (JsonUtils.WriteJson(sem.GetExamItemsList(), outputPath))
            {
                MessageBox.Show(ConstantUtils.MESSAGE_CREATED_FOLDER_OK);
            }
            else
            {
                MessageBox.Show(ConstantUtils.ERROR_COMMON);
            }

        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(outputPath);
        }

        private void btnSaveTestsAs_Click(object sender, EventArgs e)
        {
            outputPath = FileUtils.SaveFileToLocation();
            DocUtils.exportDoc(outputPath, sem.GetExamItemsList());
            if (JsonUtils.WriteJson(sem.GetExamItemsList(), outputPath))
            {
                MessageBox.Show(ConstantUtils.MESSAGE_CREATED_FOLDER_OK);
            }
            else
            {
                MessageBox.Show(ConstantUtils.ERROR_COMMON);
            }
        }

        //private void sample()
        //{
        //    List<Candidate> lqc = new List<Candidate>();
        //    lqc.Add(new Candidate("1", "1", "12456", Image.FromFile("9.png")));
        //    lqc.Add(new Candidate("1", "2", "12456", Image.FromFile("9.png")));
        //    lqc.Add(new Candidate("1", "3", "12456", Image.FromFile("9.png")));
        //    List<Candidate> lqc2 = new List<Candidate>();
        //    lqc2.Add(new Candidate("2", "1", "12456", Image.FromFile("9.png")));
        //    lqc2.Add(new Candidate("2", "2", "12456", Image.FromFile("9.png")));
        //    lqc2.Add(new Candidate("2", "3", "12456", Image.FromFile("9.png")));
        //    lqc2.Add(new Candidate("2", "4", "12456", Image.FromFile("9.png")));
        //    lqc2.Add(new Candidate("2", "5", "12456", Image.FromFile("9.png")));
        //    List<Candidate> lqc3 = new List<Candidate>();
        //    lqc3.Add(new Candidate("3", "1", "12456", Image.FromFile("9.png")));
        //    lqc3.Add(new Candidate("3", "2", "12456", Image.FromFile("9.png")));
        //    lqc3.Add(new Candidate("3", "3", "12456", Image.FromFile("9.png")));
        //    lqc3.Add(new Candidate("3", "4", "12456", Image.FromFile("9.png")));
        //    List<Candidate> lqc4 = new List<Candidate>();
        //    lqc4.Add(new Candidate("4", "4", "12456", Image.FromFile("9.png")));
        //    lqc4.Add(new Candidate("4", "3", "12456", Image.FromFile("9.png")));
        //    lqc4.Add(new Candidate("4", "2", "12456", Image.FromFile("9.png")));
        //    lqc4.Add(new Candidate("4", "1", "12456", Image.FromFile("9.png")));
        //    List<Candidate> lqc5 = new List<Candidate>();
        //    lqc5.Add(new Candidate("5", "1", "12456", Image.FromFile("9.png")));

        //    List<Question> lq = new List<Question>();
        //    lq.Add(new Question("1", lqc));
        //    lq.Add(new Question("2", lqc2));
        //    lq.Add(new Question("3", lqc3));
        //    lq.Add(new Question("4", lqc4));
        //    lq.Add(new Question("5", lqc5));
        //    qb = new QuestionsBank(lq);
        //}

        private void txtNumberOfTest_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
