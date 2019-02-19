using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using DBI_ShuffleTool.Entity;
using DBI_ShuffleTool.Model;
using DBI_ShuffleTool.Utils;
using Newtonsoft.Json;

namespace DBI_ShuffleTool.UI
{
    public partial class ShuffleTool : Form
    {
        

        public ShuffleTool()
        {
            InitializeComponent();
            List<QuestionCandidate> lqc = new List<QuestionCandidate>();
            lqc.Add(new QuestionCandidate("11", "content"));
            lqc.Add(new QuestionCandidate("12", "content"));
            List<QuestionCandidate> lqc2 = new List<QuestionCandidate>();
            lqc2.Add(new QuestionCandidate("21", "2content"));
            lqc2.Add(new QuestionCandidate("22", "2content"));
            List<QuestionCandidate> lqc3 = new List<QuestionCandidate>();
            lqc3.Add(new QuestionCandidate("31", "3content"));
            lqc3.Add(new QuestionCandidate("32", "3content"));
            List<QuestionCandidate> lqc4 = new List<QuestionCandidate>();
            lqc4.Add(new QuestionCandidate("41", "4content"));
            lqc4.Add(new QuestionCandidate("42", "4content"));
            List<QuestionCandidate> lqc5 = new List<QuestionCandidate>();
            lqc5.Add(new QuestionCandidate("51", "5content"));
            lqc5.Add(new QuestionCandidate("52", "5content"));
            List<Question> lq = new List<Question>();
            lq.Add(new Question("1", lqc));
            lq.Add(new Question("2", lqc2));
            lq.Add(new Question("3", lqc3));
            lq.Add(new Question("4", lqc4));
            lq.Add(new Question("5", lqc5));

            QuestionsBank qb = new QuestionsBank(lq);
            ShuffleExamModel sem = new ShuffleExamModel(qb, 10);
            List<ExamItem> eiList = sem.getExamItemsList();
            Console.WriteLine(JsonConvert.SerializeObject(eiList));

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtLocationFolderInput.Text = FileUtils.GetFileLocation();

            //String location = ImportModel.getLocationFolderOfBank();
            //if (location.Equals(ConstantUtils.ERROR_LOAD_FOLDER_FAILED)){ 
            //    MessageBox.Show(ConstantUtils.ERROR_LOAD_FOLDER_FAILED);
            //}
            //else
            //{
            //    txtLocationFolderInput.Text = location;
            //    MessageBox.Show(ConstantUtils.MESSAGE_LOAD_FOLDER_OK);
            //}
            
        }

        private void btnCreateTests_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the File
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Data File|*.dat";
            sfd.Title = "Save a File";
            sfd.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (sfd.FileName != "")
            {

                // Saves the Image via a FileStream created by the OpenFile method.  
                FileStream fs = (FileStream) sfd.OpenFile();
                // Write File here

                // Close FileStream
                fs.Close();
            }


        }
    }
}
