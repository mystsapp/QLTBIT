using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class CaiDatViewModel
    {
        public CaiDat CaiDat { get; set; }
        public BanGiao BanGiao { get; set; }
        public IEnumerable<PhanMem> PhanMems { get; set; }
        public string strUrl { get; set; }
    }
}
