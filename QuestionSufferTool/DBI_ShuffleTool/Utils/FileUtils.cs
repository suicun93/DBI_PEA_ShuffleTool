using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DBI_ShuffleTool.Utils
{
    class FileUtils
    {
        //Get Folder location by browsing directory
        //Return an uri
        //If something goes wrong, return null
        public static Uri GetFolderLocation()
        {
            Uri uri;
            using (var fbd = new FolderBrowserDialog())
            {
                //Show dialog select a folder
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] files = Directory.GetFiles(fbd.SelectedPath);

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                    return new Uri(fbd.SelectedPath);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Please try again! Something wrong", "Error");
                    return null;
                }


            }
            return null;
        }

    
    }
}
