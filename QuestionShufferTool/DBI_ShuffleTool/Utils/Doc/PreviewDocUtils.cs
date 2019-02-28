using DBI_ShuffleTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Drawing;

namespace DBI_ShuffleTool.Utils.Doc
{
    class PreviewDocUtils
    {
        public static void PreviewCandidatesSet(List<Question> listQuestions)
        {

            Application wordApp = new Application();
            try
            {
                //Create word file
                wordApp.Visible = true;
                object missing = Missing.Value;
                Document doc = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                //Insert Content of the Exam
                for (int i = 0; i < listQuestions.Count; i++)
                {
                    Section section = (i == 0) ? doc.Sections.First : doc.Sections.Add();
                    for (int j = 0; j < listQuestions.ElementAt(i).Candidates.Count; j++)
                    {
                        AppendSection(listQuestions.ElementAt(i).Candidates.ElementAt(j), section, (i + 1), (j + 1), ref missing);
                    }
                }

                //Settings Page
                DocUtils.SettingsPage(doc);

                //Insert Header and Footer of the page
                DocUtils.SettingsHeaderAndFooter(null, doc);

                ////wordApp.Visible = true;
                //doc.SaveAs(@"D:\\" + "Test", WdSaveFormat.wdFormatDocument97);

            }
            catch (Exception e)
            {
                wordApp = null;
                throw e;
            }

        }

        /// <summary>
        /// Append Content of Question
        /// </summary>
        /// <param name="q"></param>
        /// <param name="section"></param>
        static private void AppendSection(Candidate q, Section section, int questionNumber, int candidateNumber, ref object missing)
        {
            //Insert Title of question
            Paragraph paraTitle = section.Range.Paragraphs.Add(ref missing);
            paraTitle.Range.Text = "Question " + questionNumber + "." + candidateNumber + ":";
            paraTitle.Range.Font.Name = "Arial";
            paraTitle.Range.Font.Bold = 1;
            paraTitle.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
            paraTitle.Range.ParagraphFormat.LeftIndent = 0;
            paraTitle.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paraTitle.Range.InsertParagraphAfter();

            //Insert question requirement
            if (!string.IsNullOrEmpty(q.Content))
            {
                Paragraph paraRequirement = section.Range.Paragraphs.Add(ref missing);
                q.Content = q.Content.Trim();
                //if (!q.Content.EndsWith(".")) q.Content = string.Concat(q.Content, ".");
                paraRequirement.Range.Text = q.Content;
                paraRequirement.Range.Font.Name = "Arial";
                paraRequirement.Range.Font.Bold = 0;
                paraRequirement.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                paraRequirement.Range.Font.Italic = 0;
                paraRequirement.Range.ParagraphFormat.LeftIndent = 0;
                paraRequirement.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                paraRequirement.Range.InsertParagraphAfter();
            }

            //Insert illustration images
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
                    Paragraph paraImage = section.Range.Paragraphs.Add(ref missing);
                    InlineShape pictureShape = paraImage.Range.InlineShapes.AddPicture(imageName);
                    paraImage.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraImage.Range.ParagraphFormat.LeftIndent = 0;

                    Paragraph paraImageDescription = section.Range.Paragraphs.Add(ref missing);
                    paraImageDescription.Range.Text = "Picture " + questionNumber + "." + (++i) + "";
                    paraImageDescription.Format.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    paraImageDescription.Range.Font.Bold = 0;
                    paraImageDescription.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                    paraImageDescription.Range.ParagraphFormat.LeftIndent = 0;
                    paraImageDescription.Range.InsertParagraphAfter();
                    paraImageDescription.Range.InsertParagraphAfter();
                }
            }

            //Insert Solution query
            InsertBlock("Solution: ", q.Solution, true, section, ref missing);

            //Insert Active query
            InsertBlock("Active query: ", q.ActivateQuery, true, section, ref missing);
        }

        private static void InsertBlock(string title, string content, bool isQuery, Section section, ref object missing)
        {
            if (!string.IsNullOrEmpty(content))
            {
                Paragraph paraActiveQueryTitle = section.Range.Paragraphs.Add(ref missing);
                paraActiveQueryTitle.Range.Text = title;
                paraActiveQueryTitle.Range.Font.Name = "Arial";
                paraActiveQueryTitle.Range.Font.Bold = 0;
                paraActiveQueryTitle.Range.Font.Underline = WdUnderline.wdUnderlineSingle;
                paraActiveQueryTitle.Range.Font.Italic = 1;
                paraActiveQueryTitle.Format.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                paraActiveQueryTitle.Range.ParagraphFormat.LeftIndent = 0;
                paraActiveQueryTitle.Range.InsertParagraphAfter();

                content = content.Trim();
                Paragraph paraActiveQueryContent = section.Range.Paragraphs.Add(ref missing);
                if (isQuery)
                {
                    content = SqlUtils.FormatSqlCode(content);
                }
                paraActiveQueryContent.Range.Font.Name = "Arial";
                paraActiveQueryContent.Range.Font.Bold = 0;
                paraActiveQueryContent.Range.Font.Underline = WdUnderline.wdUnderlineNone;
                paraActiveQueryContent.Range.Font.Italic = 0;
                paraActiveQueryContent.Range.ParagraphFormat.LeftIndent = 36;
                paraActiveQueryContent.Range.Text = content;
            }
        }
    }
}
