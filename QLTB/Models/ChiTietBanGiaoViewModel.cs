using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class ChiTietBanGiaoViewModel
    {
        public ChiTietBanGiao ChiTietBanGiao { get; set; }
        public BanGiao BanGiao { get; set; }
        public IEnumerable<ThietBi> ThietBis { get; set; }
        public IEnumerable<NhanVien> NhanViens { get; set; }
        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
        public IEnumerable<LoaiThietBi> LoaiThietBis { get; set; }
        public string strUrl { get; set; }

        public int Id { get; set; }
    }
}
