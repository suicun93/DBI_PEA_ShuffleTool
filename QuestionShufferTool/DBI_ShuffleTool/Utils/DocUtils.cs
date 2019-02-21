using System;
using System.Collections.Generic;
using System.Drawing;
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
            foreach(ExamItem ei in examItems)
            {
            Document doc = new Document();
            //Add Section
            Section section = doc.AddSection();
            //Settings Page
            settingPage(doc);

            //Insert Header and Footer of the page
            insertHeaderAndFooter(ei, section);
            
            //Insert Content of the Exam
            foreach(Candidate qc in ei.ExamQuestionsList)
                {
                    appendQuestion(qc, section);
                }

            doc.SaveToFile(path + @"\" + ei.ExamItemCode +".doc", FileFormat.Doc);
            }

            return true;
        }


        /// <summary>
        /// Setting for doc file
        /// </summary>
        /// <param name="document"></param>
        static private void settingPage(Document document)
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
        static private void appendQuestion(Candidate q, Section section)
        {
            Paragraph paraContent = section.AddParagraph();
            paraContent.AppendText("Question " + q.QuestionId + ": ");
            paraContent.AppendText(q.Content);
            paraContent.AppendText("\n");
            Image img = ImageUtils.Base64ToImage(q.ImageData);
            Image tempImg = new Bitmap(img);
            Paragraph paraImage = section.AddParagraph();
            paraImage.Format.HorizontalAlignment = HorizontalAlignment.Center;


           // Image img = Image.FromFile(@"E:\Pic\Mail\test.jpg");


            paraImage.AppendPicture(tempImg);

            paraImage.AppendText("\n");
        }

        /// <summary>
        /// Setting header and footer 
        /// </summary>
        /// <param name="examItem"></param>
        /// <param name="section"></param>
        static private void insertHeaderAndFooter(ExamItem examItem, Section section)
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
            headerParagraph.AppendText("ExamCode: " + examItem.ExamItemCode + "\n");
            headerParagraph.Format.HorizontalAlignment = HorizontalAlignment.Right;
        }
    }
}
