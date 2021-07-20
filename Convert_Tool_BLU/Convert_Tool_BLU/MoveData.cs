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
    public partial class MoveData : Form
    {
        public MoveData()
        {
            InitializeComponent();
        }

        private void MoveData_Load(object sender, EventArgs e)
        {
            UpdateComboBox();
        }

        private void UpdateComboBox()
        {
            Dictionary<int, string> services = new Dictionary<int, string>
            {
                {0, "DEV" },
                {1, "BLU" },
            };
            cb_From_Server.DataSource = new BindingSource(services, null);
            cb_From_Server.DisplayMember = "Value";
            cb_From_Server.ValueMember = "Key";
            cb_From_Server.SelectedIndex = 0;

            cb_To_Server.DataSource = new BindingSource(services, null);
            cb_To_Server.DisplayMember = "Value";
            cb_To_Server.ValueMember = "Key";
            cb_To_Server.SelectedIndex = 0;
        }
    }
}
