using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface ILoaiThietBiRepository: IRepository<LoaiThietBi>
    {

    }
    public class LoaiThietBiRepository : Repository<LoaiThietBi>, ILoaiThietBiRepository
    {
        public LoaiThietBiRepository(QLTBITDbContext context) : base(context)
        {
        }
    }
}
