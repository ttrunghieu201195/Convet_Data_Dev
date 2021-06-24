using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    class Import_VanBan_Flow : Import_Abstract
    {
        protected override void ParseData(DataRow row)
        {
            Dcm_Activiti_Log dcm_Activiti_Log = new Dcm_Activiti_Log();
            dcm_Activiti_Log.doc_id = long.Parse(row["id_vanban"].ToString());
        }

        protected override void resetListData()
        {
            throw new NotImplementedException();
        }
    }
}
