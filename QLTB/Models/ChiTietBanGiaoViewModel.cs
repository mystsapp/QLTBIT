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
        public string strUrl { get; set; }
    }
}
