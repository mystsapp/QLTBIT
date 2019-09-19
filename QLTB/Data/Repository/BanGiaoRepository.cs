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

    }
    public class BanGiaoRepository : Repository<BanGiao>, IBanGiaoRepository
    {
        public BanGiaoRepository(QLTBITDbContext context) : base(context)
        {
        }
    }
}
