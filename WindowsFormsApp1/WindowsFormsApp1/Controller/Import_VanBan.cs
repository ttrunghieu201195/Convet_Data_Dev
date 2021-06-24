using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{
    class Import_VanBan
    {
        // list dcm_doc
        private static List<Dcm_Doc> dcm_Docs = new List<Dcm_Doc>();
        // list dcm_doc_relation
        private static List<Dcm_Doc_Relation> dcm_Doc_Relations = new List<Dcm_Doc_Relation>();
        // list fem_file
        private static List<Fem_File> fem_Files = new List<Fem_File>();
        // list dcm_attach_file
        private static List<Dcm_Attach_File> dcm_Attach_Files = new List<Dcm_Attach_File>();
        // list dcm_track
        private static List<Dcm_Track> dcm_Tracks = new List<Dcm_Track>();

        // SEQ
        public static long SEQ_DCM_DOC_RELATION;
        public static long SEQ_FEM_FILE;
        public static long SEQ_DCM_ATTACH_FILE;
        public static long SEQ_DCM_TRACK;

        public static List<Dcm_Doc> getDcm_Docs()
        {
            return dcm_Docs;
        }

        public static List<Dcm_Doc_Relation> getDcm_Doc_Relations()
        {
            return dcm_Doc_Relations;
        }

        public static List<Fem_File> getFem_Files()
        {
            return fem_Files;
        }

        public static List<Dcm_Attach_File> getDcm_Attach_Files()
        {
            return dcm_Attach_Files;
        }

        public static List<Dcm_Track> getDcm_Tracks()
        {
            return dcm_Tracks;
        }

        public static void exportdataFromPostgres(NpgsqlConnection postgresConnection, Configs configs, string query, Common.VB_TYPE type_vb)
        {
            try
            {
                var cmd = new NpgsqlCommand(query, postgresConnection);

                NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                DataTable dataTable = new DataTable();

                dataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];
                resetListData();
                foreach (DataRow row in dataTable.Rows)
                {
                    if (type_vb == Common.VB_TYPE.VB_DI)
                    {
                        ParseDataToOutgoingDocInfo(row, type_vb);
                    }

                    if (type_vb == Common.VB_TYPE.VB_DEN)
                    {
                        ParseDataToIncomingDocInfo(row, configs, type_vb);
                    }
                    
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ParseDataToOutgoingDocInfo(DataRow row, Common.VB_TYPE type_vb)
        {
            Dcm_Doc dcm_Doc = new Dcm_Doc();
            // 1 - id van ban
            dcm_Doc.id_VanBan = int.Parse(row["id"].ToString()) + Constants.INCREASEID_VBDI;

            // 2 - thoi gian tao
            string ngay_tao = row["thoigian_tao"].ToString();
            if (ngay_tao != null && ngay_tao != String.Empty)
            {
                DateTime ngaytao = DateTime.ParseExact(ngay_tao.Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                dcm_Doc.ngay_tao = ngaytao;
            }

            // 3 - nguoi tao ban hanh
            string nguoi_vaoso = row["nguoi_tao_banhanh"].ToString();
            if (nguoi_vaoso != null && nguoi_vaoso != String.Empty)
            {
                dcm_Doc.nguoi_vaoso = nguoi_vaoso.Trim();
            }

            // Set ngay den di = ngay tao voi vb di
            dcm_Doc.ngay_den_di = dcm_Doc.ngay_tao;

            // 4 - trich yeu
            string trich_yeu = row["trich_yeu"].ToString();
            if (trich_yeu != null && trich_yeu != String.Empty)
            {
                dcm_Doc.trich_yeu = Common.escape_Trichyeu(trich_yeu.Trim());
            }

            // 5 - so ky hieu
            string so_kyhieu = row["so_kyhieu"].ToString();
            if (so_kyhieu != null && so_kyhieu != String.Empty)
            {
                dcm_Doc.so_kyhieu = so_kyhieu.Replace("'", "").Trim();
            }

            // 6 - co quan ban hanh
            string coquan_banhanh = row["coquan_banhanh"].ToString();
            if (coquan_banhanh != null && coquan_banhanh != String.Empty)
            {
                dcm_Doc.coquan_banhanh = coquan_banhanh.Trim();
            }

            // 7 - hinh thuc code
            string dcmtype_code = row["id_hinhthuc"].ToString();
            if (dcmtype_code != null && dcmtype_code != String.Empty)
            {
                dcm_Doc.dcmtype_code = dcmtype_code.Trim();
            }

            // 8 - do khan code
            string priority_code = row["id_dokhan"].ToString();
            if (priority_code != null && priority_code != String.Empty)
            {
                switch (priority_code.Trim())
                {
                    case "1":
                        dcm_Doc.priority_code = "THUONG";
                        break;
                    case "2":
                        dcm_Doc.priority_code = "KHAN";
                        break;
                    case "3":
                        dcm_Doc.priority_code = "HOATOC";
                        break;
                    default:
                        dcm_Doc.priority_code = "THUONG";
                        break;
                }
            }

            // 9 - linh vuc code
            string linhvuc_code = row["id_linhvuc"].ToString();
            if (linhvuc_code != null && linhvuc_code != String.Empty)
            {
                dcm_Doc.linhvuc_code = linhvuc_code.Trim();
            }

            // 10 - so van ban code
            string so_vanban_code = row["id_sovanban"].ToString();
            if (so_vanban_code != null && so_vanban_code != String.Empty)
            {
                // + 20.000.000
                dcm_Doc.so_vanban_code = (int.Parse(so_vanban_code.Trim()) + Constants.INCREASEID_OTHERS).ToString();
            }

            // 11 - ngay ban hanh/ ngay cong van
            string ngay_cong_van = row["ngay_banhanh"].ToString();
            if (ngay_cong_van != null && ngay_cong_van != String.Empty)
            {
                DateTime ngaybanhanh = DateTime.ParseExact(ngay_cong_van.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_Doc.ngay_ban_hanh = ngaybanhanh;
            }

            // Set ngay van ban = ngay ban hanh voi vb di
            dcm_Doc.ngay_van_ban = dcm_Doc.ngay_ban_hanh;

            // 12 - nguoi ky
            string nguoi_ky = row["nguoi_ky"].ToString();
            if (nguoi_ky != null && nguoi_ky != String.Empty)
            {
                dcm_Doc.nguoi_ky_chinh = nguoi_ky.Trim();
            }

            // 13 - so trang
            string so_trang = row["so_trang"].ToString();
            if (so_trang != null && so_trang != String.Empty)
            {
                dcm_Doc.so_trang = int.Parse(so_trang.Trim());
            }

            // 14 - so ban
            string so_ban = row["so_ban"].ToString();
            if (so_ban != null && so_ban != String.Empty)
            {
                dcm_Doc.so_ban = int.Parse(so_ban.Trim());
            }

            // 15 - file dinh kem
            string file_dinhkems = row["file_dinhkem"].ToString();
            if (file_dinhkems != null && file_dinhkems != String.Empty)
            {
                appendToListFileDinhKem(dcm_Doc.id_VanBan, file_dinhkems.Trim());
                //dcm_Doc.filesDinhKem = file_dinhkems.Trim();
            }

            // 16 - van ban lien quan
            string id_vblqs = row["id_vblqs"].ToString();
            if (id_vblqs != null && id_vblqs != String.Empty)
            {
                appendToListDcmDocRelation(dcm_Doc.id_VanBan, id_vblqs.Trim(), type_vb);
                //dcm_Doc.id_vblqs = id_vblqs.Trim();
            }

            // 17 - don vi nhan ngoai
            string donvi_nhanngoai = row["donvi_nhanngoai"].ToString();
            if (donvi_nhanngoai != null && donvi_nhanngoai != String.Empty)
            {
                dcm_Doc.dv_nhanngoai = donvi_nhanngoai.Trim();
            }

            // 18 - nguoi soan
            string nguoi_soan = row["nguoi_soan"].ToString();
            if (nguoi_soan != null && nguoi_soan != String.Empty)
            {
                dcm_Doc.nguoi_soan = nguoi_soan.Trim();
            }

            // 6 - do mat
            string id_domat = row["id_domat"].ToString();
            if (id_domat != null && id_domat != String.Empty)
            {
                switch (id_domat.Trim())
                {
                    case "1":
                        dcm_Doc.confidential_code = "THUONG";
                        break;
                    case "2":
                        dcm_Doc.confidential_code = "MAT";
                        break;
                    case "3":
                        dcm_Doc.confidential_code = "TOIMAT";
                        break;
                    default:
                        dcm_Doc.confidential_code = "THUONG";
                        break;
                }
            }

            // 20 - don vi soan thao
            string donvi_soanthao = row["donvi_soanthao"].ToString();
            if (donvi_soanthao != null && donvi_soanthao != String.Empty)
            {
                dcm_Doc.donvi_soanthao = donvi_soanthao.Trim();
            }

            // 21 - chuc vu nguoi ky
            string chucvu_nguoiky = row["chucvu_nguoiky"].ToString();
            if (chucvu_nguoiky != null && chucvu_nguoiky != String.Empty)
            {
                dcm_Doc.chucvu_nguoiky = chucvu_nguoiky.Trim();
            }

            // 22 - nam van ban

            // 23 - ghi chu trinh ky
            string ghichu_trinhky = row["ghichu_trinhky"].ToString();
            if (ghichu_trinhky != null && ghichu_trinhky != String.Empty)
            {
                dcm_Doc.ghi_chu = ghichu_trinhky.Trim();
            }

            // 24 - ghi chu ban hanh
            string ghichu_banhanh = row["ghichu_banhanh"].ToString();
            if (ghichu_banhanh != null && ghichu_banhanh != String.Empty)
            {
                dcm_Doc.doc_note = ghichu_banhanh.Trim();
            }

            // 25 - y kien ban hanh

            // 26 - stateid

            // 27 - vb trinh ky
            string vb_trinh_ky = row["vb_trinh_ky"].ToString();
            if (vb_trinh_ky != null && vb_trinh_ky != String.Empty)
            {
                dcm_Doc.vb_trinhky = int.Parse(vb_trinh_ky.Trim());
            }


            // 28 - so trinh ky
            string so_trinh_ky = row["so_trinh_ky"].ToString();
            if (so_trinh_ky != null && so_trinh_ky != String.Empty)
            {
                dcm_Doc.so_trinhky = int.Parse(so_trinh_ky.Trim());
            }

            // 29 - ngay trinh ky
            string ngay_trinh_ky = row["ngay_trinh_ky"].ToString();
            if (ngay_trinh_ky != null && ngay_trinh_ky != String.Empty)
            {
                DateTime ngay_trinhky = DateTime.ParseExact(ngay_trinh_ky.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_Doc.ngay_trinhky = ngay_trinhky;
                //dcm_doc.ngay_trinhky = "TO_DATE('" + ngay_trinhky.ToString("MM/dd/yyyy") + "','mm/dd/yyyy')";
            }

            // 30 - ngay ky
            string ngay_ky = row["ngay_ky"].ToString();
            if (ngay_ky != null && ngay_ky != String.Empty)
            {
                DateTime ngayky = DateTime.ParseExact(ngay_ky.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_Doc.ngay_ky = ngayky;
                //dcm_doc.ngay_ky = "TO_DATE('" + ngayky.ToString("MM/dd/yyyy") + "','mm/dd/yyyy')";
            }

            // 31 - lanh dao ky
            string lanhdao_ky = row["lanhdao_ky"].ToString();
            if (lanhdao_ky != null && lanhdao_ky != String.Empty)
            {
                switch (lanhdao_ky)
                {
                    case "0":
                        dcm_Doc.trang_thai = 21;
                        break;
                    case "2":
                        dcm_Doc.trang_thai = 2;
                        break;
                    case "1":
                        dcm_Doc.trang_thai = 1;
                        break;
                    default:
                        dcm_Doc.trang_thai = 1;
                        break;
                }
            }

            // 32 - unit id
            string unit_id = row["unit_id"].ToString();
            if (unit_id != null && unit_id != String.Empty)
            {
                dcm_Doc.unit_id = int.Parse(unit_id);
            }

            // so den di
            if (dcm_Doc.so_kyhieu.Length > 0)
            {
                string[] regex = Regex.Split(dcm_Doc.so_kyhieu, @"\D+");
                dcm_Doc.so_den_di = Int64.Parse(regex[0] != String.Empty ? regex[0] : "0");
            }

            // hinh thuc gui code
            dcm_Doc.hinhthuc_gui_code = "BUUDIEN";

            // cong van den di (2: van ban di, 1: van ban den)
            dcm_Doc.congvan_dendi = 2;

            if (dcm_Doc.vb_trinhky == 1)
            {
                dcm_Doc.process_key = "BLU_VB_DI_TRINH_KY_01_6563:1:188167505";
            }
            else
            {
                dcm_Doc.process_key = "VB_DI_VT2_UBBL_6563: 1:188102558";
            }

            // add to list_dcm_doc
            dcm_Docs.Add(dcm_Doc);
        }

        private static void ParseDataToIncomingDocInfo(DataRow row, Configs configs, Common.VB_TYPE type_vb)
        {

            Dcm_Doc dcm_Doc = new Dcm_Doc();
            // 1 - id van ban
            dcm_Doc.id_VanBan = int.Parse(row["id"].ToString());

            // 2 - thoi gian tao
            string ngay_tao = row["thoigian_tao"].ToString();
            if (ngay_tao != null && ngay_tao != String.Empty)
            {
                DateTime ngaytao = DateTime.ParseExact(ngay_tao.Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                dcm_Doc.ngay_tao = ngaytao;
                //dcm_doc.ngay_tao = "TO_DATE('" + ngaytao.ToString("MM/dd/yyyy HH:mm:ss") + "','mm/dd/yyyy HH24:MI:SS')";
            }

            // 3 - nguoi vao so
            string nguoi_vaoso = row["nguoi_tao"].ToString();
            if (nguoi_vaoso != null && nguoi_vaoso != String.Empty)
            {
                dcm_Doc.nguoi_vaoso = nguoi_vaoso.Trim();
            }

            // 4 - trich yeu
            string trich_yeu = row["trich_yeu"].ToString();
            if (trich_yeu != null && trich_yeu != String.Empty)
            {
                dcm_Doc.trich_yeu = Common.escape_Trichyeu(trich_yeu.Trim());
            }

            // 7 - so ky hieu
            string so_kyhieu = row["so_kyhieu"].ToString();
            if (so_kyhieu != null && so_kyhieu != String.Empty)
            {
                dcm_Doc.so_kyhieu = so_kyhieu.Replace("'", "").Trim();
            }

            // 8 - co quan ban hanh
            string coquan_banhanh = row["coquan_banhanh"].ToString();
            if (coquan_banhanh != null && coquan_banhanh != String.Empty)
            {
                dcm_Doc.coquan_banhanh = coquan_banhanh.Trim();
            }

            // 9 - hinh thuc code
            string dcmtype_code = row["id_hinhthuc"].ToString();
            if (dcmtype_code != null && dcmtype_code != string.Empty)
            {
                dcm_Doc.dcmtype_code = dcmtype_code.Trim();
            }

            // 10 - do khan code
            string priority_code = row["id_dokhan"].ToString();
            if (priority_code != null && priority_code != String.Empty)
            {
                switch (priority_code.Trim())
                {
                    case "1":
                        dcm_Doc.priority_code = "THUONG";
                        break;
                    case "2":
                        dcm_Doc.priority_code = "KHAN";
                        break;
                    case "3":
                        dcm_Doc.priority_code = "HOATOC";
                        break;
                    default:
                        dcm_Doc.priority_code = "THUONG";
                        break;
                }
            }

            // 11 - linh vuc code
            string linhvuc_code = row["id_linhvuc"].ToString();
            if (linhvuc_code != null && linhvuc_code != String.Empty)
            {
                dcm_Doc.linhvuc_code = linhvuc_code.Trim();
            }

            // 12 - so van ban code
            string so_vanban_code = row["id_sovanban"].ToString();
            if (so_vanban_code != null && so_vanban_code != String.Empty)
            {
                // + 20.000.000
                dcm_Doc.so_vanban_code = (int.Parse(so_vanban_code.Trim()) + Constants.INCREASEID_OTHERS).ToString();
            }

            // 13 - ngay den
            string ngay_den = row["ngay_den"].ToString();
            if (ngay_den != null && ngay_den != String.Empty)
            {
                DateTime ngay_dendi = DateTime.ParseExact(ngay_den.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_Doc.ngay_den_di = ngay_dendi;
            }

            // 14 - ngay ban hanh/ ngay cong van
            string ngay_van_ban = row["ngay_van_ban"].ToString();
            if (ngay_van_ban != null && ngay_van_ban != String.Empty)
            {
                DateTime ngay_vanban = DateTime.ParseExact(ngay_van_ban.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_Doc.ngay_van_ban = ngay_vanban;
            }

            // 15 - nguoi ky
            string nguoi_ky = row["nguoi_ky"].ToString();
            if (nguoi_ky != null && nguoi_ky != String.Empty)
            {
                dcm_Doc.nguoi_ky_chinh = nguoi_ky.Trim();
            }


            // 17 - so trang
            string so_trang = row["so_trang"].ToString();
            if (so_trang != null && so_trang != String.Empty)
            {
                dcm_Doc.so_trang = int.Parse(so_trang.Trim());
            }

            // 18 - so ban
            string so_ban = row["so_ban"].ToString();
            if (so_ban != null && so_ban != String.Empty)
            {
                dcm_Doc.so_ban = int.Parse(so_ban.Trim());
            }

            // 19 - han xu ly
            string han_xuly = row["han_xu_ly"].ToString();
            if (han_xuly != null && han_xuly != String.Empty)
            {
                DateTime han_xu_ly = DateTime.ParseExact(han_xuly.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcm_Doc.han_giaiquyet = han_xu_ly;
            }

            // 20 - file dinh kem
            string file_dinhkems = row["ds_file"].ToString();
            if (file_dinhkems != null && file_dinhkems != String.Empty)
            {
                appendToListFileDinhKem(dcm_Doc.id_VanBan, file_dinhkems.Trim());
            }

            // 21 - van ban lien quan
            string id_vblqs = row["id_vblqs"].ToString();
            if (id_vblqs != null && id_vblqs != String.Empty)
            {
                appendToListDcmDocRelation(dcm_Doc.id_VanBan, id_vblqs.Trim(), type_vb);
            }

            // 6 - do mat
            string id_domat = row["do_mat"].ToString();
            if (id_domat != null && id_domat != String.Empty)
            {
                switch (id_domat.Trim())
                {
                    case "1":
                        dcm_Doc.confidential_code = "THUONG";
                        break;
                    case "2":
                        dcm_Doc.confidential_code = "MAT";
                        break;
                    case "3":
                        dcm_Doc.confidential_code = "TOIMAT";
                        break;
                    default:
                        dcm_Doc.confidential_code = "THUONG";
                        break;
                }
            }

            // 16 - chuc vu nguoi ky
            string chucvu_nguoiky = row["chuc_vu"].ToString();
            if (chucvu_nguoiky != null && chucvu_nguoiky != String.Empty)
            {
                dcm_Doc.chucvu_nguoiky = chucvu_nguoiky.Trim();
            }

            // 5 - so den
            string so_den = row["so_den"].ToString();
            if (so_den != null && so_den != String.Empty)
            {
                dcm_Doc.so_den_di = int.Parse(so_den);
            }

            // 22 - receive document id/Van ban ban hanh den don vi
            string receivedocumentid = row["receivedocumentid"].ToString();
            if (receivedocumentid != null && receivedocumentid != String.Empty)
            {
                long vb_banhanh_den_donvi = long.Parse(receivedocumentid);
                if (vb_banhanh_den_donvi > 0)
                {
                    // 23 - id don vi ban hanh/ issueorganizationid

                    // 24 - schema don vi ban hanh / schema
                    string schema = row["schema"].ToString();
                    if (schema != null && schema != String.Empty)
                    {
                        appendToListDcmTrack(dcm_Doc.id_VanBan, configs.schema, vb_banhanh_den_donvi, schema, dcm_Doc.ngay_van_ban);
                    }
                }
            }

            //23 - ghi chu vb den/ ghi_chu
            string ghi_chu = row["ghi_chu"].ToString();
            if (ghi_chu != null && ghi_chu != String.Empty)
            {
                dcm_Doc.doc_note = ghi_chu;
            }

            // 26 - unit id
            string unit_id = row["unit_id"].ToString();
            if (unit_id != null && unit_id != String.Empty)
            {
                dcm_Doc.unit_id = int.Parse(unit_id);
            }

            // cong van den di (2: van ban di, 1: van ban den)
            dcm_Doc.congvan_dendi = 1;

            // Process key
            dcm_Doc.process_key = "VAN_BAN_DEN_CAP1_UBBL_6563: 1:175549970";

            // add to list_dcm_doc
            dcm_Docs.Add(dcm_Doc);
        }

        private static void appendToListDcmDocRelation(int id_vb, string id_vblqs, Common.VB_TYPE type_vb)
        {
            string[] id_vblqs_arr = id_vblqs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string id in id_vblqs_arr)
            {
                Dcm_Doc_Relation dcm_Doc_Relation = new Dcm_Doc_Relation();
                dcm_Doc_Relation.id = ++SEQ_DCM_DOC_RELATION;
                dcm_Doc_Relation.dcm_id = id_vb;
                dcm_Doc_Relation.dcm_document_id = int.Parse(id); // + 2.000.000 voi van ban di
                if (type_vb == Common.VB_TYPE.VB_DEN)
                {
                    dcm_Doc_Relation.dcm_document_id += Constants.INCREASEID_VBDI;
                }
                dcm_Doc_Relations.Add(dcm_Doc_Relation);
            }
        }

        private static void appendToListFileDinhKem(int id_vb, string filesDinhKems)
        {
            string[] filesDinhKems_arr = filesDinhKems.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string fileDinhKem in filesDinhKems_arr)
            {
                Fem_File fem_File = new Fem_File();
                fem_File.id = ++SEQ_FEM_FILE;
                fem_File.hdd_file = fileDinhKem;
                fem_File.name = Common.getFileNameFromHddFile(fileDinhKem);
                fem_Files.Add(fem_File);

                Dcm_Attach_File dcm_Attach_File = new Dcm_Attach_File();
                dcm_Attach_File.id = ++SEQ_DCM_ATTACH_FILE;
                dcm_Attach_File.id_vb = id_vb;
                dcm_Attach_File.file_id = fem_File.id;
                dcm_Attach_Files.Add(dcm_Attach_File);
            }
        }

        private static void appendToListDcmTrack(long doc_id_des, string schema_id_des, long doc_id_sour, string schema_id_sour, DateTime? date_ins)
        {
            Dcm_Track dcm_Track = new Dcm_Track();
            dcm_Track.id = ++SEQ_DCM_TRACK;
            dcm_Track.doc_id = doc_id_des;
            dcm_Track.schema_id = schema_id_des;
            dcm_Track.doc_id_source = doc_id_sour;
            dcm_Track.schema_id_source = schema_id_sour;
            dcm_Track.date_ins = date_ins;
            dcm_Track.parent = doc_id_des + "|" + schema_id_des;
            dcm_Track.child = doc_id_sour + "|" + schema_id_sour;
            dcm_Tracks.Add(dcm_Track);
        }

        public static void insert_Dcm_Doc(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Doc> data)
        {
            try
            {
                if (data.Count > 0)
                {
                    OracleCommand cmd = oracleConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.ArrayBindCount = data.Count;

                    cmd.CommandText = string.Format(query, configs.schema);

                    cmd.Parameters.Add("ID", OracleDbType.Int64);
                    cmd.Parameters.Add("DCMTYPE_CODE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("TRICH_YEU", OracleDbType.Varchar2);
                    cmd.Parameters.Add("SO_KYHIEU", OracleDbType.Varchar2);
                    cmd.Parameters.Add("SO_DEN_DI", OracleDbType.Int64);
                    cmd.Parameters.Add("NGUOI_KY_CHINH", OracleDbType.Varchar2);
                    cmd.Parameters.Add("NGUOI_SOAN", OracleDbType.Varchar2);
                    cmd.Parameters.Add("SO_VANBAN_CODE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("NGAY_TAO", OracleDbType.Date);
                    cmd.Parameters.Add("NGAY_VAN_BAN", OracleDbType.Date);
                    cmd.Parameters.Add("NGAY_DEN_DI", OracleDbType.Date);
                    cmd.Parameters.Add("SO_BAN", OracleDbType.Int64);
                    cmd.Parameters.Add("SO_TRANG", OracleDbType.Int64);
                    cmd.Parameters.Add("PRIORITY_CODE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("CONFIDENTIAL_CODE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("LINHVUC_CODE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("HINHTHUC_GUI_CODE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("HAN_GIAIQUYET", OracleDbType.Date);
                    cmd.Parameters.Add("CONGVAN_DENDI", OracleDbType.Int64);
                    cmd.Parameters.Add("TRANG_THAI", OracleDbType.Int64);
                    cmd.Parameters.Add("ngay_ban_hanh", OracleDbType.Date);
                    cmd.Parameters.Add("donvi_soanthao", OracleDbType.Varchar2);
                    cmd.Parameters.Add("DONVI_BANHANH", OracleDbType.Varchar2);
                    cmd.Parameters.Add("unit_id", OracleDbType.Int64);
                    cmd.Parameters.Add("DONVI_NHANNGOAI", OracleDbType.Varchar2);
                    cmd.Parameters.Add("NGUOI_SOANTHAO", OracleDbType.Varchar2);
                    cmd.Parameters.Add("CHUCVU_NGUOIKY", OracleDbType.Varchar2);
                    cmd.Parameters.Add("DOC_NOTE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("VB_TRINH_KY", OracleDbType.Int64);
                    cmd.Parameters.Add("SO_TRINH_KY", OracleDbType.Int64);
                    cmd.Parameters.Add("GHI_CHU", OracleDbType.Varchar2);
                    cmd.Parameters.Add("NGAY_TRINH_KY", OracleDbType.Date);
                    cmd.Parameters.Add("NGAY_KY", OracleDbType.Date);
                    cmd.Parameters.Add("PROCESS_KEY", OracleDbType.Varchar2);

                    cmd.Parameters["ID"].Value = data.Select(dcm_doc => dcm_doc.id_VanBan).ToArray();
                    cmd.Parameters["DCMTYPE_CODE"].Value = data.Select(dcm_doc => dcm_doc.dcmtype_code).ToArray();
                    cmd.Parameters["TRICH_YEU"].Value = data.Select(dcm_doc => dcm_doc.trich_yeu).ToArray();
                    cmd.Parameters["SO_KYHIEU"].Value = data.Select(dcm_doc => dcm_doc.so_kyhieu).ToArray();
                    cmd.Parameters["SO_DEN_DI"].Value = data.Select(dcm_doc => dcm_doc.so_den_di).ToArray();
                    cmd.Parameters["NGUOI_KY_CHINH"].Value = data.Select(dcm_doc => dcm_doc.nguoi_ky_chinh).ToArray();
                    cmd.Parameters["NGUOI_SOAN"].Value = data.Select(dcm_doc => dcm_doc.nguoi_vaoso).ToArray();
                    cmd.Parameters["SO_VANBAN_CODE"].Value = data.Select(dcm_doc => dcm_doc.so_vanban_code).ToArray();
                    cmd.Parameters["NGAY_TAO"].Value = data.Select(dcm_doc => dcm_doc.ngay_tao).ToArray();
                    cmd.Parameters["NGAY_VAN_BAN"].Value = data.Select(dcm_doc => dcm_doc.ngay_van_ban).ToArray();
                    cmd.Parameters["NGAY_DEN_DI"].Value = data.Select(dcm_doc => dcm_doc.ngay_den_di).ToArray();
                    cmd.Parameters["SO_BAN"].Value = data.Select(dcm_doc => dcm_doc.so_ban).ToArray();
                    cmd.Parameters["SO_TRANG"].Value = data.Select(dcm_doc => dcm_doc.so_trang).ToArray();
                    cmd.Parameters["PRIORITY_CODE"].Value = data.Select(dcm_doc => dcm_doc.priority_code).ToArray();
                    cmd.Parameters["CONFIDENTIAL_CODE"].Value = data.Select(dcm_doc => dcm_doc.confidential_code).ToArray();
                    cmd.Parameters["LINHVUC_CODE"].Value = data.Select(dcm_doc => dcm_doc.linhvuc_code).ToArray();
                    cmd.Parameters["HINHTHUC_GUI_CODE"].Value = data.Select(dcm_doc => dcm_doc.hinhthuc_gui_code).ToArray();
                    cmd.Parameters["HAN_GIAIQUYET"].Value = data.Select(dcm_doc => dcm_doc.han_giaiquyet).ToArray();
                    cmd.Parameters["CONGVAN_DENDI"].Value = data.Select(dcm_doc => dcm_doc.congvan_dendi).ToArray();
                    cmd.Parameters["TRANG_THAI"].Value = data.Select(dcm_doc => dcm_doc.trang_thai).ToArray();
                    cmd.Parameters["ngay_ban_hanh"].Value = data.Select(dcm_doc => dcm_doc.ngay_ban_hanh).ToArray();
                    cmd.Parameters["donvi_soanthao"].Value = data.Select(dcm_doc => dcm_doc.donvi_soanthao).ToArray();
                    cmd.Parameters["DONVI_BANHANH"].Value = data.Select(dcm_doc => dcm_doc.coquan_banhanh).ToArray();
                    cmd.Parameters["unit_id"].Value = data.Select(dcm_doc => dcm_doc.unit_id).ToArray();
                    cmd.Parameters["DONVI_NHANNGOAI"].Value = data.Select(dcm_doc => dcm_doc.dv_nhanngoai).ToArray();
                    cmd.Parameters["NGUOI_SOANTHAO"].Value = data.Select(dcm_doc => dcm_doc.nguoi_soan).ToArray();
                    cmd.Parameters["CHUCVU_NGUOIKY"].Value = data.Select(dcm_doc => dcm_doc.chucvu_nguoiky).ToArray();
                    cmd.Parameters["DOC_NOTE"].Value = data.Select(dcm_doc => dcm_doc.doc_note).ToArray();
                    cmd.Parameters["VB_TRINH_KY"].Value = data.Select(dcm_doc => dcm_doc.vb_trinhky).ToArray();
                    cmd.Parameters["SO_TRINH_KY"].Value = data.Select(dcm_doc => dcm_doc.so_trinhky).ToArray();
                    cmd.Parameters["GHI_CHU"].Value = data.Select(dcm_doc => dcm_doc.ghi_chu).ToArray();
                    cmd.Parameters["NGAY_TRINH_KY"].Value = data.Select(dcm_doc => dcm_doc.ngay_trinhky).ToArray();
                    cmd.Parameters["NGAY_KY"].Value = data.Select(dcm_doc => dcm_doc.ngay_ky).ToArray();
                    cmd.Parameters["PROCESS_KEY"].Value = data.Select(dcm_doc => dcm_doc.process_key).ToArray();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void insert_Dcm_Doc_Relation (OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Doc_Relation> data)
        {
            try
            {
                if (data.Count > 0)
                {
                    OracleCommand cmd = oracleConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.ArrayBindCount = data.Count;

                    cmd.CommandText = string.Format(query, configs.schema);

                    cmd.Parameters.Add("ID", OracleDbType.Int64);
                    cmd.Parameters.Add("DCM_ID", OracleDbType.Int64);
                    cmd.Parameters.Add("DCM_DOCUMENT_ID", OracleDbType.Int64);

                    cmd.Parameters["ID"].Value = data.Select(dcm_doc_relation => dcm_doc_relation.id).ToArray();
                    cmd.Parameters["DCM_ID"].Value = data.Select(dcm_doc_relation => dcm_doc_relation.dcm_id).ToArray();
                    cmd.Parameters["DCM_DOCUMENT_ID"].Value = data.Select(dcm_doc_relation => dcm_doc_relation.dcm_document_id).ToArray();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void insert_fem_file(OracleConnection oracleConnection, Configs configs, string query, List<Fem_File> data)
        {
            try
            {
                if (data.Count > 0)
                {
                    OracleCommand cmd = oracleConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.ArrayBindCount = data.Count;

                    cmd.CommandText = string.Format(query, configs.schema);

                    cmd.Parameters.Add("ID", OracleDbType.Int64);
                    cmd.Parameters.Add("FILE_TYPE_ID", OracleDbType.Int64);
                    cmd.Parameters.Add("NAME", OracleDbType.Varchar2);
                    cmd.Parameters.Add("HDD_FILE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("DESCRIBE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("FILE_SIZE", OracleDbType.Int64);
                    cmd.Parameters.Add("IS_PRIVATE_FILE", OracleDbType.Int64);
                    cmd.Parameters.Add("CREATOR", OracleDbType.Varchar2);
                    cmd.Parameters.Add("IS_DELETED", OracleDbType.Int64);

                    cmd.Parameters["ID"].Value = data.Select(fem_file => fem_file.id).ToArray();
                    cmd.Parameters["FILE_TYPE_ID"].Value = data.Select(fem_file => fem_file.file_type_id).ToArray();
                    cmd.Parameters["NAME"].Value = data.Select(fem_file => fem_file.name).ToArray();
                    cmd.Parameters["HDD_FILE"].Value = data.Select(fem_file => fem_file.hdd_file).ToArray();
                    cmd.Parameters["DESCRIBE"].Value = data.Select(fem_file => fem_file.description).ToArray();
                    cmd.Parameters["FILE_SIZE"].Value = data.Select(fem_file => fem_file.file_size).ToArray();
                    cmd.Parameters["IS_PRIVATE_FILE"].Value = data.Select(fem_file => fem_file.is_private_file).ToArray();
                    cmd.Parameters["CREATOR"].Value = data.Select(fem_file => fem_file.creator).ToArray();
                    cmd.Parameters["IS_DELETED"].Value = data.Select(fem_file => fem_file.is_delete).ToArray();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void insert_Dcm_Attach_File(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Attach_File> data)
        {
            try
            {
                if (data.Count > 0)
                {
                    OracleCommand cmd = oracleConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.ArrayBindCount = data.Count;

                    cmd.CommandText = string.Format(query,configs.schema);

                    cmd.Parameters.Add("ATTACHMENT_ID", OracleDbType.Int64);
                    cmd.Parameters.Add("DOC_ID", OracleDbType.Int64);
                    cmd.Parameters.Add("TRANG_THAI", OracleDbType.Int64);
                    cmd.Parameters.Add("FILE_ID", OracleDbType.Int64);

                    cmd.Parameters["ATTACHMENT_ID"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.id).ToArray();
                    cmd.Parameters["DOC_ID"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.id_vb).ToArray();
                    cmd.Parameters["TRANG_THAI"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.trang_thai).ToArray();
                    cmd.Parameters["FILE_ID"].Value = data.Select(dcm_doc_attach_file => dcm_doc_attach_file.file_id).ToArray();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void insert_Dcm_Track(OracleConnection oracleConnection, Configs configs, string query, List<Dcm_Track> data)
        {
            try
            {
                if (data.Count > 0)
                {
                    OracleCommand cmd = oracleConnection.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.ArrayBindCount = data.Count;

                    cmd.CommandText = string.Format(query, configs.schema);

                    cmd.Parameters.Add("ID", OracleDbType.Int64);
                    cmd.Parameters.Add("DOC_ID", OracleDbType.Int64);
                    cmd.Parameters.Add("SCHEMA_ID", OracleDbType.Varchar2);
                    cmd.Parameters.Add("DOC_ID_SOURCE", OracleDbType.Int64);
                    cmd.Parameters.Add("SCHEMA_ID_SOURCE", OracleDbType.Varchar2);
                    cmd.Parameters.Add("DATE_INS", OracleDbType.Date);
                    cmd.Parameters.Add("PARENT", OracleDbType.Varchar2);
                    cmd.Parameters.Add("CHILD", OracleDbType.Varchar2);

                    cmd.Parameters["ID"].Value = data.Select(dcm_track => dcm_track.id).ToArray();
                    cmd.Parameters["DOC_ID"].Value = data.Select(dcm_track => dcm_track.doc_id).ToArray();
                    cmd.Parameters["SCHEMA_ID"].Value = data.Select(dcm_track => dcm_track.schema_id).ToArray();
                    cmd.Parameters["DOC_ID_SOURCE"].Value = data.Select(dcm_track => dcm_track.doc_id_source).ToArray();
                    cmd.Parameters["SCHEMA_ID_SOURCE"].Value = data.Select(dcm_track => dcm_track.schema_id_source).ToArray();
                    cmd.Parameters["DATE_INS"].Value = data.Select(dcm_track => dcm_track.date_ins).ToArray();
                    cmd.Parameters["PARENT"].Value = data.Select(dcm_track => dcm_track.parent).ToArray();
                    cmd.Parameters["CHILD"].Value = data.Select(dcm_track => dcm_track.child).ToArray();

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void resetListData()
        {
            dcm_Docs.Clear();
            dcm_Doc_Relations.Clear();
            fem_Files.Clear();
            dcm_Attach_Files.Clear();
        }
    }
}
