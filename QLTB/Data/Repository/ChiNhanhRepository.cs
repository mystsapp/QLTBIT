using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IChiNhanhRepository: IRepository<ChiNhanh>
    {

    }
    public class ChiNhanhRepository : Repository<ChiNhanh>, IChiNhanhRepository
    {
        public ChiNhanhRepository(QLTBITDbContext context) : base(context)
        {
        }
    }
}
