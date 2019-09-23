using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class BanGiaoCreateViewModel
    {
        public BanGiao BanGiao { get; set; }

        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
        public IEnumerable<VanPhong> VanPhongs { get; set; }
        public IEnumerable<LoaiThietBi> LoaiThietBis { get; set; }
        public IEnumerable<NhanVien> NhanViens { get; set; }

    }
}
