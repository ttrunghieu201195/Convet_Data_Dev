﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Models
{
    class Dcm_Doc_Relation
    {
        public long id { get; set; }
        public long dcm_id { get; set; }
        public long dcm_document_id { get; set; }
    }
}
