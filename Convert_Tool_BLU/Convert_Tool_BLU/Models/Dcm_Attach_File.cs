using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class Dcm_Attach_File : IDCM_
    {
        public Int64 id { get; set; }
        public Int64 id_vb { get; set; }
        public int trang_thai { get; set; } = 0;
        public Int64 file_id { get; set; }
    }
}
