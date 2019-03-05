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
        /// <param name="path">Save to location</param>
        /// <param name="examItems"></param>
        /// <param name="wordApp">Application</param>
        /// <returns></returns>
        public static void ExportDoc(Object obj)
        {
            TestThreadEntity appInfo = (TestThreadEntity)obj;
            Application wordApp = null;
            Document doc = null;
            try
            {
                //Create word file
                wordApp = new Application();
                wordApp.Visible = false;
                wordApp.ShowAnimation = false;
                object missing = Missing.Value;
                doc = new Document();

                //Settings Page
                DocUtils.SettingsPage(doc);

                //Setings Header and Footer of the page
                DocUtils.SettingsHeaderAndFooter(appInfo.TestItem, doc);

                //Insert QuestionRequirement of the Exam
                for (int i = 0; i < appInfo.TestItem.ExamQuestionsList.Count; i++)
                {
                    AppendTestQuestion(appInfo.TestItem.ExamQuestionsList.ElementAt(i), doc, (i + 1), ref missing);
                }

                //Saving file
                doc.SaveAs(appInfo.Path + @"\" + appInfo.TestItem.PaperNo, WdSaveFormat.wdFormatDocument97);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                doc.Close();
                wordApp.Quit();
                wordApp = null;
            }
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
