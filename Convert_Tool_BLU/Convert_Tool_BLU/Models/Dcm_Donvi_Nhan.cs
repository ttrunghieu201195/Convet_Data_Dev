using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class Dcm_Donvi_Nhan : IDCM_
    {
        public long ID { get; set; }
        public long DOC_ID { get; set; }
        public string XULY_CHINH { get; set; } = "";
        public long AGENT_ID { get; set; }
        public long UNIT_ID { get; set; }
        public string ROLE_TYPE_CODE { get; set; } = "";
        public int DONVI_TRONG_NGOAI { get; set; } = 0;
        public long ACTIVITI_LOG_ID { get; set; }
        public DateTime? ASSIGNED_DATE { get; set; } = null;
        public int XU_LY { get; set; }
        public int? TRUOC_BANHANH { get; set; } = null;
        public int TRANGTHAI_GUI { get; set; } = 1;
    }
}
