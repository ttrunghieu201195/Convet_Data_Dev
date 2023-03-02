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
                {2, "Delete Data" },
                {3, "Verify Converted Data" },
                {4, "ImportData" }
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
                    form_Convert form_Convert = new form_Convert();
                    form_Convert.Show();
                    break;
                case 1:
                    // Move Data
                    MoveData configuration = new MoveData();
                    configuration.Show();
                    break;
                case 2:
                    // Delete Data
                    break;
                case 3:
                    CheckingDataForm checkingDataForm = new CheckingDataForm();
                    checkingDataForm.Show();
                    break;
                case 4:
                    ImportData import = new ImportData();
                    import.Show();
                    break;
                default:
                    // Do nothing
                    break;
            }
            Console.WriteLine(cb_Services.SelectedIndex.ToString() + " - " +  cb_Services.SelectedItem.ToString());
        }

        private void TestingForm_Load(object sender, EventArgs e)
        {
            UpdateComboBoxService();
        }
    }
}
