using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class BanGiaoViewModel
    {
        public IEnumerable<BanGiao> BanGiaos { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string strUrl { get; set; }
    }
}
