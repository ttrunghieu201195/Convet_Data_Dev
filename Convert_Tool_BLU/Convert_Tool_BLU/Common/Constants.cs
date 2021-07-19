using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Data
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
                "User Id={2};Password={3};Database={4};Timeout=300;CommandTimeout=900",
                postgres_host, postgres_port, postgres_user,
                postgres_pass, postgres_db);
        #endregion

        #region oracle connection
        private static string oracle_host = "123.31.40.153";
        private static string oracle_port = "1521";
        private static string oracle_service_name = "eofdichvu";
        private static string oracle_user = "CLOUD_ADMIN_DEV_BLU_4";
        private static string oracle_pass = "d3vblu@2020";
        public static string oracle_connstring = String.Format("Data Source=( DESCRIPTION = " +
            "( ADDRESS_LIST = ( ADDRESS = ( PROTOCOL = TCP )( HOST = {0} )(PORT = {1} ) ) )" +
            "(CONNECT_DATA = (SERVER = DEDICATED )(SERVICE_NAME = {2}) ) ); " +
            "User Id = {3}; Password = {4};", oracle_host, oracle_port, oracle_service_name, oracle_user, oracle_pass);
        #endregion oracle connection

        #region
        public static string BLU_CONNSTRING = String.Format("Data Source=( DESCRIPTION = " +
            "( ADDRESS_LIST = ( ADDRESS = ( PROTOCOL = TCP )( HOST = {0} )(PORT = {1} ) ) )" +
            "(CONNECT_DATA = (SERVER = DEDICATED )(SERVICE_NAME = {2}) ) ); " +
            "User Id = {3}; Password = {4};", "10.163.8.36", oracle_port, "eoffice", "EOFFICE_RO", "roeof2021");
        #endregion

        #region query data
        #region postgres query
        #region UBNDTINH
        #region get thongtin_vb_di from postgres
        public static string sql_thongtin_vb_di = "SELECT a.id, to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  thoigian_tao, u_banhanh.emailaddress nguoi_tao_banhanh "
            + " , a.summarycontent trich_yeu, case when length(a.booknumber)>0 then a.booknumber else a.code end so_kyhieu "
            + "    , dv_bh.name coquan_banhanh, lvb.\"CODE\" id_hinhthuc "
            + "    , a.emergencyid id_dokhan, lv.\"CODE\" id_linhvuc, a.bookid id_sovanban, to_char(a.issuedate,' DD/MM/YYYY') ngay_banhanh "
            + "    , u_ky.emailaddress nguoi_ky,0 so_trang, 0 so_ban "
            + "    , a.ds_file file_dinhkem "
            + "    , string_agg(CAST(c.documentincomingid as text), ';' ORDER BY c.documentincomingid) id_vblqs "
            + "    , d.donvi_ngoai donvi_nhanngoai "
            + "    , (case when length(a.fullname)>0 then a.fullname else u_tao.emailaddress end) nguoi_soan, a.secretid id_domat, CAST(dv_st.\"UNIT_ID\" as text) donvi_soanthao "
            + "    , a.office chucvu_nguoiky, a.yeardocument, a.submitnote ghichu_trinhky, a.note ghichu_banhanh, a.note1 ykien_banhanh "
            + "    , a.stateid, case when a.numbersubmit > 0 or a.submitdate is not null then 1 else 0 end vb_trinh_ky  "
            + "    , a.numbersubmit so_trinh_ky "
            + "    , to_char(a.submitdate,' DD/MM/YYYY') ngay_trinh_ky "
            + "    , to_char(a.signdate,' DD/MM/YYYY') ngay_ky "
            + "    , case when a.stateid = 8 then 0  "
            + "        when a.stateid = 4 then 2  "
            + "    when a.stateid = 9 then 2 else 1 end lanhdao_ky  "
            + "  , unit_id.\"UNIT_ID\" unit_id "
            + " FROM (select a.*, string_agg(f.filename, ';' ORDER BY f.documentoutgoingid) ds_file "
            + "   FROM \"public\".\"documentoutgoing\" a "
            + "   left join \"public\".\"documentoutgoingattach\" f "
            + "   on a.id = f.documentoutgoingid "
            + "   where a.organizationid = {0} and a.yeardocument in ({1}) "
            + "   group by a.id, f.documentoutgoingid) a "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoingbase\" c on  a.id = c.documentoutgoingid "
            + "   left join (select a.id, string_agg(d.organizationname, ',' ORDER BY d.documentid) donvi_ngoai "
            + "        FROM \"public\".\"documentoutgoing\" a "
            + " , \"public\".\"documentoutgoingdetail\" d "
            + "        where a.organizationid = {2} and a.yeardocument in ({3})"
            + " and a.id = d.documentid and d.type=1 "
            + "        group by a.id, d.documentid) d "
            + "   on a.id = d.id "
            + " left join \"public\".\"organization_hrm_unit\" unit_id on unit_id.\"ORGANIZATIONID\" = a.organizationid "
            + " left join \"public\".\"organization_hrm_unit\" dv_st on dv_st.\"ORGANIZATIONID\" = a.issueorganizationid "
            + " left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LINHVUC\" lv on lv.\"ID\" = a.fieldid and lv.\"ORGANIZATIONID\"= a.organizationid "
            + " left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LOAIVANBAN\" lvb on lvb.\"ID\" = a.documenttypeid and lvb.\"ORGANIZATIONID\"= a.organizationid "
            + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_tao "
            + " on  a.writerid = u_tao.userid "
            + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_ky "
            + " on  a.signerid = u_ky.userid "
            + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_banhanh "
            + " on  a.userid = u_banhanh.userid "
            + "   left join\"public\".\"organization\" dv_bh "
            + " on a.organizationid = dv_bh.id  "
            + " group by a.id, a.createdate, a.writerid, a.summarycontent, a.booknumber, dv_bh.name, lvb.\"CODE\"  "
            + "     ,a.emergencyid, lv.\"CODE\", a.bookid, a.issuedate, a.signerid, a.ds_file, a.secretid, dv_st.\"UNIT_ID\", unit_id.\"UNIT_ID\" "
            + "     ,a.office, a.yeardocument, a.submitnote, a.note, a.note1, a.stateid, a.submit, a.numbersubmit, a.submitdate, a.signdate,d.donvi_ngoai "
            + "     ,u_banhanh.emailaddress, u_tao.emailaddress, u_ky.emailaddress, a.code";
        #endregion get thongtin_vb_di from postgres

        #region get thongtin_vb_den from postgres
        public static string sql_thongtin_vb_den = "select a.id,to_char(a.createdate, ' DD/MM/YYYY HH24:MI:SS') thoigian_tao "
                + "     , n.emailaddress nguoi_tao, a.summarycontent trich_yeu, a.booknumber so_den, a.secretid do_mat "
                + "     , a.originalnumber so_kyhieu, b.name coquan_banhanh, lvb.\"CODE\" id_hinhthuc "
                + "         , a.emergencyid id_dokhan,lv.\"CODE\" id_linhvuc, a.bookid id_sovanban "
                + "     , to_char(a.receivedate, ' DD/MM/YYYY') ngay_den "
                + "     , to_char(a.issuedate, ' DD/MM/YYYY') ngay_van_ban "
                + "     , a.signer nguoi_ky, a.office chuc_vu, '' so_trang, '' so_ban "
                + "     , case when a.expiredateleader is not null then to_char(a.expiredateleader, ' DD/MM/YYYY') "
                + "         else to_char(a.expiredateoffice, ' DD/MM/YYYY') end han_xu_ly "
                + "     ,a.ds_file "
                + "     , string_agg(CAST(c.documentoutgoingid as text), ';' ORDER BY c.documentoutgoingid) id_vblqs "
                + "     , a.receivedocumentid, a.issueorganizationid "
                + "  , (case when s.schema is not null then s.schema  "
                + "            when a.receivedocumentid > 0 then  "
                + "                    (select ss.schema "
                + "         from   \"public\".\"documentoutgoing\" aa "
                + "                left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" ss on cast(aa.organizationid as varchar) = ss.organizationid "
                + "         where a.receivedocumentid = aa.id "
                + "                    ) end) as schema "
                + "  , a.note1 ghi_chu, unit_id.\"UNIT_ID\" unit_id "
                + " from (select a.*, string_agg(f.filename, ';' ORDER BY f.documentincomingid) ds_file "
                + "   FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
                + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingattach\" f "
                + "   on a.id = f.documentincomingid "
                + "  where a.organizationid = {0} and a.yeardocument in ({1}) "
                + "   group by a.id, f.documentincomingid) a "
                + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoingbase\" c on  a.id = c.documentincomingid "
                + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" s on cast(a.issueorganizationid as varchar) = s.organizationid "
                + "  left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LINHVUC\" lv on lv.\"ID\" = a.fieldid and lv.\"ORGANIZATIONID\"=a.organizationid "
                + "  left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LOAIVANBAN\" lvb on lvb.\"ID\" = a.documenttypeid and lvb.\"ORGANIZATIONID\"=a.organizationid "
                + "  left join \"public\".\"organization_hrm_unit\" unit_id on unit_id.\"ORGANIZATIONID\" = a.organizationid "
                + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"organization\" b on a.issueorganizationid = b.id "
                + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) n on a.userid = n.userid "
                + " group by a.id,a.createdate,n.emailaddress, a.summarycontent,a.booknumber, a.secretid, a.originalnumber,b.name,lvb.\"CODE\" "
                + "   ,a.emergencyid,lv.\"CODE\", a.bookid,a.receivedate,a.issuedate,a.signer, a.office, s.schema, unit_id.\"UNIT_ID\" "
                + "   ,a.expiredateleader,a.expiredateoffice,a.ds_file, c.documentincomingid,a.receivedocumentid, a.issueorganizationid, a.note1";
        #endregion get thongtin_vb_den from postgres

        #region get luong_xuly_vb_di from postgres
        public static string sql_luong_xuly_vb_di = "select 0 STT, a.id id_vanban, da.emailaddress nguoi_gui "
            + " , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + " else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_gui "
            + " , da.emailaddress nguoi_nhan "
            + " , '1' vai_tro_nguoinhan "
            + " , '' donvi_nhan "
            + " , '' vaitro_donvi "
            + " , '' ykien_xuly "
            + " , '' agent_id "
            + " , '' task_key, '' action_tv, '' approved "
            + " , '2' trang_thai_xuly "
            + " , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + " else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_xuly "
            + " , '' trang_thai_xuly_donvi "
            + " , '' thoigian_xuly_donvi "
            + " , '' loai_dv "
            + " , 1 truoc_banhanh "
            + " from \"public\".\"documentoutgoing\" a "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) da "
            + " on da.userid = a.userid  "
            + " where a.organizationid = {0} and a.yeardocument in ({1}) and a.userid > 0  "
            + " union all "
            + " select 1 STT, a.id id_vanban, da.emailaddress nguoi_gui "
            + " , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + " else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_gui "
            + " , string_agg(case when b.userid is not null then CAST(c.emailaddress as text) end, ';' ORDER BY b.id) nguoi_nhan "
            + " , string_agg(case when b.userid is not null then '0' end, ';' ORDER BY b.id) vai_tro_nguoinhan "
            + " , string_agg(case when b.organizationid is not null and b.type in (0,3,4) and dd.\"UNIT_ID\" is null then '' "
            + "   when b.organizationid is not null and b.type in (0,3,4) then cast(dd.\"UNIT_ID\" as text) end, ';' ORDER BY b.id) donvi_nhan "
            + " , string_agg(case when b.organizationid is not null and b.type in (0,4,3) then '1' end, ';' ORDER BY b.id) vaitro_donvi "
            + " , a.note1 ykien_xuly "
            + " ,  string_agg(case when b.organizationid is not null and b.type=4 then '-1' "
            + "    when b.organizationid is not null and (b.type=0 or b.type is null) then '0' "
            + "    when b.organizationid is not null and b.type=3 and dd.\"SCHEMA_ID\" is null then '' "
            + "    when b.organizationid is not null and b.type=3 then CAST(dd.\"SCHEMA_ID\" as text) end, ';' ORDER BY b.id) agent_id "
            + " , 'end' task_key, 'Ban hành' action_tv, 'VT_BANHANH' approved "
            + " , string_agg(case when b.userid is not null and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + " when b.userid is not null and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + " when b.userid is not null and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) trang_thai_xuly "
            + " , string_agg(case when b.userid is not null and b.reviewdatenote is null then '' "
            + " when b.userid is not null and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly "
            + " , string_agg(case when b.organizationid is not null and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + " when b.organizationid is not null and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + " when b.organizationid is not null and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) trang_thai_xuly_donvi "
            + " , string_agg(case when b.organizationid is not null and b.reviewdatenote is null then '' "
            + " when b.organizationid is not null and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly_donvi "
            + " , string_agg(case when b.organizationid is not null then CAST(b.type as text) end, ';' ORDER BY b.id) loai_dv "
            + " , 0 truoc_banhanh "
            + " from \"public\".\"documentoutgoing\" a "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) da "
            + " on da.userid = a.userid  "
            + " , \"public\".\"documentoutgoingdetail\" b  "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + " on b.userid = c.userid "
            + " left join \"public\".\"organization_hrm_unit\" dd "
            + " on b.organizationid = dd.\"ORGANIZATIONID\"  "
            + " where a.organizationid = {2} and a.yeardocument in ({3}) "
            + " and a.id = b.documentid "
            + " and b.type != 1 "
            + " group by a.id, da.emailaddress "
            + " union ALL "
            + " select 2 STT, a.id id_vanban, da.emailaddress nguoi_gui "
            + " , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + " else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_gui "
            + " , '' nguoi_nhan "
            + " , '3' vai_tro_nguoinhan "
            + " , '' donvi_nhan "
            + " , '' vaitro_donvi "
            + " , a.note1 ykien_xuly "
            + " , '' agent_id "
            + " , '' task_key, 'Nhập tắt' action_tv, 'GUI_YKIEN' approved "
            + " , '2' trang_thai_xuly "
            + " , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + " else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_xuly "
            + " , '' trang_thai_xuly_donvi "
            + " , '' thoigian_xuly_donvi "
            + " , '' loai_dv "
            + " , 0 truoc_banhanh "
            + " from \"public\".\"documentoutgoing\" a "
            + " ,(select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) da "
            + " where length(\"a\".note1)>0 and a.userid = da.userid "
            + " and a.organizationid = {4} and a.yeardocument in ({5}) "
            + " and not exists(select 1 from \"public\".\"documentoutgoingdetail\" b where a.id = b.documentid) "
            + " order by id_vanban, stt, thoigian_gui";
        #endregion

        #region get luong_xuly_vb_den from postgres
        public static string sql_luong_xuly_vb_den = @"select a.stt, a.id_vanban, c.emailaddress nguoi_gui, a.thoigian_gui, a.nguoi_nhan, a.vai_tro vai_tro_nguoinhan "
            + "       , a.donvi_nhan, a.vaitro_donvi, a.ykien_xuly, 0 agent_id, a.task_key, a. action_tv, a.approved "
            + "       , a.trang_thai_xuly, a.thoigian_xuly, a.trang_thai_xuly_donvi, a.thoigian_xuly_donvi, a.loai_dv "
            + " from ( "
            + " select a.stt, a.id_vanban, a.nguoi_gui, a.thoigian_gui, d.emailaddress nguoi_nhan, a.donvi_nhan, a.vai_tro "
            + "    , a.ykien_xuly, a.vaitro_donvi, 0 agent_id, a.task_key, a. action_tv, a.approved "
            + "    , a.trang_thai_xuly, a.thoigian_xuly, a.trang_thai_xuly_donvi, a.thoigian_xuly_donvi, a.loai_dv "
            + " from ( "
            + " select 0 stt, a.id id_vanban, CAST(userid as text)  nguoi_gui "
            + "    , to_char(CASE WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            ELSE a.createdate END, ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "   , '' nguoi_nhan, '' donvi_nhan "
            + "    , '3' vai_tro "
            + "   , ('Ý kiến đề xuất: '|| COALESCE(idea,'') || E'\r\nÝ kiến CVP: '||COALESCE(idea1,'')||E'\r\nÝ kiến Thường trực: '||COALESCE(leadernote,'')) as  ykien_xuly "
            + "    , '' vaitro_donvi, 0 agent_id, '' task_key, 'Xin ý kiến'  action_tv, 'GUI_YKIEN' approved "
            + "    , '2' trang_thai_xuly "
            + "    , to_char(CASE WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            ELSE a.createdate END + (5 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi, '' thoigian_xuly_donvi, '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + " where a.yeardocument in ({0}) and a.organizationid = {1} and (processleaderid=0 or processleaderid is null) and (leaderid=0 or leaderid is null) "
            + " and (length(idea)>0 or length(idea1)>0 or length(leadernote)>0) "
            + " union all "
            + " select 1 stt, a.id id_vanban, CAST(userid as text)  nguoi_gui "
            + "    , to_char(CASE WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            ELSE a.createdate END, ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , CAST(processleaderid as text) nguoi_nhan, '' donvi_nhan "
            + "    , '2' vai_tro, idea ykien_xuly, '' vaitro_donvi, 0 agent_id "
            + "    , 'USE_2' task_key, 'Trình Chánh văn phòng'  action_tv, 'VT_TRINH_LDDV' approved "
            + "    , '2' trang_thai_xuly "
            + "    , to_char(CASE WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            ELSE a.createdate END + (5 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi, '' thoigian_xuly_donvi, '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + " where a.yeardocument in ({2}) and a.organizationid = {3} and processleaderid > 0 "
            + " union  ALL "
            + " select 2 stt, a.id id_vanban, CAST(processleaderid as text) nguoi_gui "
            + "    , to_char(CASE WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            ELSE a.createdate END + (5 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , CAST(userid as text) nguoi_nhan, '' donvi_nhan "
            + "    , '2' vai_tro, idea1 ykien_xuly, '' vaitro_donvi, 0 agent_id "
            + "    , 'USE_1' task_key, 'Chuyển Văn thư'  action_tv, 'LDDV_CHUYEN_VT' approved "
            + "     , '2' trang_thai_xuly "
            + "    , to_char(CASE WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            ELSE a.createdate END + (10 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi, '' thoigian_xuly_donvi, '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + " where a.yeardocument in ({4}) and a.organizationid = {5} and processleaderid > 0 "
            + " union ALL "
            + " select 3 stt, a.id id_vanban, CAST(userid as text) nguoi_gui "
            + "    , to_char(CASE WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            ELSE a.createdate END + (10 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , CAST(leaderid as text) nguoi_nhan, '' donvi_nhan "
            + "    , '2' vai_tro, case when idea1 is not null then idea1 else idea end ykien_xuly, '' vaitro_donvi, 0 agent_id "
            + "    , 'USE_2' task_key, 'Trình Lãnh đạo'  action_tv, 'VT_TRINH_LDDV' approved "
            + "    , '2' trang_thai_xuly "
            + "    , to_char(CASE WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            ELSE a.createdate END + (15 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi, '' thoigian_xuly_donvi, '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + " where a.yeardocument in ({6}) and a.organizationid = {7} and leaderid > 0 "
            + " union  ALL "
            + " select 4 stt, a.id id_vanban,CAST(leaderid as text) nguoi_gui "
            + "    , to_char(CASE WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            ELSE a.createdate END + (15 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , CAST(userid as text) nguoi_nhan, '' donvi_nhan "
            + "    , '2' vai_tro "
            + "    , case when leadernote is not null then leadernote  "
            + "        when processleaderid = 0 or processleaderid is null then idea1 end ykien_xuly "
            + "    , '' vaitro_donvi, 0 agent_id "
            + "    , 'USE_1' task_key, 'Chuyển Văn thư'  action_tv, 'LDDV_CHUYEN_VT' approved "
            + "    , '2' trang_thai_xuly "
            + "    , to_char(CASE WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            ELSE a.createdate END + (20 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi, '' thoigian_xuly_donvi, '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + " where a.yeardocument in ({8}) and a.organizationid = {9} and leaderid > 0) a "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) d on a.nguoi_nhan = CAST(d.userid as text) "
            + " union all  "
            + " select 5 stt, a.id id_vanban, CAST(userid as text) nguoi_gui "
            + "    , to_char(CASE WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            ELSE a.createdate END + (20 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , (SELECT string_agg(CAST(c.emailaddress as text) , ';' ORDER BY b.id) "
            + "      FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "         ,(select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + "      where a.id = b.documentid and c.userid = b.userid and b.assignuserid is null "
            + "      group by b.documentid) nguoi_nhan "
            + "    , (SELECT string_agg(CAST(d.\"UNIT_ID\" as text) , ';' ORDER BY b.id) "
            + "      FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "        left join \"public\".\"organization_hrm_unit\" d on b.organizationid = d.\"ORGANIZATIONID\" "
            + "      where a.id = b.documentid and b.assignuserid is null  "
            + "      group by b.documentid) donvi_nhan "
            + "    , (SELECT string_agg(case when (b.mainprocess=0 or b.mainprocess is null) and b.userid is not null then '0'  "
            + "             when b.mainprocess=1 and b.userid is not null then '2' "
            + "             when b.mainprocess=2 and b.userid is not null then '1'  end, ';' ORDER BY b.id) "
            + "      FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "      where a.id = b.documentid and b.assignuserid is null "
            + "      group by b.documentid) vai_tro "
            + "    , '' ykien_xuly "
            + "    , (SELECT string_agg(case when (b.mainprocess=0 or b.mainprocess is null) and b.organizationid is not null then '0'  "
            + "             when b.mainprocess=1 and b.organizationid is not null then '1' "
            + "             when b.mainprocess=2 and b.organizationid is not null then '2' end, ';' ORDER BY b.id) "
            + "      FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "      where a.id = b.documentid and b.assignuserid is null "
            + "      group by b.documentid) vaitro_donvi "
            + "    , 0 agent_id, 'end' task_key, 'Chuyển xử lý'  action_tv, 'VT_BANHANH' approved "
            + "    , (SELECT string_agg(case when b.userid is not null and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + "                 when b.userid is not null and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + "                 when b.userid is not null and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) "
            + "      FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "      where a.id = b.documentid and b.assignuserid is null "
            + "      group by b.documentid) trang_thai_xuly "
            + "    , (SELECT string_agg(case when b.userid is not null and b.reviewdatenote is null then '' "
            + "                 when b.userid is not null and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) "
            + "     FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "      where a.id = b.documentid and b.assignuserid is null "
            + "      group by b.documentid) thoigian_xuly "
            + "    , (SELECT string_agg(case  when b.organizationid is not null and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + "         when b.organizationid is not null and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + "         when b.organizationid is not null and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) "
            + "     FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "      where a.id = b.documentid and b.assignuserid is null "
            + "      group by b.documentid) trang_thai_xuly_donvi "
            + "     , (SELECT string_agg(case when b.organizationid is not null and b.reviewdatenote is null then '' "
            + "         when b.organizationid is not null and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) "
            + "     FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "      where a.id = b.documentid and b.assignuserid is null "
            + "      group by b.documentid) thoigian_xuly_donvi "
            + "    , (SELECT string_agg(case  when b.organizationid is not null then '0' end, ';' ORDER BY b.id) "
            + "     FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "      where a.id = b.documentid and b.assignuserid is null "
            + "      group by b.documentid) loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + " where a.yeardocument in ({10}) and a.organizationid = {11} "
            + " and exists(select 1 from \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b where a.id = b.documentid and b.assignuserid is null) "
            + " union all "
            + " select 6 stt, a.id id_vanban, CAST(b.assignuserid as text) nguoi_gui "
            + "    , to_char(min(b.assigndate), ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , string_agg(c.emailaddress, ';' ORDER BY b.documentid) nguoi_nhan "
            + "    , string_agg(CAST(d.\"UNIT_ID\" as text) , ';' ORDER BY b.documentid) donvi_nhan "
            + "    , string_agg(case when (b.mainprocess=0 or b.mainprocess is null) and b.userid is not null then '0'  "
            + "             when b.mainprocess=1 and b.userid is not null then '2' "
            + "             when b.mainprocess=2 and b.userid is not null then '1' end, ';' ORDER BY b.id) vai_tro "
            + "    , '' ykien_xuly "
            + "    , string_agg(case when (b.mainprocess=0 or b.mainprocess is null) and b.organizationid is not null then '0'  "
            + "             when b.mainprocess=1 and b.organizationid is not null then '1' "
            + "             when b.mainprocess=2 and b.organizationid is not null then '2' end, ';' ORDER BY b.id) vaitro_donvi "
            + "    , 0 agent_id, '' task_key, 'Chuyển xử lý'  action_tv, '' approved "
            + "    , string_agg(case when b.userid is not null and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + "             when b.userid is not null and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + "             when b.userid is not null and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) trang_thai_xuly "
            + "     , string_agg(case when b.userid is not null and b.reviewdatenote is null then '' "
            + "             when b.userid is not null and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly "
            + "    , string_agg(case  when b.organizationid is not null and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + "         when b.organizationid is not null and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + "         when b.organizationid is not null and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) trang_thai_xuly_donvi "
            + "    , string_agg(case when b.organizationid is not null and b.reviewdatenote is null then '' "
            + "         when b.organizationid is not null and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly_donvi "
            + "    , string_agg(case  when b.organizationid is not null then '0' end, ';' ORDER BY b.id) loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + "      ,\"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + "     on c.userid = b.userid "
            + "   left join \"public\".\"organization_hrm_unit\" d "
            + "    on b.organizationid = d.\"ORGANIZATIONID\"  "
            + " where a.yeardocument in ({12}) and a.organizationid = {13}  "
            + "   and a.id = b.documentid and b.assignuserid is not null "
            + " group by a.id,b.documentid, b.assignuserid) a "
            + "    ,(select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + " where a.nguoi_gui = CAST(c.userid as text) "
            + " union all  "
            + " select 7 stt, a.id id_vanban "
            + "    , case when b.userid is null then b.organizationname else d.emailaddress end nguoi_gui "
            + "    , to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , '' nguoi_nhan "
            + "    , case when b.userid is null then '' else '3' end vai_tro_nguoinhan "
            + "    , '' donvi_nhan "
            + "    , case when b.userid is null then '3' else '' end vaitro_donvi "
            + "    , b.note ykien_xuly "
            + "    , 0 agent_id, '' task_key, 'Gửi ý kiến'  action_tv, 'GUI_YKIEN' approved "
            + "    , '2' trang_thai_xuly "
            + "    , to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi, '' thoigian_xuly_donvi, '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + "      ,\"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) d "
            + "    on b.userid = d.userid "
            + " where a.yeardocument in ({14}) and a.organizationid = {15} "
            + "   and a.id = b.documentid and b.reviewdatenote is not null "
            + " order by id_vanban, stt";
        #endregion get luong_xuly_vb_den from postgres

        #region sql_log_xuly_vb_den
        public static string sql_log_xuly_vb_den = "SELECT a.id stt, b.id id_vanban, c.emailaddress nguoi_xu_ly, CAST(d.\"UNIT_ID\" as text) donvi_xuly "
            + " , case when (a.viewdocument=0 or a.viewdocument is null) then 0  "
            + "    when a.viewdocument=1 and a.reviewdatenote is null then 1 "
            + "  else 2 end trang_thai "
            + " , case when a.createdatenote is not null then to_char(a.createdatenote, ' DD/MM/YYYY HH24:MI:SS')  "
            + "  else '' end thoi_gian "
            + " FROM \"public\".\"documentincomingdetail\" a "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + " on a.userid = c.userid "
            + " left join \"public\".\"organization_hrm_unit\" d "
            + " on a.organizationid = d.\"ORGANIZATIONID\"  "
            + " , \"public\".\"documentincoming\" b "
            + " where a.documentid = b.id  "
            + " and b.organizationid = {0} and b.yeardocument in ({1}) "
            + " order by id_vanban";
        #endregion sql_log_xuly_vb_den

        #region sql_log_xuly_vb_di
        public static string sql_log_xuly_vb_di = "SELECT a.id stt, b.id id_vanban, c.emailaddress nguoi_xu_ly "
            + " , (case when a.type=4 then CAST(b.organizationid as text) "
            + "  when a.type in (0,3) then CAST(d.\"UNIT_ID\" as text) end) donvi_xuly "
            + " , case when (a.viewdocument=0 or a.viewdocument is null) then 0  "
            + "    when a.viewdocument=1 and a.reviewdatenote is null then 1 "
            + "  else 2 end trang_thai "
            + " , case when a.createdatenote is not null then to_char(a.createdatenote, ' DD/MM/YYYY HH24:MI:SS')  "
            + "  else '' end thoi_gian "
            + " FROM \"public\".\"documentoutgoingdetail\" a "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + " on a.userid = c.userid "
            + " left join \"public\".\"organization_hrm_unit\" d "
            + " on a.organizationid = d.\"ORGANIZATIONID\"  "
            + " , \"public\".\"documentoutgoing\" b "
            + " where a.documentid = b.id and a.type != 1 "
            + " and b.organizationid = {0} and b.yeardocument in ({1}) and b.id in (69510)"
            + " order by id_vanban";
        #endregion sql_log_xuly_vb_di
        #endregion UBNDTINH

        #region DONVI_SO
        #region sql_so_thongtin_vbdi_from_postgres
        public static string sql_so_thongtin_vbdi = "SELECT a.id, to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  thoigian_tao, u_banhanh.emailaddress nguoi_tao_banhanh "
            + "   , a.summarycontent trich_yeu, case when length(a.booknumber)>0 then a.booknumber else a.code end so_kyhieu "
            + "    , dv_bh.name coquan_banhanh, lvb.\"CODE\" id_hinhthuc "
            + "    , a.emergencyid id_dokhan, lv.\"CODE\" id_linhvuc, a.bookid id_sovanban, to_char(a.issuedate,' DD/MM/YYYY') ngay_banhanh "
            + "    , u_ky.emailaddress nguoi_ky,0 so_trang, 0 so_ban "
            + "    , a.ds_file file_dinhkem "
            + "    , string_agg(CAST(c.documentincomingid as text), ';' ORDER BY c.documentincomingid) id_vblqs "
            + "    , d.donvi_ngoai donvi_nhanngoai "
            + "    , (case when length(a.fullname)>0 then a.fullname else u_tao.emailaddress end) nguoi_soan, a.secretid id_domat, CAST(dv_st.\"UNIT_ID\" as text) donvi_soanthao "
            + "    , a.office chucvu_nguoiky, a.yeardocument, a.submitnote ghichu_trinhky, a.note ghichu_banhanh, a.note1 ykien_banhanh "
            + "    , a.stateid, case when a.numbersubmit > 0 or a.submitdate is not null then 1 else 0 end vb_trinh_ky  "
            + "    , a.numbersubmit so_trinh_ky "
            + "    , to_char(a.submitdate,' DD/MM/YYYY') ngay_trinh_ky "
            + "    , to_char(a.signdate,' DD/MM/YYYY') ngay_ky "
            + "    , case when a.stateid = 8 then 0  "
            + "        when a.stateid = 4 then 2  "
            + "     when a.stateid = 9 then 2 else 1 end lanhdao_ky  "
            + "   , unit_id.\"UNIT_ID\" unit_id "
            + " FROM (select a.*, string_agg(replace(sb.schema,'.','@')||f.filename, ';' ORDER BY f.documentoutgoingid) ds_file "
            + "   FROM \"public\".\"documentoutgoing\" a "
            + "   left join \"public\".\"documentoutgoingattach\" f "
            + "   on a.id = f.documentoutgoingid "
            + " 	left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" sb on CAST(a.organizationid as text)= sb.organizationid "
            + "   where a.organizationid in ({0}) and a.yeardocument in ({1}) "
            + "   group by a.id, f.documentoutgoingid) a "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoingbase\" c on  a.id = c.documentoutgoingid "
            + "   left join (select a.id, string_agg(d.organizationname, ',' ORDER BY d.documentid) donvi_ngoai "
            + "        FROM \"public\".\"documentoutgoing\" a "
            + "       , \"public\".\"documentoutgoingdetail\" d "
            + "        where a.organizationid in ({2}) and a.yeardocument in ({3}) "
            + "       and a.id = d.documentid and d.type=1 "
            + "        group by a.id, d.documentid) d "
            + "   on a.id = d.id "
            + "  left join \"public\".\"organization_hrm_unit\" unit_id on unit_id.\"ORGANIZATIONID\" = a.organizationid "
            + "  left join \"public\".\"organization_hrm_unit\" dv_st on dv_st.\"ORGANIZATIONID\" = a.issueorganizationid "
            + "  left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LINHVUC\" lv on lv.\"ID\" = a.fieldid and lv.\"ORGANIZATIONID\"= a.organizationid "
            + "  left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LOAIVANBAN\" lvb on lvb.\"ID\" = a.documenttypeid and lvb.\"ORGANIZATIONID\"= a.organizationid "
            + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_tao "
            + "  on  a.writerid = u_tao.userid "
            + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_ky "
            + "  on  a.signerid = u_ky.userid "
            + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) u_banhanh "
            + "  on  a.userid = u_banhanh.userid "
            + "   left join\"public\".\"organization\" dv_bh "
            + "  on a.organizationid = dv_bh.id  "
            + " group by a.id, a.createdate, a.writerid, a.summarycontent, a.booknumber, dv_bh.name, lvb.\"CODE\"  "
            + "     ,a.emergencyid, lv.\"CODE\", a.bookid, a.issuedate, a.signerid, a.ds_file, a.secretid, dv_st.\"UNIT_ID\", unit_id.\"UNIT_ID\" "
            + "     ,a.office, a.yeardocument, a.submitnote, a.note, a.note1, a.stateid, a.submit, a.numbersubmit, a.submitdate, a.signdate,d.donvi_ngoai "
            + "     ,u_banhanh.emailaddress, u_tao.emailaddress, u_ky.emailaddress, a.code,a.fullname";
        #endregion sql_so_thongtin_vbdi_from_postgres

        #region sql_so_thongtin_vbden_from_postgres
        public static string sql_so_thongtin_vbden = "select a.id,to_char(a.createdate, ' DD/MM/YYYY HH24:MI:SS') thoigian_tao "
            + "     , n.emailaddress nguoi_tao, a.summarycontent trich_yeu, a.booknumber so_den, a.secretid do_mat "
            + "     , a.originalnumber so_kyhieu, b.name coquan_banhanh, lvb.\"CODE\" id_hinhthuc "
            + "                 , a.emergencyid id_dokhan, lv.\"CODE\" id_linhvuc, a.bookid id_sovanban "
            + "     , to_char(a.receivedate, ' DD/MM/YYYY') ngay_den "
            + "     , to_char(a.issuedate, ' DD/MM/YYYY') ngay_van_ban "
            + "     , a.signer nguoi_ky, a.office chuc_vu, '' so_trang, '' so_ban "
            + "     , case when a.expiredateleader is not null then to_char(a.expiredateleader, ' DD/MM/YYYY') "
            + "         else to_char(a.expiredateoffice, ' DD/MM/YYYY') end han_xu_ly "
            + "     , case when length(a.ds_file1)>0 and length(a.ds_file2)>0 then a.ds_file1 || ';' || a.ds_file2 "
            + "         when length(a.ds_file1)>0 then a.ds_file1 "
            + "                when length(a.ds_file2)>0 then a.ds_file2 "
            + "         else '' end ds_file "
            + "     , string_agg(CAST(c.documentoutgoingid as text), ';' ORDER BY c.documentoutgoingid) id_vblqs "
            + "     ,a.receivedocumentid, a.issueorganizationid, (case when s.schema is not null then s.schema  "
            + "                 when a.receivedocumentid > 0 then  "
            + "                    (select ss.schema "
            + "         from   \"public\".\"documentoutgoing\" aa "
            + "                left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" ss on cast(aa.organizationid as varchar) = ss.organizationid "
            + "         where a.receivedocumentid = aa.id "
            + "                    ) end) as schema "
            + "     , a.note1 ghi_chu, unit_id.\"UNIT_ID\" unit_id "
            + " from (select a.*, string_agg(replace(sb.schema,'.','@')||f.filename, ';' ORDER BY f.documentincomingid) ds_file1 "
            + "         , (case when a.organizationid in (19329,63) then ''  "
            + "          else string_agg(replace(sa.schema,'.','@')||o.filename, ';' ORDER BY o.documentoutgoingid) end) ds_file2 "
            + "   FROM \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentincomingattach\" f "
            + "   on a.id = f.documentincomingid "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoingattach\" o "
            + "   on a.receivedocumentid = o.documentoutgoingid "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoing\" oa on a.receivedocumentid = oa.id "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" sa on CAST(oa.organizationid as text)= sa.organizationid "
            + "  left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" sb on CAST(a.organizationid as text)= sb.organizationid "
            + "   where a.organizationid in ({0}) and a.yeardocument in ({1}) "
            + "   group by a.id, f.documentincomingid) a "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"documentoutgoingbase\" c on  a.id = c.documentincomingid "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"organization_schema\" s on cast(a.issueorganizationid as varchar) = s.organizationid "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LINHVUC\" lv on lv.\"ID\" = a.fieldid and lv.\"ORGANIZATIONID\"=a.organizationid "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"DM_LOAIVANBAN\" lvb on lvb.\"ID\" = a.documenttypeid and lvb.\"ORGANIZATIONID\"=a.organizationid "
            + "   left join \"public\".\"organization_hrm_unit\" unit_id on unit_id.\"ORGANIZATIONID\" = a.organizationid "
            + "   left join \"dms_ubnd_baclieu_release\".\"public\".\"organization\" b  on a.issueorganizationid = b.id "
            + "   left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) n on a.userid = n.userid "
            + " group by a.id,a.createdate,n.emailaddress, a.summarycontent,a.booknumber, a.secretid, a.originalnumber,b.name,lvb.\"CODE\" "
            + "   ,a.emergencyid,lv.\"CODE\", a.bookid,a.receivedate,a.issuedate,a.signer, a.office, s.schema, unit_id.\"UNIT_ID\" "
            + "   ,a.expiredateleader,a.expiredateoffice,a.ds_file1,a.ds_file2, c.documentincomingid,a.receivedocumentid, a.issueorganizationid, a.note1";
        #endregion sql_so_thongtin_vbden_from_postgres

        #region sql_so_luong_xuly_vbdi_from_postgres
        public static string sql_so_luong_xuly_vbdi = "select 0 STT, a.id id_vanban, da.emailaddress nguoi_gui "
            + "      , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + "          else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_gui "
            + "      , da.emailaddress nguoi_nhan "
            + "      , '1' vai_tro_nguoinhan "
            + "      , '' donvi_nhan "
            + "      , '' vaitro_donvi "
            + "      , '' ykien_xuly "
            + "      , '' agent_id "
            + "      , '' task_key, '' action_tv, '' approved "
            + "      , '2' trang_thai_xuly "
            + "      , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + "          else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_xuly "
            + "      , '' trang_thai_xuly_donvi "
            + "      , '' thoigian_xuly_donvi "
            + "      , '' loai_dv "
            + "      , 1 truoc_banhanh "
            + " from \"public\".\"documentoutgoing\" a "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) da "
            + "    on da.userid = a.userid  "
            + " where a.organizationid in ({0}) and a.yeardocument in ({1}) and a.userid > 0  "
            + " union all "
            + " select 1 STT, a.id id_vanban, da.emailaddress nguoi_gui "
            + "      , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + "          else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_gui "
            + "      , string_agg(case when b.userid>0 and (b.organizationid is null or b.organizationid=0) then CAST(c.emailaddress as text) end, ';' ORDER BY b.id) nguoi_nhan "
            + "      , string_agg(case when b.userid>0 and (b.organizationid is null or b.organizationid=0)  then '0' end, ';' ORDER BY b.id) vai_tro_nguoinhan "
            + "      , string_agg(case when b.organizationid>0 and b.type in (0,3,4) and dd.\"UNIT_ID\" is null then '' "
            + "            when b.organizationid >0 and b.type in (0,3,4) then cast(dd.\"UNIT_ID\" as text) end, ';' ORDER BY b.id) donvi_nhan "
            + "      , string_agg(case when b.organizationid>0 and b.type in (0,4,3) then '1' end, ';' ORDER BY b.id) vaitro_donvi "
            + "      , a.note1 ykien_xuly "
            + "      ,  string_agg(case when b.organizationid>0 and b.type=4 then '-1' "
            + "             when b.organizationid>0 and (b.type=0 or b.type is null) then '0' "
            + "             when b.organizationid>0 and b.type in (0,4,3) and dd.\"SCHEMA_ID\" is null then '' "
            + "             when b.organizationid>0 and b.type in (0,3,4) and dd.\"SCHEMA_ID\" is not null then CAST(dd.\"SCHEMA_ID\" as text) end, ';' ORDER BY b.id) agent_id "
            + "      , 'end' task_key, 'Ban hành' action_tv, 'VT_BANHANH' approved "
            + "      , string_agg(case when b.userid>0 and (b.organizationid is null or b.organizationid=0) and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + "             when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + "             when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) trang_thai_xuly "
            + "      , string_agg(case when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.reviewdatenote is null then '' "
            + "             when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly "
            + "      , string_agg(case when b.organizationid>0 and (b.viewdocument=0 or b.viewdocument is null) and b.type in (0,4,3) then '0' "
            + "             when b.organizationid>0 and b.viewdocument=1 and b.reviewdatenote is null and b.type in (0,4,3) then '1' "
            + "             when b.organizationid>0 and b.viewdocument=1 and b.reviewdatenote is not null and b.type in (0,4,3) then '2' end, ';' ORDER BY b.id) trang_thai_xuly_donvi "
            + "      , string_agg(case when b.organizationid>0 and b.reviewdatenote is null and b.bookdate is null and b.type in (0,4,3) then '' "
            + "             when b.organizationid>0 and b.bookdate is not null and b.type in (0,4,3) then to_char(b.bookdate, ' DD/MM/YYYY HH24:MI:SS')  "
            + "             when b.organizationid>0 and b.reviewdatenote is not null and b.type in (0,4,3) then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly_donvi "
            + "      , string_agg(case when b.organizationid>0 then CAST(b.type as text) end, ';' ORDER BY b.id) loai_dv "
            + "      , 0 truoc_banhanh "
            + " from \"public\".\"documentoutgoing\" a "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) da "
            + "    on da.userid = a.userid  "
            + "   , \"public\".\"documentoutgoingdetail\" b  "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + "    on b.userid = c.userid "
            + "    left join \"public\".\"organization_hrm_unit\" dd "
            + "    on b.organizationid = dd.\"ORGANIZATIONID\"  "
            + " where a.organizationid in ({2}) and a.yeardocument in ({3}) "
            + "    and a.id = b.documentid "
            + " group by a.id, da.emailaddress "
            + " union ALL "
            + " select 2 STT, a.id id_vanban, da.emailaddress nguoi_gui "
            + "      , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + "          else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_gui "
            + "      , '' nguoi_nhan "
            + "      , '3' vai_tro_nguoinhan "
            + "      , '' donvi_nhan "
            + "      , '' vaitro_donvi "
            + "      , a.note1 ykien_xuly "
            + "      , '' agent_id "
            + "      , '' task_key, 'Nhập tắt' action_tv, 'GUI_YKIEN' approved "
            + "      , '2' trang_thai_xuly "
            + "      , (case when a.createdate is not null then to_char(a.createdate,' DD/MM/YYYY HH24:MI:SS')  "
            + "          else to_char(a.issuedate,' DD/MM/YYYY HH24:MI:SS') end) thoigian_xuly "
            + "      , '' trang_thai_xuly_donvi "
            + "      , '' thoigian_xuly_donvi "
            + "      , '' loai_dv "
            + "      , 0 truoc_banhanh "
            + " from \"public\".\"documentoutgoing\" a "
            + "    ,(select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) da "
            + " where length(\"a\".note1)>0 and a.userid = da.userid "
            + " and a.organizationid in ({4}) and a.yeardocument in ({5}) "
            + " and not exists(select 1 from \"public\".\"documentoutgoingdetail\" b where a.id = b.documentid) "
            + " order by id_vanban, stt, thoigian_gui";
        #endregion sql_so_luong_xuly_vbdi_from_postgres

        #region sql_so_luong_xuly_vbden_from_postgres
        public static string sql_so_luong_xuly_vbden = "select a.stt, a.id_vanban, c.emailaddress nguoi_gui, a.thoigian_gui, a.nguoi_nhan, a.vai_tro vai_tro_nguoinhan "
            + "   , a.donvi_nhan, a.vaitro_donvi, a.ykien_xuly, 0 agent_id, a.task_key, a.action_tv, a.approved "
            + "   , a.trang_thai_xuly, a.thoigian_xuly, a.trang_thai_xuly_donvi, a.thoigian_xuly_donvi, a.loai_dv "
            + " from ( "
            + " select 0 stt, a.id id_vanban,  CAST(a.userid as text)  nguoi_gui "
            + "    , to_char(CASE WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            ELSE a.createdate END, ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , '' nguoi_nhan, '' donvi_nhan "
            + "    , '3' vai_tro "
            + "   , ('Ý kiến đề xuất: '|| COALESCE(idea,'')  "
            + "   ||E'\r\nNgày xin ý kiến: '||COALESCE(to_char(a.ideadate,' DD/MM/YYYY'),'') "
            + "   ||E'\r\n---Ý KIẾN XỬ LÝ VĂN BẢN---'  "
            + "   ||E'\r\nNgày cho Ý kiến: '||COALESCE(to_char(a.assigndate,' DD/MM/YYYY'),'') "
            + "   ||E'\r\nÝ kiến: '||COALESCE(idea1,'') "
            + "   ||E'\r\nLãnh đạo: '||COALESCE(cvp.lastname,'')||' '||COALESCE(cvp.middlename,'')||' '||COALESCE(cvp.firstname,'') "
            + "   ||E'\r\n---Ý KIẾN LÃNH ĐẠO---'  "
            + "   ||E'\r\nNgày cho Ý kiến: '||COALESCE(to_char(a.leaderapprovedate,' DD/MM/YYYY'),'') "
            + "   ||E'\r\nÝ kiến: '||COALESCE(leadernote,'') "
            + "   ||E'\r\nLãnh đạo: '||COALESCE(tt.lastname,'')||' '||COALESCE(tt.middlename,'')||' '||COALESCE(tt.firstname,'')) as  ykien_xuly "
            + "    , '' vaitro_donvi,'' task_key, 'Xin ý kiến'  action_tv, 'GUI_YKIEN' approved "
            + "    , '2' trang_thai_xuly "
            + "    , to_char(CASE WHEN a.ideadate IS NOT NULL THEN a.ideadate "
            + "            WHEN a.assigndate IS NOT NULL THEN a.assigndate "
            + "            WHEN a.leaderapprovedate IS NOT NULL THEN a.leaderapprovedate "
            + "            ELSE a.createdate END + (5 * interval '1 minute'), ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi, '' thoigian_xuly_donvi, '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress,lastname,middlename,firstname from user_') "
            + "      as t(userid int, emailaddress text,lastname text,middlename text,firstname text)) cvp "
            + " on cvp.userid = a.processleaderid "
            + " left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress,lastname,middlename,firstname from user_')  "
            + "      as t(userid int, emailaddress text,lastname text,middlename text,firstname text)) tt "
            + " on tt.userid = a.leaderid "
            + " where a.yeardocument in ({0}) and a.organizationid in ({1}) "
            + " union all  "
            + " select 5 stt, a.id id_vanban, CAST((case when b.assignuserid>0 then b.assignuserid else a.userid end) as text) nguoi_gui "
            + "    , case when to_char(min(b.assigndate), ' DD/MM/YYYY HH24:MI:SS') is null then to_char(min(a.assigndate), ' DD/MM/YYYY HH24:MI:SS') "
			+ "		else to_char(min(b.assigndate), ' DD/MM/YYYY HH24:MI:SS') end thoigian_gui "
            + "    , string_agg(case when b.organizationid is null or b.organizationid=0 then c.emailaddress end, ';' ORDER BY b.id) nguoi_nhan "
            + "    , string_agg(CAST(d.\"UNIT_ID\" as text) , ';' ORDER BY b.id) donvi_nhan "
            + "    , string_agg(case when (b.mainprocess=0 or b.mainprocess is null) and b.userid>0 and (b.organizationid is null or b.organizationid=0) then '0'  "
            + "             when b.mainprocess=1 and b.userid>0 and (b.organizationid is null or b.organizationid=0) then '2' "
            + "             when b.mainprocess=2 and b.userid>0 and (b.organizationid is null or b.organizationid=0) then '1' end, ';' ORDER BY b.id) vai_tro "
            + "    , '' ykien_xuly "
            + "    , string_agg(case when (b.mainprocess=0 or b.mainprocess is null) and b.organizationid>0 then '2'  "
            + "             when b.mainprocess=1 and b.organizationid>0 then '1' "
            + "             when b.mainprocess=2 and b.organizationid>0 then '0' end, ';' ORDER BY b.id) vaitro_donvi "
            + "    , (case when row_number() OVER (PARTITION BY b.documentid) = 1 then 'end' else '' end) task_key "
            + "    , 'Chuyển xử lý' action_tv "
            + "    , (case when row_number() OVER (PARTITION BY b.documentid) = 1 then 'VT_BANHANH' else '' end)  approved "
            + "    , string_agg(case  when b.userid>0 and (b.organizationid is null or b.organizationid=0)  is not null and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + "         when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + "         when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) trang_thai_xuly "
            + "    , string_agg(case when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.reviewdatenote is null then '' "
            + "         when b.userid>0 and (b.organizationid is null or b.organizationid=0) and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly "
            + "    , string_agg(case  when b.organizationid>0 and (b.viewdocument=0 or b.viewdocument is null) then '0' "
            + "         when b.organizationid>0 and b.viewdocument=1 and b.reviewdatenote is null then '1' "
            + "         when b.organizationid>0 and b.viewdocument=1 and b.reviewdatenote is not null then '2' end, ';' ORDER BY b.id) trang_thai_xuly_donvi "
            + "    , string_agg(case when b.organizationid>0 and b.reviewdatenote is null then '' "
            + "         when b.organizationid>0 and b.reviewdatenote is not null then to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') end, ';' ORDER BY b.id) thoigian_xuly_donvi "
            + "    , string_agg(case  when b.organizationid>0 then '0' end, ';' ORDER BY b.id) loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + "      ,\"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + "     on c.userid = b.userid "
            + "    left join \"public\".\"organization_hrm_unit\" d "
            + "    on b.organizationid = d.\"ORGANIZATIONID\"  "
            + " where a.yeardocument in ({2})  and a.organizationid in ({3}) "
            + "   and a.id = b.documentid "
            + " group by a.id,b.documentid, b.assignuserid) a "
            + "    ,(select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + " where a.nguoi_gui = CAST(c.userid as text) "
            + " union all  "
            + " select 6 stt, a.id id_vanban "
            + "    , case when b.organizationid>0 then b.organizationname else d.emailaddress end nguoi_gui "
            + "    , to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') thoigian_gui "
            + "    , '' nguoi_nhan "
            + "    , case when b.userid>0 and (b.organizationid is null or b.organizationid=0) then '3' else '' end vai_tro_nguoinhan "
            + "    , '' donvi_nhan "
            + "    , case when b.organizationid>0 then '3' else '' end vaitro_donvi "
            + "    , b.note ykien_xuly "
            + "    , 0 agent_id, '' task_key, 'Gửi ý kiến'  action_tv, 'GUI_YKIEN' approved "
            + "    , '2' trang_thai_xuly "
            + "    , to_char(b.reviewdatenote, ' DD/MM/YYYY HH24:MI:SS') thoigian_xuly "
            + "    , '' trang_thai_xuly_donvi "
            + "    , '' thoigian_xuly_donvi "
            + "    , '' loai_dv "
            + " from \"dms_ubnd_baclieu_release\".\"public\".\"documentincoming\" a "
            + "      ,\"dms_ubnd_baclieu_release\".\"public\".\"documentincomingdetail\" b "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) d "
            + "    on b.userid = d.userid "
            + " where a.yeardocument in ({4})  and a.organizationid in ({5}) "
            + "   and a.id = b.documentid and length(b.note)>0 and b.reviewdatenote is not null "
            + " order by id_vanban, stt";
        #endregion sql_so_luong_xuly_vbden_from_postgres

        #region sql_so_log_xuly_vbdi_from_postgres
        public static string sql_so_log_xuly_vbdi = "SELECT a.id stt, b.id id_vanban, c.emailaddress nguoi_xu_ly "
            + "    , (case when a.type=4 then CAST(b.organizationid as text) "
            + "       when a.type in (0,3) then CAST(d.\"UNIT_ID\" as text) end) donvi_xuly "
            + "    , case when (a.viewdocument=0 or a.viewdocument is null) then 0  "
            + "         when a.viewdocument=1 and a.reviewdatenote is null then 1 "
            + "        else 2 end trang_thai "
            + "    , case when a.createdatenote is not null then to_char(a.createdatenote, ' DD/MM/YYYY HH24:MI:SS')  "
            + "        else '' end thoi_gian "
            + " FROM \"public\".\"documentoutgoingdetail\" a "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + "    on a.userid = c.userid "
            + "    left join \"public\".\"organization_hrm_unit\" d "
            + "    on a.organizationid = d.\"ORGANIZATIONID\"  "
            + "   , \"public\".\"documentoutgoing\" b "
            + " where a.documentid = b.id and a.type != 1 "
            + " and b.organizationid in ({0}) and b.yeardocument in ({1}) "
            + " and not exists(select 1 from \"public\".\"documentoutgoingdetail\" b where a.documentid = b.documentid and a.userid = b.userid and b.id<a.id) "
            + " order by id_vanban";
        #endregion sql_so_log_xuly_vbdi_from_postgres

        #region sql_so_log_xuly_vbden_from_postgres
        public static string sql_so_log_xuly_vbden = "SELECT a.id stt, b.id id_vanban, c.emailaddress nguoi_xu_ly, cast(d.\"UNIT_ID\" as text) donvi_xuly "
            + "    , case when (a.viewdocument=0 or a.viewdocument is null) then 0  "
            + "         when a.viewdocument=1 and a.reviewdatenote is null then 1 "
            + "        else 2 end trang_thai "
            + "    , case when a.createdatenote is not null then to_char(a.createdatenote, ' DD/MM/YYYY HH24:MI:SS')  "
            + "        else '' end thoi_gian "
            + " FROM \"public\".\"documentincomingdetail\" a "
            + "    left join (select * from dblink('dbname=lportal_ubnd_baclieu_release_ga3 user=postgres password=root','select userid, emailaddress from user_') as t(userid int, emailaddress text)) c "
            + "    on a.userid = c.userid "
            + "    left join \"public\".\"organization_hrm_unit\" d "
            + "    on a.organizationid = d.\"ORGANIZATIONID\"  "
            + "   , \"public\".\"documentincoming\" b "
            + " where a.documentid = b.id  "
            + " and b.organizationid in ({0}) and b.yeardocument in ({1}) "
            + " and not exists(select 1 from \"public\".\"documentincomingdetail\" b where a.documentid = b.documentid and a.userid = b.userid and b.id<a.id) "
            + " order by id_vanban";
        #endregion sql_so_log_xuly_vbden_from_postgres

        #endregion DONVI_SO

        #region PROC_SO
        public static string PROC_SO_VBDI_INFO = "ds_vb_di_sobannganh";
        public static string PROC_SO_VBDEN_INFO = "ds_vb_den_sobannganh";
        public static string PROC_SO_VBDI_FLOW = "luong_vb_di_sobannganh";
        public static string PROC_SO_VBDEN_FLOW = "luong_vb_den_sobannganh";
        public static string PROC_SO_VBDI_LOG = "log_vb_di_sobannganh";
        public static string PROC_SO_VBDEN_LOG = "log_vb_den_sobannganh";
        #endregion PROC_SO

        #region PROC_UBND
        public static string PROC_UBND_VBDI_INFO = "ds_vb_di_ubnd";
        public static string PROC_UBND_VBDEN_INFO = "ds_vb_den_ubnd";
        public static string PROC_UBND_VBDI_FLOW = "luong_vb_di_ubnd";
        public static string PROC_UBND_VBDEN_FLOW = "luong_vb_den_ubnd";
        public static string PROC_UBND_VBDI_LOG = "log_vb_di_ubnd";
        public static string PROC_UBND_VBDEN_LOG = "log_vb_den_ubnd";
        #endregion PROC_UBND

        #region DANHMUC
        #region sql_danhmuc_sovanban
        public static string sql_danhmuc_sovanban = "SELECT a.id \"ID\",  "
            + "  a.name \"ten_sovb\", "
            + "  case when a.type = 0 then 1 else 2 end \"loai_so\", "
            + "  ' 01/01/2011' \"ngay_mo_so\", "
            + "  ' 01/01/2021' \"ngay_het_hieuluc\", "
            + "  'admin.skhdtblu,huathithuyhang@skhdt.baclieu.gov.vn' \"nguoi_quantri\", "
            + "  'admin.skhdtblu,huathithuyhang@skhdt.baclieu.gov.vn' \"nguoi_theodoi\", "
            + "        a.position \"stt\", "
            + "  b.\"UNIT_ID\" \"UNIT_CODE\" "
            + " FROM \"public\".\"book\" a, \"public\".\"organization_hrm_unit\" b "
            + " where a.organizationid = {0} and b.\"ORGANIZATIONID\" = a.organizationid "
            + " order by \"loai_so\", \"stt\"";
        #endregion sql_danhmuc_sovanban

        #region DANHMUC_LINHVUC
        public static string sql_danhmuc_linhvuc = "SELECT a.id \"ID\",  "
            + "     a.name \"ten_linhvuc\", "
            + "        a.position \"stt\", "
            + "     b.\"CODE\" \"CODE\" "
            + " FROM \"public\".\"field\" a  "
            + "    left join \"public\".\"DM_LINHVUC\" b "
            + "    on a.id = b.\"ID\" "
            + " where a.organizationid = {0}";
        #endregion DANHMUC_LINHVUC

        #region DANHMUC_DCM_TYPE
        public static string sql_danhmuc_dcm_type = "SELECT a.id \"ID\",  "
            +"     a.name \"ten_hinhthuc\", "
            +"        a.position \"stt\", "
            +"     b.\"CODE\", "
            +"     b.\"KY_HIEU\" "
            +" FROM \"public\".\"documenttype\" a  "
            +"    left join \"public\".\"DM_LOAIVANBAN\" b "
            +"    on a.id = b.\"ID\" "
            +" where a.organizationid = {0}";
        #endregion DANHMUC_DCM_TYPE

        #endregion DANHMUC


        #region
        public static string sql_danhmuc_donvi_schema = "SELECT * from \"public\".\"organization_schema\"";
        #endregion

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
        public static string sql_insert_dcm_track = @"INSERT INTO CLOUD_ADMIN_DEV_BLU_2.DCM_TRACK (ID,DOC_ID,SCHEMA_ID, DOC_ID_SOURCE, SCHEMA_ID_SOURCE, DATE_INS, PARENT, CHILD) "
            + " VALUES (:ID,:DOC_ID,:SCHEMA_ID,:DOC_ID_SOURCE,:SCHEMA_ID_SOURCE,:DATE_INS,:PARENT,:CHILD)";
        #endregion sql_insert_dcm_track

        #region sql_insert_dcm_activiti_log
        public static string sql_insert_dcm_activiti_log = @"INSERT INTO {0}.DCM_ACTIVITI_LOG(ID,TASK_KEY,UPDATED_DATE,UPDATED_BY,ACTION"
            + ",DOC_ID, APPROVED,COMMENT_,COMMENT_FULL,FORMID,ACTION_CODE) VALUES(:ID,:TASK_KEY,:UPDATED_DATE,:UPDATED_BY,:ACTION,:DOC_ID"
            +",:APPROVED,:COMMENT_,:COMMENT_FULL,:FORMID,:ACTION_CODE)";
        #endregion sql_insert_dcm_activiti_log

        #region sql_insert_dcm_assign
        public static string sql_insert_dcm_assign = @"INSERT INTO {0}.DCM_ASSIGN(ID,DOCUMENT_ID,ASSIGNEE,ASSIGNER,ASSIGNED_DATE,ROLE_TYPE_CODE"
            + ",XU_LY,NGAY_XULY,ACTIVITI_LOG_ID,TRUOC_BANHANH) VALUES(:ID,:DOCUMENT_ID,:ASSIGNEE,:ASSIGNER,:ASSIGNED_DATE,:ROLE_TYPE_CODE,:XU_LY,:NGAY_XULY,:ACTIVITI_LOG_ID,:TRUOC_BANHANH)";
        #endregion sql_insert_dcm_assign

        #region sql_insert_dcm_donvi_nhan
        public static string sql_insert_dcm_donvi_nhan = @"INSERT INTO {0}.DCM_DONVI_NHAN(ID,DOC_ID, XULY_CHINH, AGENT_ID, UNIT_ID, ROLE_TYPE_CODE, DONVI_TRONG_NGOAI"
            + ", ACTIVITI_LOG_ID, ASSIGNED_DATE, XU_LY, TRUOC_BANHANH, TRANGTHAI_GUI) VALUES (:ID,:DOC_ID,:XULY_CHINH,:AGENT_ID,:UNIT_ID,:ROLE_TYPE_CODE,:DONVI_TRONG_NGOAI"
            +",:ACTIVITI_LOG_ID,:ASSIGNED_DATE,:XU_LY,:TRUOC_BANHANH,:TRANGTHAI_GUI)";
        #endregion sql_insert_dcm_donvi_nhan

        #region sql_insert_dcm_log
        public static string sql_insert_dcm_log = @"INSERT INTO {0}.DCM_LOG(ID,USERNAME,DATE_LOG,DCM_ID,IS_READ) VALUES (:ID,:USERNAME,:DATE_LOG,:DCM_ID,:IS_READ)";
        #endregion sql_insert_dcm_log

        #region sql_insert_dcm_log_read
        public static string sql_insert_dcm_log_read = @"INSERT INTO {0}.DCM_LOG_READ(ID,USERNAME,DATE_LOG,DCM_ID,IS_READ) VALUES (:ID,:USERNAME,:DATE_LOG,:DCM_ID,:IS_READ)";
        #endregion sql_insert_dcm_log_read


        #region delete table
        public static string sql_delete_table = "DELETE FROM {0}.{1} WHERE ROWNUM < {2}";
        public static string sql_delete_doc_doc = "DELETE FROM {0}.DCM_DOC";
        public static string sql_delete_doc_relation = "DELETE FROM {0}.DCM_DOC_RELATION";
        public static string sql_delete_FEM_FILE = "DELETE FROM {0}.FEM_FILE";
        public static string sql_delete_DCM_ATTACH_FILE = "DELETE FROM {0}.DCM_ATTACH_FILE";
        public static string sql_delete_DCM_TRACK = "DELETE FROM CLOUD_ADMIN_DEV_BLU_2.DCM_TRACK";
        public static string sql_delete_DCM_ACTIVITI_LOG = "DELETE FROM {0}.DCM_ACTIVITI_LOG";
        public static string sql_delete_DCM_ASSIGN = "DELETE FROM {0}.DCM_ASSIGN";
        public static string sql_delete_DCM_DONVI_NHAN = "DELETE FROM {0}.DCM_DONVI_NHAN";
        public static string sql_delete_DCM_LOG = "DELETE FROM {0}.DCM_LOG";
        public static string sql_delete_DCM_LOG_READ = "DELETE FROM {0}.DCM_LOG_READ";
        #endregion delete table

        # region sql_get_dcm_type
        public static string sql_get_dcm_type = "select code from {0}.dcm_type";
        #endregion sql_get_dcm_type

        #region DANHMUC
        #region insert_sovanban
        public static string SQL_INSERT_SOVANBAN = "INSERT INTO {0}.DCM_SOVANBAN(ID,NAME,SOBATDAU,MA_LOAI_SO,NGAY_BATDAU_SUDUNG,NGAY_DONGSO"
                + ",UNIT_CODE,CODE,NGUOI_THEODOI,NGUOI_QUANTRI,STT_HIENTHI) VALUES(:ID,:NAME,:SOBATDAU,:MA_LOAI_SO,:NGAY_BATDAU_SUDUNG,:NGAY_DONGSO"
                +",:UNIT_CODE,:CODE,:NGUOI_THEODOI,:NGUOI_QUANTRI,:STT_HIENTHI)";
        #endregion insert_sovanban

        #region DCM_LINHVUC
        public static string SQL_INSERT_DCM_LINHVUC = "INSERT INTO {0}.DCM_LINHVUC(ID, NAME, CODE, NGUOI_TAO, NGAY_TAO, STT_HIENTHI) "
                + "VALUES(:ID, :NAME, :CODE, :NGUOI_TAO, :NGAY_TAO, :STT_HIENTHI)";
        #endregion DCM_LINHVUC

        #region DCM_TYPE
        public static string SQL_INSERT_DCM_TYPE = "INSERT INTO {0}.DCM_TYPE(ID, NAME, CODE, NGUOI_TAO, NGAY_TAO, STT_HIENTHI, KYHIEU) "
                + "VALUES(:ID, :NAME, :CODE, :NGUOI_TAO, :NGAY_TAO, :STT_HIENTHI, :KYHIEU)";
        #endregion DCM_TYPE
        #endregion DANHMUC

        #region
        public static string sql_insert_Dcm_SoVB_TemplateSinhSo = "INSERT INTO {0}.DCM_SOVB_TEMPLATESINHSO(ID,SOVANBAN_CODE,TYPE_CODE,TEMPLATE_SINH_SOVB_CODE)"
            + " VALUES(:ID,:SOVANBAN_CODE,:TYPE_CODE,:TEMPLATE_SINH_SOVB_CODE)";
        #endregion

        #region
        public static string sql_insert_DCM_QUYTAC_NHAYSO = "INSERT INTO {0}.DCM_QUYTAC_NHAYSO(ID,MA_QUYTAC,SOVANBAN_CODE,TYPE_CODE) "
            +" VALUES(:ID,:MA_QUYTAC,:SOVANBAN_CODE,:TYPE_CODE)";
        #endregion

        #endregion oracle query

        #endregion query data

        string[] vbdi_col_arr = { "id", "thoigian_tao", "nguoi_tao_banhanh", "trich_yeu", "so_kyhieu", "coquan_banhanh", "id_hinhthuc", "id_dokhan", "id_linhvuc", "id_sovanban", "ngay_banhanh", "nguoi_ky", "so_trang", "so_ban", "file_dinhkem", "id_vblqs", "donvi_nhanngoai", "nguoi_soan", "id_domat", "donvi_soanthao", "chucvu_nguoiky", "yeardocument", "ghichu_trinhky", "ghichu_banhanh", "ykien_banhanh", "stateid", "vb_trinh_ky", "so_trinh_ky", "ngay_trinh_ky", "ngay_ky", "lanhdao_ky" };

        public static int INCREASEID_VBDI = 2000000;
        public static long INCREASEID_OTHERS = 20000000;
        
        public static string SEQ_DCM_DOC_RELATION = "DCM_DOC_RELATION_SEQ";
        public static string SEQ_FEM_FILE = "FEM_FILE_SEQ";
        public static string SEQ_DCM_ATTACH_FILE = "DCM_ATTACH_FILE_SEQ";
        public static string SEQ_DCM_TRACK = "DCM_TRACK_SEQ";

        public static string SEQ_DCM_ACTIVITI_LOG = "DCM_ACTIVITI_LOG_SEQ";
        public static string SEQ_DCM_ASSIGN = "DCM_ASSIGN_SEQ";
        public static string SEQ_DCM_DONVI_NHAN = "DCM_DONVI_NHAN_SEQ";

        public static string SEQ_DCM_LOG = "DCM_LOG_SEQ";
        public static string SEQ_DCM_LOG_READ = "DCM_LOG_READ_SEQ";

        public static string SEQ_DCM_SOVB_TEMPLATESINHSO = "DCM_SOVB_TEMPLATESINHSO_SEQ";
        public static string SEQ_DCM_QUYTAC_NHAYSO = "DCM_QUYTAC_NHAYSO_SEQ";

        #region CHECKING_COVERTED_DATA
        public static string SQL_GET_CONVERTED_DATA_COUNT = "SELECT COUNT(1) FROM {0}{1}";
        public static string SQL_GET_CONVERTED_DATA = "SELECT ID FROM {0}{1}";
        public static string SQL_INSERT_CONVERTED_DATA = "INSERT INTO CLOUD_ADMIN_DEV_BLU_4.{0}_TEST(ID) VALUES(:ID)";

        public static string SQL_INSERT_CONVERTED_DATA_DCM_ACTIVITI_LOG = "INSERT INTO CLOUD_ADMIN_DEV_BLU_4.DCM_ACTIVITI_LOG_TEST(ID) VALUES(:ID)";
        #endregion
    }
}
