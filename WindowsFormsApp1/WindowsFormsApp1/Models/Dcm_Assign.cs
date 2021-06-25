using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Dcm_Assign
    {
        public long id { get; set; }
        public long document_id { get; set; }
        public string assignee { get; set; } = "";
        public string assigner { get; set; } = "";
        public DateTime? assigned_date { get; set; } = null;
        public string role_type_code { get; set; } = "";
        public int xu_ly { get; set; }
        public DateTime? ngay_xuly { get; set; } = null;
        public long activiti_log_id { get; set; }
    }
}
