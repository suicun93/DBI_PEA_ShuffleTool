using System;
using DBI_ShuffleTool.Entity.Paper;
using Microsoft.Office.Interop.Word;

namespace DBI_ShuffleTool.Utils.Office
{
    static class DocUtils
    {
        /// <summary>
        /// Setting for doc file
        /// </summary>
        /// <param name="document"></param>
        static public void SettingsPage(Document document)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Saving file
        /// </summary>
        /// <param name="doc">Document want to save</param>
        /// <param name="path"></param>
        /// <param name="ei">ExamForDoc</param>
        static public void SavingDocFile(Document doc, string path, Paper exam)
        {
            try
            {
                doc.SaveAs(path + @"\" + exam.PaperNo, WdSaveFormat.wdFormatDocumentDefault);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Setting header and footer 
        /// </summary>
        /// <param name="examItem"></param>
        /// <param name="section"></param>
        /// <param name="isTest">For adding Paper No</param>
        static public void SettingsHeaderAndFooter(Paper examItem, Document doc)
        {
            try
            {
                foreach (Section wordSection in doc.Sections)
                {
                    Range headerRange = wordSection.Headers[WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Collapse(WdCollapseDirection.wdCollapseEnd);
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    //headerRange.Fields.Add(headerRange, WdFieldType.wdFieldNumPages);

                    //Paragraph p4 = headerRange.Paragraphs.Add();
                    //p4.Range.Text = " of ";
                    headerRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    headerRange.Fields.Add(headerRange, WdFieldType.wdFieldPage);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
