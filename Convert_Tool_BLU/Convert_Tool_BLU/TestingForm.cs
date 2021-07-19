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
    public partial class TestingForm : Form
    {
        public TestingForm()
        {
            InitializeComponent();
        }

        private void UpdateComboBoxService()
        {
            Dictionary<int, string> services = new Dictionary<int, string>
            {
                {0, "Convert Data" },
                {1, "Move Data" },
                {2, "Delete Data" }
            };
            cb_Services.DataSource = new BindingSource(services, null);
            cb_Services.DisplayMember = "Value";
            cb_Services.ValueMember = "Key";
            cb_Services.SelectedIndex = 0;
        }

        private void btn_StartService_Click(object sender, EventArgs e)
        {
            switch(cb_Services.SelectedIndex)
            {
                case 0:
                    //form_Convert form_Convert = new form_Convert();
                    //form_Convert.Show();
                    Configuration configuration = new Configuration();
                    configuration.Show();
                    break;
                case 1:
                    // Move Data
                    break;
                case 2:
                    // Delete Data
                    break;
                default:
                    // Do nothing
                    break;
            }
            Console.WriteLine(cb_Services.SelectedIndex.ToString() + " - " +  cb_Services.SelectedItem.ToString());
        }
    }
}
