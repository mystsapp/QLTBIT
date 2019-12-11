using QLTB.Data.Models;

namespace QLTB.Models
{
    public class NhapKhoViewModel
    {
        public NhapKho NhapKho { get; set; }
        public BanGiao BanGiao { get; set; }
        public ChiTietBanGiao ChiTietBanGiao { get; set; }
        public string strUrl { get; set; }
        public string khuVuc { get; set; }
    }
}
