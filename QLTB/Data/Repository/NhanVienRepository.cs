using Microsoft.EntityFrameworkCore;
using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface INhanVienRepository: IRepository<NhanVien>
    {
        Task<IEnumerable<NhanVien>> NhanVienIncludeChiNhanh();
        Task<NhanVien> FindIdIncludeChiNhanh(int? id);
    }
    public class NhanVienRepository : Repository<NhanVien>, INhanVienRepository
    {
        public NhanVienRepository(QLTBITDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<NhanVien>> NhanVienIncludeChiNhanh()
        {

            return await _context.NhanViens.Include(x => x.ChiNhanh).ToListAsync();
        }

        public async Task<NhanVien> FindIdIncludeChiNhanh(int? id)
        {
            return await _context.NhanViens.Include(x => x.ChiNhanh).SingleAsync(x => x.Id == id);
        }
    }
}
