using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class Dcm_SoVanBan
    {
        public long id { get; set; }
        public string name { get; set; }
        public int sobatdau { get; set; } = 1;
        public string ma_loai_so { get; set; }
        public DateTime ngay_batdau_sudung { get; set; }
        public DateTime ngay_dongso { get; set; }
        public string unit_code { get; set; }
        public string code { get; set; }
        public string nguoi_theodoi { get; set; }
        public string nguoi_quantri { get; set; }
        public int stt_hienthi { get; set; }

    }
}
