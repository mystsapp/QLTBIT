using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class PhanMemViewModel
    {
        public PhanMem PhanMem { get; set; }

        public IEnumerable<LoaiPhanMem> LoaiPhanMems { get; set; }
    }
}
