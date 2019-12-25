using Microsoft.EntityFrameworkCore;
using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IBanGiaoRepository: IRepository<BanGiao>
    {
        //Task<List<BanGiao>> BanGiaoIncludeChiNhanh();
        Task<BanGiao> FindByIdIncludeVanPhong(int? id);
    }
    public class BanGiaoRepository : Repository<BanGiao>, IBanGiaoRepository
    {
        public BanGiaoRepository(QLTBITDbContext context) : base(context)
        {
        }

        //public async Task<List<BanGiao>> BanGiaoIncludeChiNhanh()
        //{
        //    return await _context.BanGiaos.Include(x => x.ChiNhanh).ToListAsync();
        //}

        public async Task<BanGiao> FindByIdIncludeVanPhong(int? id)
        {
            return await _context.BanGiaos.Include(x => x.VanPhong).SingleAsync(x => x.Id == id);
        }
    }
}
