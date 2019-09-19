using Microsoft.EntityFrameworkCore;
using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IPhanMemRepository: IRepository<PhanMem>
    {
        Task<IEnumerable<PhanMem>> PhanMemIncludeLoaiPhanMem();
        Task<PhanMem> FindIdIncludeLoaiPhanMem(int? id);
    }
    public class PhanMemRepository : Repository<PhanMem>, IPhanMemRepository
    {
        public PhanMemRepository(QLTBITDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PhanMem>> PhanMemIncludeLoaiPhanMem()
        {

            return await _context.PhanMems.Include(x => x.LoaiPhanMem).ToListAsync();
        }

        public async Task<PhanMem> FindIdIncludeLoaiPhanMem(int? id)
        {
            return await _context.PhanMems.Include(x => x.LoaiPhanMem).SingleAsync(x => x.Id == id);
        }
    }
}
