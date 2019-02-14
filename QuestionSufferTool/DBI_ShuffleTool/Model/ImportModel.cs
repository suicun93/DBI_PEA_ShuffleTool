using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBI_ShuffleTool.Utils;
using DBI_ShuffleTool.UI;

namespace DBI_ShuffleTool.Model
{
    class ImportModel
    {
        public static String getLocationFolderOfBank()
        {
            Uri uri = FileUtils.GetFolderLocation();
            if (uri == null)
            {
                return ConstantUtils.ERROR_LOAD_FOLDER_FAILED;
            }
            else
            {
                return uri.LocalPath;
            }
            
        }
    }
}
