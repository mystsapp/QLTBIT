using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class ThietBiViewModel
    {
        public ThietBi ThietBi { get; set; }

        public IEnumerable<LoaiThietBi> LoaiThietBis { get; set; }
    }
}
