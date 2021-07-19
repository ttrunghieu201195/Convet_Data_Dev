namespace Convert_Data
{
    public class Configs
    {
        public string Source_IP { get; set; } = "";
        public string Source_Port { get; set; } = "";
        public string Source_User { get; set; } = "";
        public string Source_Pass { get; set; } = "";
        public string Source_Service { get; set; } = "";
        public string Source_Schema { get; set; } = "";
        public string Des_IP { get; set; } = "";
        public string Des_Port { get; set; } = "";
        public string Des_User { get; set; } = "";
        public string Des_Pass { get; set; } = "";
        public string Des_Service { get; set; } = "";
        public string Des_Schema { get; set; } = "";
        public string Schema { get; set; } = "";
        public string Year { get; set; } = "";
        public int Donvi_lay_du_lieu { get; set; } = -1;
        public bool IsUBND { get; set; } = false;
        public string Old_schema { get; set; } = "";

        public void setConfig(Configs configs)
        {
            Source_IP = configs.Source_IP;
            Source_Port = configs.Source_Port;
            Source_User = configs.Source_User;
            Source_Pass = configs.Source_Pass;
            Source_Service = configs.Source_Service;
            Source_Schema = configs.Source_Schema;
            Des_IP = configs.Des_IP;
            Des_Port = configs.Des_Port;
            Des_User = configs.Des_User;
            Des_Pass = configs.Des_Pass;
            Des_Service = configs.Des_Service;
            Des_Schema = configs.Des_Schema;
        }
    }
}
