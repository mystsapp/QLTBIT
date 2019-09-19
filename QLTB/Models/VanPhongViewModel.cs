using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class VanPhongViewModel
    {
        public VanPhong VanPhong { get; set; }

        public IEnumerable<ChiNhanh> ChiNhanhs { get; set; }
    }
}
