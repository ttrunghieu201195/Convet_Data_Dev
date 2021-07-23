using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convert_Data
{
    public partial class CleanProgress : Form
    {
        public CleanProgress()
        {
            InitializeComponent();
        }

        private void txt_Progress_TextChanged(object sender, EventArgs e)
        {

        }

        public void setState(string progress)
        {
            txt_clean_Progress.Text = progress;
        }
    }
}
