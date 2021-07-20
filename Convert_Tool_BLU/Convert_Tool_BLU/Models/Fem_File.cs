using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class Fem_File : IDCM_
    {
        public long id { get; set; }
        public int file_type_id { get; set; } = 18;
        public string name { get; set; }
        public string hdd_file { get; set; }
        public string description { get; set; } = "Convert";
        public int file_size { get; set; } = 0;
        public int is_private_file { get; set; } = 0;
        public string creator { get; set; } = "auto_convert";
        public int is_delete { get; set; } = 0;
    }
}
