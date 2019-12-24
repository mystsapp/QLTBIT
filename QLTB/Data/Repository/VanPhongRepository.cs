﻿using Microsoft.EntityFrameworkCore;
using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IVanPhongRepository: IRepository<VanPhong>
    {
        Task<IEnumerable<VanPhong>> VanPhongIncludeChiNhanh();
        Task<VanPhong> FindIdIncludeChiNhanh(int? id);
        Task<VanPhong> FindIncludeChiNhanh(string name);
    }
    public class VanPhongRepository : Repository<VanPhong>, IVanPhongRepository
    {
        public VanPhongRepository(QLTBITDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<VanPhong>> VanPhongIncludeChiNhanh()
        {

            return await _context.VanPhongs.Include(x => x.ChiNhanh).ToListAsync();
        }

        public async Task<VanPhong> FindIdIncludeChiNhanh(int? id)
        {
            return await _context.VanPhongs.Include(x => x.ChiNhanh).SingleAsync(x => x.Id == id);
        }
        public async Task<VanPhong> FindIncludeChiNhanh(string name)
        {
            return await _context.VanPhongs.Include(x => x.ChiNhanh).SingleAsync(x => x.Name == name);
        }
    }
}
