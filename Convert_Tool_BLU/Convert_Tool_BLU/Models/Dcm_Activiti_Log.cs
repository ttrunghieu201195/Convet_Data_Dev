using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class Dcm_Activiti_Log : IDCM_
    {
        public long id { get; set; }
        public string task_key { get; set; }
        public DateTime? updated_date { get; set; } = null;
        public string updated_by { get; set; }
        public string action { get; set; }
        public long doc_id { get; set; }
        public string approved { get; set; }
        public string comment_ {get; set; }
        public string comment_full { get; set; }
        public string formid { get; set; } = "formConvert";
        public string action_code { get; set; } = "CHUYEN_THONGBAO";
    }
}
