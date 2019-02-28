using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBI_ShuffleTool.UI
{
    public partial class ProgressBarForm : Form
    {
        public Action Worker { get; set; }
        public ProgressBarForm(Action worker)
        {
            InitializeComponent();
            Worker = worker;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); }, 
            TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
