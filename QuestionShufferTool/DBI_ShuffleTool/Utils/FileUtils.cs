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
        public static void SaveFileToLocation()
        {
            // Displays a SaveFileDialog so the user can save the File
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Data File|*.dat";
            sfd.Title = "Save a File";
            sfd.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (sfd.FileName != "")
            {
                
                // Saves the Image via a FileStream created by the OpenFile method.  
                FileStream fs = (System.IO.FileStream)sfd.OpenFile();
                // Saves the File
                
                
            }
        }

        public static String GetFileLocation()
        {
            // Displays an OpenFileDialog so the user can select a File.  
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Data File|*.dat";
            ofd.Title = "Select a Data File";
            ofd.Multiselect = false;
            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .DAT file was selected, take the local path of it.  
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return ofd.FileName;
            }
            return null;
        }

        //Get Folder location by browsing directory
        //Return an uri
        //If something goes wrong, return null
        public static Uri GetFolderLocation()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                //Show dialog select a folder
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //Get Files in folder
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
