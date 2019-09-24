using Microsoft.EntityFrameworkCore;
using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IChiTietBanGiaoRepository : IRepository<ChiTietBanGiao>
    {
        Task<IEnumerable<ChiTietBanGiao>> ChiTietBanGiaoIncludeBanGiaoThietBi();
        Task<ChiTietBanGiao> FindIdIncludeBanGiaoThietBi(int? id);
        Task<IEnumerable<ChiTietBanGiao>> FindByBanGiaoIdIncludeBanGiaoThietBi(int? id);
    }
    public class ChiTietBanGiaoRepository : Repository<ChiTietBanGiao>, IChiTietBanGiaoRepository
    {
        public ChiTietBanGiaoRepository(QLTBITDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ChiTietBanGiao>> ChiTietBanGiaoIncludeBanGiaoThietBi()
        {

            return await _context.ChiTietBanGiaos.Include(x => x.BanGiao).Include(x => x.ThietBi).ToListAsync();
        }

        public async Task<ChiTietBanGiao> FindIdIncludeBanGiaoThietBi(int? id)
        {
            return await _context.ChiTietBanGiaos.Include(x => x.BanGiao).Include(x => x.ThietBi).SingleAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ChiTietBanGiao>> FindByBanGiaoIdIncludeBanGiaoThietBi(int? id)
        {
            IEnumerable<ChiTietBanGiao> result = await _context.ChiTietBanGiaos.Include(x => x.BanGiao).Include(x => x.ThietBi).Where(x => x.BanGiaoId == id).ToListAsync();
            return result;
        }
    }
}
