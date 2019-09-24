using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class ChiTietBanGiaoIndexViewModel
    {
        public IEnumerable<ChiTietBanGiao> ChiTietBanGiaos { get; set; }
        public BanGiao BanGiao { get; set; }
        public string StrUrl { get; set; }
    }
}
