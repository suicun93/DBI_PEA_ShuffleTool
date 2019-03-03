using System;
using System.Collections.Generic;
using System.Drawing;
using DBI_ShuffleTool.Entity;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Linq;

namespace DBI_ShuffleTool.Utils.Office
{
    static class ExportDocUtils
    {
        /// <summary>
        /// Export Doc in path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="examItems"></param>
        /// <returns></returns>
        public static bool ExportDoc(string path, List<TestFullInfo> examItems)
        {
            foreach (TestFullInfo ei in examItems)
            {
                Application wordApp = new Application();
                try
                {
                    //Create word file
                    wordApp.Visible = false;
                    wordApp.ShowAnimation = false;
                    object missing = Missing.Value;
                    Document doc = new Document();

                    //Settings Page
                    DocUtils.SettingsPage(doc);

                    //Setings Header and Footer of the page
                    DocUtils.SettingsHeaderAndFooter(ei, doc);

                    //Insert QuestionRequirement of the Exam
                    for (int i = 0; i < ei.ExamQuestionsList.Count; i++)
                    {
                        AppendTestQuestion(ei.ExamQuestionsList.ElementAt(i), doc, (i + 1), ref missing);
                    }

                    //Saving file
                    doc.SaveAs(path + @"\" + ei.PaperNo, WdSaveFormat.wdFormatDocument97);
                    doc.Close();
                }
                catch (Exception e)
                {

                    wordApp = null;
                    throw e;
                }
            }
            return true;
        }


        

        /// <summary>
        /// Append QuestionRequirement of Question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="section"></param>
        static private void AppendTestQuestion(Candidate q, Document doc, int questionNumber, ref object missing)
        {
            Paragraph paraQuestionNumber = doc.Content.Paragraphs.Add(ref missing);
            string question = "Question " + questionNumber + ":";
            string questionId = " [" + q.QuestionId + "]";
            paraQuestionNumber.Range.Text = string.Concat(question, questionId);

            Range questionNumberRange = doc.Range(paraQuestionNumber.Range.Start, paraQuestionNumber.Range.Start + question.Length);
            questionNumberRange.Font.Bold = 1;
            questionNumberRange.Font.Underline = WdUnderline.wdUnderlineSingle;

            paraQuestionNumber.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraQuestionNumber.Range.Font.Name = "Arial";

            paraQuestionNumber.Range.InsertParagraphAfter();

            Paragraph paraContent = doc.Content.Paragraphs.Add(ref missing);
            paraContent.Range.Font.Bold = 0;
            paraContent.Range.Font.Underline = WdUnderline.wdUnderlineNone;
            paraContent.Range.Text = q.QuestionRequirement;
            paraContent.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraContent.Range.InsertParagraphAfter();

            List<string> images = q.Illustration;
            int i = 0;
            foreach (var image in images)
            {
                if (ImageUtils.Base64ToImage(image) != null)
                {
                    Image img = ImageUtils.Base64ToImage(image);
                    Image tempImg = new Bitmap(img);
                    string imageName = AppDomain.CurrentDomain.BaseDirectory + @"/tmpImg.bmp";
                    tempImg.Save(imageName);
                    Paragraph paraImage = doc.Content.Paragraphs.Add(ref missing);
                    InlineShape pictureShape = paraImage.Range.InlineShapes.AddPicture(imageName);
                    paraImage.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    Paragraph paraImageDescription = doc.Content.Paragraphs.Add(ref missing);
                    paraImageDescription.Range.Text = "Picture " + questionNumber + "." + (++i) + "";
                    paraImageDescription.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraImageDescription.Range.InsertParagraphAfter();
                }
            }
        }
    }
}
