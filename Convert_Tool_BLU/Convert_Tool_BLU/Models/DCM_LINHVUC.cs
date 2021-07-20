using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class DCM_LINHVUC : IDCM_
    {
        public long ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public string NGUOI_TAO { get; set; } = "administrator@vnpt.vn";
        public DateTime NGAY_TAO { get; set; } = DateTime.Now;
        public int STT_HIENTHI { get; set; }
    }
}
