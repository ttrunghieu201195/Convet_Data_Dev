using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class DCM_SOVB_TEMPLATESINHSO
    {
        public long id { get; set; }
        public string SOVANBAN_CODE { get; set; }
        public string TYPE_CODE { get; set; }
        public string TEMPLATE_SINH_SOVB_CODE { get; set; } = @"[stt]/";
    }
}
