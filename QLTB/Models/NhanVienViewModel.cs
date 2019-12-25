using Microsoft.AspNetCore.Identity;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class NhanVienViewModel
    {
        public NhanVien NhanVien { get; set; }

        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
        public IEnumerable<VanPhong> VanPhongs { get; set; }
    }
}
