using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DBI_ShuffleTool.Entity;
using Spire.Doc;
using Spire.Doc.Documents;

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
        static public bool ExportDoc(String path, List<ExamItem> examItems)
        {
            foreach (ExamItem ei in examItems)
            {
                Document doc = new Document();
                //Add Section
                Section section = doc.AddSection();
                //Settings Page
                SettingPage(doc);

                //Insert Header and Footer of the page
                InsertHeaderAndFooter(ei, section);

                //Insert Content of the Exam
                for (int i = 0; i < ei.ExamQuestionsList.Count; i++)
                {
                    ei.ExamQuestionsList.ElementAt(i).QuestionId = (i + 1);
                    AppendQuestion(ei.ExamQuestionsList.ElementAt(i), section);
                }

                doc.SaveToFile(path + @"\" + ei.PaperNo + ".doc", FileFormat.Doc);
            }
            return true;
        }


        /// <summary>
        /// Setting for doc file
        /// </summary>
        /// <param name="document"></param>
        static private void SettingPage(Document document)
        {
            //Set Margins
            document.Sections[0].PageSetup.Margins.Top = 30f;
            document.Sections[0].PageSetup.Margins.Bottom = 30f;
            document.Sections[0].PageSetup.Margins.Left = 50f;
            document.Sections[0].PageSetup.Margins.Right = 30f;

            //Set Page Orientation
            document.Sections[0].PageSetup.Orientation = PageOrientation.Portrait;
            //Save and Launch
        }

        /// <summary>
        /// Append Content of Question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="section"></param>
        static private void AppendQuestion(Candidate q, Section section)
        {
            Paragraph paraContent = section.AddParagraph();
            paraContent.AppendText("Question " + q.QuestionId + ": ");
            if (!q.Content.EndsWith(".")) q.Content = String.Concat(q.Content, ".");
            String pointContent = "";
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            pointContent = q.Point == 1 ? string.Concat(pointContent, " (1 point)") :
                String.Concat(pointContent, " (" + q.Point + " points)");

            paraContent.AppendText(String.Concat(q.Content, pointContent));
            paraContent.AppendText("\n");
            if (ImageUtils.Base64ToImage(q.ImageData) != null)
            {
                Image img = ImageUtils.Base64ToImage(q.ImageData);
                Image tempImg = new Bitmap(img);
                Paragraph paraImage = section.AddParagraph();
                paraImage.Format.HorizontalAlignment = HorizontalAlignment.Center;
                paraImage.AppendPicture(tempImg);
            }
            paraContent.AppendText("\n");
        }

        /// <summary>
        /// Setting header and footer 
        /// </summary>
        /// <param name="examItem"></param>
        /// <param name="section"></param>
        static private void InsertHeaderAndFooter(ExamItem examItem, Section section)
        {
            //Adjust the height of headers in the section

            HeaderFooter footer = section.HeadersFooters.Footer;
            HeaderFooter header = section.HeadersFooters.Header;
            Paragraph footerParagraph = footer.AddParagraph();
            Paragraph headerParagraph = header.AddParagraph();
            //Append page number
            footerParagraph.AppendField("page number", FieldType.FieldPage);
            footerParagraph.AppendText(" of ");
            footerParagraph.AppendField("number of pages", FieldType.FieldNumPages);
            footerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Right;

            //Append Exam Code
            headerParagraph.AppendText("ExamCode: " + examItem.PaperNo + "\n");
            headerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Right;
        }
    }
}
