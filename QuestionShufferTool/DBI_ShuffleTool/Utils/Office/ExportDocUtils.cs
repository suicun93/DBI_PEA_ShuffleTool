using System;
using System.IO.Compression;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DBI_ShuffleTool.Entity.Paper;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Linq;
using DBI_ShuffleTool.Entity;

namespace DBI_ShuffleTool.Utils.Office
{
    static class ExportDocUtils
    {
        /// <summary>
        /// Export Doc in path
        /// </summary>
        /// <param name="paper"></param>
        /// <param name="path">Save to location</param>
        /// <returns></returns>
        public static void ExportDoc(Paper paper, string path)
        {
            Application wordApp = null;
            try
            {
                //Start new Word Application
                wordApp = new Application
                {
                    Visible = false,
                    ShowAnimation = false
                };
                object missing = Missing.Value;
                var doc = wordApp.Documents.Add(missing);
                //Settings Page
                DocUtils.SettingsPage(doc);

                //Setings Header and Footer of the page
                DocUtils.SettingsHeaderAndFooter(paper, doc);

                //Insert QuestionRequirement of the Exam
                for (int i = 0; i < paper.CandidateSet.Count; i++)
                {
                    AppendTestQuestion(paper.CandidateSet.ElementAt(i), doc, (i + 1), ref missing);
                }
                //Saving file
                DocUtils.SavingDocFile(doc, path, paper);
            }
            finally
            {
                wordApp?.Application.Quit(false);
            }
        }

        /// <summary>
        /// Append QuestionRequirement of Question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="section"></param>
        private static void AppendTestQuestion(Candidate q, Document doc, int questionNumber, ref object missing)
        {
            try
            {
                Paragraph paraQuestionNumber = doc.Content.Paragraphs.Add(ref missing);
                string question = "Question " + questionNumber + ":";
                paraQuestionNumber.Range.Text = question;
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
                string imageName = AppDomain.CurrentDomain.BaseDirectory + @"/" + q.CandidateId + ".bmp";
                foreach (var image in images)
                {
                    if (ImageUtils.Base64ToImage(image) != null)
                    {
                        Image img = ImageUtils.Base64ToImage(image);
                        Image tempImg = new Bitmap(img);
                        tempImg.Save(imageName);
                        Paragraph paraImage = doc.Content.Paragraphs.Add(ref missing);
                        paraImage.Range.InlineShapes.AddPicture(imageName);
                        paraImage.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        Paragraph paraImageDescription = doc.Content.Paragraphs.Add(ref missing);
                        paraImageDescription.Range.Text = "Picture " + questionNumber + "." + (++i) + "";
                        paraImageDescription.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        paraImageDescription.Range.InsertParagraphAfter();
                    }
                }
                if (File.Exists(imageName)) File.Delete(imageName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
