using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface ILoaiPhanMemRepository: IRepository<LoaiPhanMem>
    {

    }
    public class LoaiPhanMemRepository : Repository<LoaiPhanMem>, ILoaiPhanMemRepository
    {
        public LoaiPhanMemRepository(QLTBITDbContext context) : base(context)
        {
        }
    }
}
