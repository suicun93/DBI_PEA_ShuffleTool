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
        /// <param name="examItems"></param>
        /// <param name="wordApp">Application</param>
        /// <param name="tmpPath"></param>
        /// <returns></returns>
        public static void ExportDoc(Paper paper, string path, string tmpPath, string firstPagePath)
        {
            Application wordApp = null;
            try
            {
                FileUtils.CopyDirectory(@".\00_Database_Diagram_First_Page", tmpPath, true);
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                //Create word file
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
                ExportMaterial(paper, doc, tmpPath);


            }
            finally
            {
                wordApp?.Application.Quit(false);
            }
            //Compress
            CompressZip(paper.PaperNo, tmpPath + @"\", path);
            FileUtils.DeleteDirectory(tmpPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldPath"></param>
        /// <param name="savePath"></param>
        private static void CompressZip(string name, string oldPath, string savePath)
        {
            try
            {
                string zipPath = savePath + @"\" + name + ".zip";
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }
                ZipFile.CreateFromDirectory(oldPath, zipPath);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Print all material
        /// </summary>
        /// <param name="paper"></param>
        /// <param name="doc"></param>
        /// <param name="path"></param>
        private static void ExportMaterial(Paper paper, Document doc, string path)
        {
            try
            {
                //Write DbScript
                File.WriteAllText(path + @"\00_database-paper-no-" + paper.PaperNo + ".sql", paper.DBScript);
                //Export Requirement
                doc.ShowGrammaticalErrors = false;
                doc.ShowRevisions = false;
                doc.ShowSpellingErrors = false;
                //Opens the word document and fetch each page and converts to image
                ExportImage(path, doc, "paper_No_" + paper.PaperNo);
                //Export First Page
                doc.Close(WdSaveOptions.wdDoNotSaveChanges);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static void ExportImage(string path, Document doc, string name)
        {
            try
            {
                foreach (Window window in doc.Windows)
                {
                    foreach (Pane pane in window.Panes)
                    {
                        for (var i = 1; i <= pane.Pages.Count; i++)
                        {
                            var page = pane.Pages[i];
                            var bits = page.EnhMetaFileBits;


                            using (var ms = new MemoryStream((byte[])(bits)))
                            {
                                var image = Image.FromStream(ms);
                                var jpgPath = Path.ChangeExtension(path + @"/" + name + "-" + i, "jpg");
                                var bitmap = Transparent2Color(image, Color.White);
                                bitmap.Save(jpgPath, ImageFormat.Jpeg);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Replace Transparency by target color
        /// </summary>
        /// <param name="img"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static Bitmap Transparent2Color(Image img, Color target)
        {
            Bitmap bmp2 = new Bitmap(img);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(System.Drawing.Point.Empty, img.Size);
            using (Graphics G = Graphics.FromImage(bmp2))
            {
                G.Clear(target);
                G.DrawImageUnscaledAndClipped(img, rect);
            }
            return bmp2;
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
