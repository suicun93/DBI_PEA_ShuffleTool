using System;
using System.Collections.Generic;
using System.Drawing;
using DBI_ShuffleTool.Entity;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Linq;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
//using Spire.Doc;
//using Spire.Doc.Documents;

namespace DBI_ShuffleTool.Utils
{
    class DocUtils
    {
        /// <summary>
        /// Export Doc in path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="examItems"></param>
        /// <returns></returns>
        static public bool ExportDoc(string path, List<ExamForDoc> examItems)
        {
            foreach (ExamForDoc ei in examItems)
            {
                Application wordApp = new Application();
                try
                {
                    //Create word file
                    wordApp.Visible = false;
                    wordApp.ShowAnimation = false;
                    object missing = Missing.Value;
                    Document doc = new Document();

                    //Insert Content of the Exam
                    for (int i = 0; i < ei.ExamQuestionsList.Count; i++)
                    {
                        AppendQuestion(ei.ExamQuestionsList.ElementAt(i), doc, (i + 1), ref missing);
                    }

                    //Settings Page
                    SettingPage(doc);

                    //Insert Header and Footer of the page
                    InsertHeaderAndFooter(ei, doc);

                    //Saving file
                    doc.SaveAs(path + @"\" + ei.PaperNo, WdSaveFormat.wdFormatDocument97);
                    doc.Close();
                }
                catch (Exception e)
                {

                    wordApp = null;
                    MessageBox.Show(e.ToString());
                }
            }
            return true;
        }


        /// <summary>
        /// Setting for doc file
        /// </summary>
        /// <param name="document"></param>
        static private void SettingPage(Document document)
        {
            foreach (Section section in document.Sections)
            {
                section.PageSetup.PaperSize = WdPaperSize.wdPaperA4;
            }

            //1 inch = 72 points

            document.PageSetup.BottomMargin = 72;
            document.PageSetup.TopMargin = 72;
            document.PageSetup.LeftMargin = 72;
            document.PageSetup.RightMargin = 72;

            document.PageSetup.FooterDistance = 36;
            document.PageSetup.HeaderDistance = 36;

            document.PageSetup.Orientation = WdOrientation.wdOrientPortrait;

        }

        /// <summary>
        /// Append Content of Question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="section"></param>
        static private void AppendQuestion(Candidate q, Document doc, int questionNumber, ref object missing)
        {
            //pointContent = q.Point == 1 ? string.Concat(pointContent, " (1 point)") :
            //    string.Concat(pointContent, " (" + q.Point + " points)");
            Paragraph paraQuestionNumber = doc.Content.Paragraphs.Add(ref missing);
            string question = "Question " + questionNumber + ":";
            string questionId = " [" + q.QuestionId + "]";
            paraQuestionNumber.Range.Text = string.Concat(question, questionId);
            Range questionNumberRange = doc.Range(paraQuestionNumber.Range.Start, paraQuestionNumber.Range.Start + question.Length);
            questionNumberRange.Font.Bold = 1;
            questionNumberRange.Font.Underline = WdUnderline.wdUnderlineSingle;
            paraQuestionNumber.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraQuestionNumber.Range.InsertParagraphAfter();

            Paragraph paraContent = doc.Content.Paragraphs.Add(ref missing);
            paraContent.Range.Font.Bold = 0;
            paraContent.Range.Font.Underline = WdUnderline.wdUnderlineNone;
            if (!q.Content.EndsWith(".")) q.Content = string.Concat(q.Content, ".");
            paraContent.Range.Text = q.Content;
            paraContent.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraContent.Range.InsertParagraphAfter();

            List<string> images = q.Images;
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

        /// <summary>
        /// Setting header and footer 
        /// </summary>
        /// <param name="examItem"></param>
        /// <param name="section"></param>
        static private void InsertHeaderAndFooter(ExamForDoc examItem, Document doc)
        {
            foreach (Section wordSection in doc.Sections)
            {
                Range headerRange = wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                headerRange.Collapse(WdCollapseDirection.wdCollapseEnd);

                Paragraph p1 = headerRange.Paragraphs.Add();
                p1.Range.Text = "             Paper No: " + examItem.PaperNo;
                headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                headerRange.Fields.Add(headerRange, WdFieldType.wdFieldNumPages);

                Paragraph p4 = headerRange.Paragraphs.Add();
                p4.Range.Text = " of ";
                headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
            }
        }
    }
}
