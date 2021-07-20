using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class DCM_QUYTAC_NHAYSO : IDCM_
    {
        public long ID { get; set; }
        public string MA_QUYTAC { get; set; } = "SOVANBAN";
        public string SOVANBAN_CODE { get; set; }
        public string TYPE_CODE { get; set; }
    }
}
