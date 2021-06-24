using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Constants
    {
        #region postgres conntion
        private static string postgres_host = "10.82.24.106";
        private static string postgres_port = "5432";
        private static string postgres_db = "dms_ubnd_baclieu_release";
        private static string postgres_user = "postgres";
        private static string postgres_pass = "root";
        public static string postgres_connstring = String.Format("Server={0};Port={1};" +
                "User Id={2};Password={3};Database={4};",
                postgres_host, postgres_port, postgres_user,
                postgres_pass, postgres_db);
        #endregion

        #region oracle connection
        private static string oracle_host = "123.31.40.153";
        private static string oracle_port = "1521";
        private static string oracle_service_name = "eofdichvu";
        private static string oracle_user = "CLOUD_ADMIN_DEV_1";
        private static string oracle_pass = "Vnpt6789IT";
        public static string oracle_connstring = String.Format("Data Source=( DESCRIPTION = " +
            "( ADDRESS_LIST = ( ADDRESS = ( PROTOCOL = TCP )( HOST = {0} )(PORT = {1} ) ) )" +
            "(CONNECT_DATA = (SERVER = DEDICATED )(SERVICE_NAME = {2}) ) ); " +
            "User Id = {3}; Password = {4};", oracle_host, oracle_port, oracle_service_name, oracle_user, oracle_pass);
        #endregion oracle connection

        #region query data
        #region postgres query
        #region get thongtin_vb_di from postgres
        public static string sql_thongtin_vb_di = "SELECT a.id, to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  thoigian_tao, u_banhanh.emailaddress nguoi_tao_banhanh"
            +"   , a.summarycontent trich_yeu, case when length(a.booknumber)>0 then a.booknumber else a.code end so_kyhieu"
            +"    , dv_bh.name coquan_banhanh, lvb.\"CODE\" id_hinhthuc"
            +"    , a.emergencyid id_dokhan, lv.\"CODE\" id_linhvuc, a.bookid id_sovanban, to_char(a.issuedate,' DD/MM/YYYY') ngay_banhanh"
            +"    , u_ky.emailaddress nguoi_ky,0 so_trang, 0 so_ban"
            +"    , a.ds_file file_dinhkem"
            +"    , string_agg(CAST(c.documentincomingid as text), ';' ORDER BY c.documentincomingid) id_vblqs"
            +"    , d.donvi_ngoai donvi_nhanngoai"
            +"    , u_tao.emailaddress nguoi_soan, a.secretid id_domat, CAST(dv_st.\"UNIT_ID\" as text) donvi_soanthao"
            +"    , a.office chucvu_nguoiky, a.yeardocument, a.submitnote ghichu_trinhky, a.note ghichu_banhanh, a.note1 ykien_banhanh"
            +"    , a.stateid, case when a.submit = 0 then 1 else 0 end vb_trinh_ky "
            +"    , a.numbersubmit so_trinh_ky"
            +"    , to_char(a.submitdate,' DD/MM/YYYY') ngay_trinh_ky"
            +"    , to_char(a.signdate,' DD/MM/YYYY') ngay_ky"
            +"    , case when a.stateid = 8 then 0 "
            +"        when a.stateid = 4 then 2 "
            +"     when a.stateid = 9 then 2 else 1 end lanhdao_ky "
            +"   , unit_id.\"UNIT_ID\" unit_id"
            +" FROM (select a.*, string_agg(f.filename, ';' ORDER BY f.documentoutgoingid) ds_file"
            +"   FROM \"public\".\"documentoutgoing\" a"
            +"   left join \"public\".\"documentoutgoingattach\" f"
            +"   on a.id = f.documentoutgoingid"
            +"   where a.organizationid = 3528 and a.yeardocument = '2015'"
            +"   group by a.id, f.documentoutgoingid) a"
            +"   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoingbase\" c on  a.id = c.documentoutgoingid"
            +"   left join (select a.id, string_agg(d.organizationname, ',' ORDER BY d.documentid) donvi_ngoai"
            +"        FROM \"public\".\"documentoutgoing\" a"
            +"       , \"public\".\"documentoutgoingdetail\" d"
            + "        where a.organizationid = 3528 and a.yeardocument = '2015' and"
            + "        a.id = d.documentid and d.type=1"
            +"        group by a.id, d.documentid) d"
            +"   on a.id = d.id"
            +"  left join \"public\".\"organization_hrm_unit\" unit_id on unit_id.\"ORGANIZATIONID\" = a.organizationid"
            +"  left join \"public\".\"organization_hrm_unit\" dv_st on dv_st.\"ORGANIZATIONID\" = a.issueorganizationid"
            +"  left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LINHVUC\" lv on lv.\"ID\" = a.fieldid and lv.\"ORGANIZATIONID\"= a.organizationid"
            +"  left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LOAIVANBAN\" lvb on lvb.\"ID\" = a.documenttypeid and lvb.\"ORGANIZATIONID\"= a.organizationid"
            +"   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_tao"
            +"  on  a.writerid = u_tao.userid"
            +"   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_ky"
            +"  on  a.signerid = u_ky.userid"
            +"   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_banhanh"
            +"  on  a.userid = u_banhanh.userid"
            +"   , \"public\".\"organization\" dv_bh"
            + " where a.organizationid = dv_bh.id"
            + " group by a.id, a.createdate, a.writerid, a.summarycontent, a.booknumber, dv_bh.name, lvb.\"CODE\" "
            +"     ,a.emergencyid, lv.\"CODE\", a.bookid, a.issuedate, a.signerid, a.ds_file, a.secretid, dv_st.\"UNIT_ID\", unit_id.\"UNIT_ID\""
            +"     ,a.office, a.yeardocument, a.submitnote, a.note, a.note1, a.stateid, a.submit, a.numbersubmit, a.submitdate, a.signdate,d.donvi_ngoai"
            +"     ,u_banhanh.emailaddress, u_tao.emailaddress, u_ky.emailaddress, a.code;";
        #endregion get thongtin_vb_di from postgres

        #region get thongtin_vb_den from postgres
        public static string sql_thongtin_vb_den = "select a.id,to_char(a.createdate, ' DD/MM/YYYY HH24:MI:SS') thoigian_tao"
            + "     , n.emailaddress nguoi_tao, a.summarycontent trich_yeu, a.booknumber so_den, a.secretid do_mat"
            + "     , a.originalnumber so_kyhieu, b.name coquan_banhanh, lvb.\"CODE\" id_hinhthuc"
            + "         , a.emergencyid id_dokhan,lv.\"CODE\" id_linhvuc, a.bookid id_sovanban"
            + "     , to_char(a.receivedate, ' DD/MM/YYYY') ngay_den"
            + "     , to_char(a.issuedate, ' DD/MM/YYYY') ngay_van_ban"
            + "     , a.signer nguoi_ky, a.office chuc_vu, '' so_trang, '' so_ban"
            + "     , case when a.expiredateleader is not null then to_char(a.expiredateleader, ' DD/MM/YYYY')"
            + "         else to_char(a.expiredateoffice, ' DD/MM/YYYY') end han_xu_ly"
            + "     ,a.ds_file"
            + "     , string_agg(CAST(c.documentoutgoingid as text), ';' ORDER BY c.documentoutgoingid) id_vblqs"
            + "     , a.receivedocumentid, a.issueorganizationid, s.schema, a.note1 ghi_chu, unit_id.\"UNIT_ID\" unit_id"
            + " from (select a.*, string_agg(f.filename, ';' ORDER BY f.documentincomingid) ds_file"
            + "   FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a"
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingattach\" f"
            + "   on a.id = f.documentincomingid"
            + " 	where a.organizationid = 3528 and a.yeardocument = '2015'"
            + "   group by a.id, f.documentincomingid) a"
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoingbase\" c on  a.id = c.documentincomingid"
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" s on cast(a.issueorganizationid as varchar) = s.organizationid"
            + " 	left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LINHVUC\" lv on lv.\"ID\" = a.fieldid and lv.\"ORGANIZATIONID\"=a.organizationid"
            + " 	left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LOAIVANBAN\" lvb on lvb.\"ID\" = a.documenttypeid and lvb.\"ORGANIZATIONID\"=a.organizationid"
            + " 	left join \"public\".\"organization_hrm_unit\" unit_id on unit_id.\"ORGANIZATIONID\" = a.organizationid"
            + "   , \"dms_ubnd_baclieu_release\".\"public\".\"organization\" b "
            + "   , (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) n"
            + " where a.issueorganizationid = b.id and a.userid = n.userid"
            + " group by a.id,a.createdate,n.emailaddress, a.summarycontent,a.booknumber, a.secretid, a.originalnumber,b.name,lvb.\"CODE\""
            + "   ,a.emergencyid,lv.\"CODE\", a.bookid,a.receivedate,a.issuedate,a.signer, a.office, s.schema, unit_id.\"UNIT_ID\""
            + "   ,a.expiredateleader,a.expiredateoffice,a.ds_file, c.documentincomingid,a.receivedocumentid, a.issueorganizationid, a.note1";
        #endregion get thongtin_vb_den from postgres

        #endregion postgres 


        #region oracle query
        #region insert dcm_doc
        public static string sql_insert_dcm_doc = @"INSERT INTO {0}.DCM_DOC(ID,DCMTYPE_CODE,TRICH_YEU,SO_KYHIEU,SO_DEN_DI"
            + ",NGUOI_KY_CHINH,NGUOI_SOAN,SO_VANBAN_CODE,NGAY_TAO,NGAY_VAN_BAN,NGAY_DEN_DI,SO_BAN,SO_TRANG,PRIORITY_CODE,CONFIDENTIAL_CODE"
            + ",LINHVUC_CODE, HINHTHUC_GUI_CODE,HAN_GIAIQUYET,CONGVAN_DENDI,TRANG_THAI,ngay_ban_hanh,donvi_soanthao,DONVI_BANHANH, unit_id"
            + ",DONVI_NHANNGOAI, NGUOI_SOANTHAO, CHUCVU_NGUOIKY, DOC_NOTE, VB_TRINH_KY, SO_TRINH_KY, GHI_CHU, NGAY_TRINH_KY, NGAY_KY, PROCESS_KEY)"
            + " VALUES(:ID,:DCMTYPE_CODE,:TRICH_YEU,:SO_KYHIEU,:SO_DEN_DI,:NGUOI_KY_CHINH,:NGUOI_SOAN,:SO_VANBAN_CODE"
            + ",:NGAY_TAO,:NGAY_VAN_BAN,:NGAY_DEN_DI,:SO_BAN,:SO_TRANG,:PRIORITY_CODE,:CONFIDENTIAL_CODE,:LINHVUC_CODE,:HINHTHUC_GUI_CODE"
            + ",:HAN_GIAIQUYET,:CONGVAN_DENDI,:TRANG_THAI,:ngay_ban_hanh,:donvi_soanthao,:DONVI_BANHANH,:unit_id,:DONVI_NHANNGOAI,:NGUOI_SOANTHAO"
            + ",:CHUCVU_NGUOIKY,:DOC_NOTE,:VB_TRINH_KY,:SO_TRINH_KY,:GHI_CHU,:NGAY_TRINH_KY,:NGAY_KY, :PROCESS_KEY)";
        #endregion insert dcm_doc

        #region sql_insert_dcm_doc_relation
        public static string sql_insert_dcm_doc_relation = @"INSERT INTO {0}.DCM_DOC_RELATION (ID,DCM_ID,DCM_DOCUMENT_ID) "
            + "VALUES (:ID,:DCM_ID,:DCM_DOCUMENT_ID)";
        #endregion sql_insert_dcm_doc_relation

        #region sql_insert_fem_file
        public static string sql_insert_fem_file = @"INSERT INTO {0}.FEM_FILE (ID,FILE_TYPE_ID,NAME,HDD_FILE,DESCRIBE,FILE_SIZE,IS_PRIVATE_FILE,CREATOR,IS_DELETED) "
            + "VALUES (:ID,:FILE_TYPE_ID,:NAME,:HDD_FILE,:DESCRIBE,:FILE_SIZE,:IS_PRIVATE_FILE,:CREATOR,:IS_DELETED)";
        #endregion sql_insert_fem_file

        #region sql_insert_dcm_attach_file
        public static string sql_insert_dcm_attach_file = @"INSERT INTO {0}.DCM_ATTACH_FILE (ATTACHMENT_ID,DOC_ID,TRANG_THAI,FILE_ID) "
            + "VALUES (:ATTACHMENT_ID,:DOC_ID,:TRANG_THAI,:FILE_ID)";
        #endregion sql_insert_dcm_attach_file

        #region sql_insert_dcm_track
        public static string sql_insert_dcm_track = @"INSERT INTO CLOUD_ADMIN.DCM_TRACK (ID,DOC_ID,SCHEMA_ID, DOC_ID_SOURCE, SCHEMA_ID_SOURCE, DATE_INS, PARENT, CHILD) "
            + " VALUES (:ID,:DOC_ID,:SCHEMA_ID,:DOC_ID_SOURCE,:SCHEMA_ID_SOURCE,:DATE_INS,:PARENT,:CHILD)";
        #endregion sql_insert_dcm_track

        #endregion oracle query

        #endregion query data

        string[] vbdi_col_arr = { "id", "thoigian_tao", "nguoi_tao_banhanh", "trich_yeu", "so_kyhieu", "coquan_banhanh", "id_hinhthuc", "id_dokhan", "id_linhvuc", "id_sovanban", "ngay_banhanh", "nguoi_ky", "so_trang", "so_ban", "file_dinhkem", "id_vblqs", "donvi_nhanngoai", "nguoi_soan", "id_domat", "donvi_soanthao", "chucvu_nguoiky", "yeardocument", "ghichu_trinhky", "ghichu_banhanh", "ykien_banhanh", "stateid", "vb_trinh_ky", "so_trinh_ky", "ngay_trinh_ky", "ngay_ky", "lanhdao_ky" };

        public static int INCREASEID_VBDI = 2000000;
        public static long INCREASEID_OTHERS = 20000000;
        
        public static string SEQ_DCM_DOC_RELATION = "DCM_DOC_RELATION_SEQ";
        public static string SEQ_FEM_FILE = "FEM_FILE_SEQ";
        public static string SEQ_DCM_ATTACH_FILE = "DCM_ATTACH_FILE_SEQ";
        public static string SEQ_DCM_TRACK = "DCM_TRACK_SEQ";
    }
}
