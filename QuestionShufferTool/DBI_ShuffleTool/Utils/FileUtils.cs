using System;
using System.IO;
using System.Windows.Forms;
using DBI_ShuffleTool.Properties;

namespace DBI_ShuffleTool.Utils
{
    class FileUtils
    {
        public static String CreateNewDirectory(String path, String nameOfFolder)
        {
            path = path + @"\" + nameOfFolder;
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    return path;
                }

                // Try to create the directory.
                Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(Resources.FileUtils_CreateNewDirectory_The_process_failed___0_, e);
            }
            return path;
        }

        /// <summary>
        /// Choosing a location to save file
        /// </summary>
        /// <returns></returns>
        public static String SaveFileToLocation()
        {
            var fbd = new FolderBrowserDialog();
            // Show the FolderBrowserDialog.
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                return fbd.SelectedPath;
            }
            return "";
        }

        public static String GetFileLocation()
        {
            // Displays an OpenFileDialog so the user can select a File.  
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"Data File|*.dat";
            ofd.Title = @"Select a Data File";
            ofd.Multiselect = false;
            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .DAT file was selected, take the local path of it.  
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                MessageBox.Show(Resources.FileUtils_GetFileLocation_Please_try_again__Something_wrong, Resources.FileUtils_GetFileLocation_Error);
                return null;
            }
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
                    MessageBox.Show(Resources.FileUtils_GetFileLocation_Please_try_again__Something_wrong, Resources.FileUtils_GetFileLocation_Error);
                    return null;
                }
            }
        }
    }
}
