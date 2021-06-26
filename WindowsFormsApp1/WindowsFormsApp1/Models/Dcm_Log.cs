using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    class Dcm_Log
    {
        public long id { get; set; }
        public string username { get; set; }
        public DateTime? date_log { get; set; } = null;
        public long dcm_id { get; set; }
        public int is_read { get; set; }
    }
}
