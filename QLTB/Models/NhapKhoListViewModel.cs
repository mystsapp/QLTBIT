using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Models
{
    public class NhapKhoListViewModel
    {
        public NhapKho NhapKho { get; set; }
        public IEnumerable<NhapKho> NhapKhos { get; set; }
        public int Id { get; set; }
    }
}
