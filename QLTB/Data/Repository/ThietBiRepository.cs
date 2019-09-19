using Microsoft.EntityFrameworkCore;
using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IThietBiRepository: IRepository<ThietBi>
    {
        Task<IEnumerable<ThietBi>> ThietBiWithLoaiThietBi();
        Task<ThietBi> FindIdWithLoaiThietBi(int? id);
        //ThietBi GetByIdNoTracking(int id);
    }
    public class ThietBiRepository : Repository<ThietBi>, IThietBiRepository
    {
        public ThietBiRepository(QLTBITDbContext context) : base(context)
        {
        }

        public async Task<ThietBi> FindIdWithLoaiThietBi(int? id)
        {
            return await _context.ThietBis.Include(x => x.LoaiThietBi).SingleAsync(x => x.Id == id);
        }

        //public ThietBi GetByIdNoTracking(int id)
        //{
        //    return _context.ThietBis.AsNoTracking().SingleOrDefault(x => x.Id == id);
        //}

        public async Task<IEnumerable<ThietBi>> ThietBiWithLoaiThietBi()
        {
            
            return await _context.ThietBis.Include(x => x.LoaiThietBi).ToListAsync();
        }
    }
}
