
using System;

namespace Convert_Data.Models
{
    class Dcm_Doc : IDCM_
    {
        public int id_VanBan { get; set; } = -1;
        public int? congvan_dendi { get; set; } = null;
        public DateTime? ngay_tao { get; set; } = null;
        public string nguoi_vaoso { get; set; } = "";
        public string trich_yeu { get; set; } = "";
        public string so_kyhieu { get; set; } = "";
        public string coquan_banhanh { get; set; } = "";
        public string dcmtype_code { get; set; } = "";
        public string priority_code { get; set; } = "";
        public string linhvuc_code { get; set; } = "";
        public string so_vanban_code { get; set; } = "";
        public DateTime? ngay_van_ban { get; set; } = null;
        public DateTime? ngay_den_di { get; set; } = null;
        public DateTime? ngay_ban_hanh { get; set; } = null;
        public string nguoi_ky_chinh { get; set; } = "";
        public int so_trang { get; set; } = 0;
        public int so_ban { get; set; } = 0;
        public string filesDinhKem { get; set; } = "";
        public string id_vblqs { get; set; } = "";
        public string dv_nhanngoai { get; set; } = "";
        public string nguoi_soan { get; set; } = "";
        public string confidential_code { get; set; } = "";
        public string donvi_soanthao { get; set; } = "";
        public string chucvu_nguoiky { get; set; } = "";
        public string ghi_chu { get; set; } = "";
        public string doc_note { get; set; } = "";
        public int? vb_trinhky { get; set; } = null;
        public int? so_trinhky { get; set; } = null;
        public DateTime? ngay_trinhky { get; set; } = null;
        public DateTime? ngay_ky { get; set; } = null;
        public Int64 so_den_di { get; set; } = 0;
        public string hinhthuc_gui_code { get; set; } = "";
        public int? unit_id { get; set; } = null;
        public int? trang_thai { get; set; } = null;
        public DateTime? han_giaiquyet { get; set; } = null;
        public string process_key { get; set; } = "";
    }
}
