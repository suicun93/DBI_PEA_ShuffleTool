using System;
using System.Windows.Forms;
using DBI_ShuffleTool.Model;
using DBI_ShuffleTool.Utils;

namespace DBI_ShuffleTool.UI
{
    public partial class ShuffleTool : Form
    {
        

        public ShuffleTool()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            String location = ImportModel.getLocationFolderOfBank();
            if (location.Equals(ConstantUtils.ERROR_LOAD_FOLDER_FAILED)){ 
                MessageBox.Show(ConstantUtils.ERROR_LOAD_FOLDER_FAILED);
            }
            else
            {
                txtLocationFolderInput.Text = location;
                MessageBox.Show(ConstantUtils.MESSAGE_LOAD_FOLDER_OK);
            }
            
        }

        private void btnCreateTests_Click(object sender, EventArgs e)
        {
            FileUtils.GetSaveLocation();
        }
    }
}
