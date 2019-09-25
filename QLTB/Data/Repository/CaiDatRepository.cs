using Microsoft.EntityFrameworkCore;
using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface ICaiDatRepository : IRepository<CaiDat>
    {
        Task<IEnumerable<CaiDat>> CaiDatIncludeBanGiaoPhanMem();
        Task<CaiDat> FindByIdIncludeBanGiaoPhanMem(int? id);
        Task<IEnumerable<CaiDat>> FindByBanGiaoIdIncludeBanGiaoPhanMem(int? id);
    }
    public class CaiDatRepository : Repository<CaiDat>, ICaiDatRepository
    {
        public CaiDatRepository(QLTBITDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CaiDat>> CaiDatIncludeBanGiaoPhanMem()
        {

            return await _context.CaiDats.Include(x => x.BanGiao).Include(x => x.PhanMem).ToListAsync();
        }

        public async Task<CaiDat> FindByIdIncludeBanGiaoPhanMem(int? id)
        {
            return await _context.CaiDats.Include(x => x.BanGiao).Include(x => x.PhanMem).SingleAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CaiDat>> FindByBanGiaoIdIncludeBanGiaoPhanMem(int? id)
        {
            IEnumerable<CaiDat> result = await _context.CaiDats.Include(x => x.BanGiao).Include(x => x.PhanMem).Where(x => x.BanGiaoId == id).ToListAsync();
            return result;
        }
    }
}
