using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class CaiDatIndexViewModel
    {
        public IEnumerable<CaiDat> CaiDats { get; set; }
        public BanGiao BanGiao { get; set; }
        public string StrUrl { get; set; }
    }
}
