using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class Dcm_Track : IDCM_
    {
        public long id { get; set; }
        public long doc_id { get; set; }
        public string schema_id { get; set; }
        public long doc_id_source { get; set; }
        public string schema_id_source { get; set; }
        public DateTime? date_ins { get; set; } = null;
        public string parent { get; set; }
        public string child { get; set; }

    }
}
