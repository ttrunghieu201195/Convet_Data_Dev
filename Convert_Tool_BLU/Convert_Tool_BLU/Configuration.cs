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
    public partial class Configuration : Form
    {
        Configs configs;
        public Configuration()
        {
            InitializeComponent();
            //this.configs = configs;
            configs = new Configs();
        }

        private void btn_Save_Conf_Click(object sender, EventArgs e)
        {
            configs.Source_IP = txt_Sour_IP.Text.Trim();
            configs.Source_Port = txt_Sour_Port.Text.Trim();
            configs.Source_User = txt_Sour_User.Text.Trim();
            configs.Source_Pass = txt_Sour_Pass.Text.Trim();
            configs.Source_Service = txt_Sour_Service.Text.Trim();
            configs.Source_Schema = txt_Sour_Schema.Text.Trim();

            configs.Des_IP = txt_Des_IP.Text.Trim();
            configs.Des_Port = txt_Des_Port.Text.Trim();
            configs.Des_User = txt_Des_User.Text.Trim();
            configs.Des_Pass = txt_Des_Pass.Text.Trim();
            configs.Des_Service = txt_Des_Service.Text.Trim();
            configs.Des_Schema = txt_Des_Schema.Text.Trim();
        }

        private void GetConfiguration()
        {
            configs.Source_IP = txt_Sour_IP.Text.Trim();
            configs.Source_Port = txt_Sour_Port.Text.Trim();
            configs.Source_User = txt_Sour_User.Text.Trim();
            configs.Source_Pass = txt_Sour_Pass.Text.Trim();
            configs.Source_Service = txt_Sour_Service.Text.Trim();
            configs.Source_Schema = txt_Sour_Schema.Text.Trim();

            configs.Des_IP = txt_Des_IP.Text.Trim();
            configs.Des_Port = txt_Des_Port.Text.Trim();
            configs.Des_User = txt_Des_User.Text.Trim();
            configs.Des_Pass = txt_Des_Pass.Text.Trim();
            configs.Des_Service = txt_Des_Service.Text.Trim();
            configs.Des_Schema = txt_Des_Schema.Text.Trim();
        }

        private void btn_Convert_Click(object sender, EventArgs e)
        {
            GetConfiguration();
            form_Convert form_Convert = new form_Convert(configs);
            form_Convert.Show();
        }
    }
}
