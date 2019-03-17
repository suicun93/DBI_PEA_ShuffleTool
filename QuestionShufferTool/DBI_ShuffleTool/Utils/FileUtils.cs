using System;
using System.IO;
using System.Windows.Forms;
using DBI_ShuffleTool.Properties;

namespace DBI_ShuffleTool.Utils
{
    class FileUtils
    {
        public static void DeleteDirectory(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        public static string CreateNewDirectory(string path, string nameOfFolder)
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
            catch
            {
                // ignored
            }
            return path;
        }

        /// <summary>
        /// Choosing a location to save file
        /// </summary>
        /// <returns></returns>
        public static string SaveFileLocation()
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK) return fbd.SelectedPath;
            return null;
        }

        public static string GetFileLocation(string filter, string title)
        {
            // Displays an OpenFileDialog so the user can select a File.  
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = filter,
                Title = title,
                Multiselect = false
            };
            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .DAT file was selected, take the local path of it.  
            if (ofd.ShowDialog() == DialogResult.OK)
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
                    //string[] files = Directory.GetFiles(sfd.SelectedPath);

                    //System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                    return new Uri(fbd.SelectedPath);
                }
                    MessageBox.Show(Resources.FileUtils_GetFileLocation_Please_try_again__Something_wrong, Resources.FileUtils_GetFileLocation_Error);
                    return null;
            }
        }
    }
}
