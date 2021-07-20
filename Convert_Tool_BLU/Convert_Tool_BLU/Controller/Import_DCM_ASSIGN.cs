using Convert_Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data.Controller
{
    class Import_DCM_ASSIGN : Import_Abstract
    {
        private List<Dcm_Assign> dcm_Assigns = new List<Dcm_Assign>();
        public static long SEQ_DCM_ASSIGN;
        public List<Dcm_Assign> GetDcm_Assigns()
        {
            return dcm_Assigns;
        }
        protected override void ParseData(DataRow row)
        {
            throw new NotImplementedException();
        }

        protected override void ParseData<T>(T data, DataTable dcm_type)
        {
            throw new NotImplementedException();
        }

        protected override void ParseData(DataRow row, Common.VB_TYPE type_vb)
        {
            throw new NotImplementedException();
        }

        protected override void resetListData()
        {
            throw new NotImplementedException();
        }

        /*protected override void ParseData<T>(T data)
        {
            throw new NotImplementedException();
        }*/
    }
}
