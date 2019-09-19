using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IChiTietBanGiaoRepository: IRepository<ChiTietBanGiao>
    {

    }
    public class ChiTietBanGiaoRepository : Repository<ChiTietBanGiao>, IChiTietBanGiaoRepository
    {
        public ChiTietBanGiaoRepository(QLTBITDbContext context) : base(context)
        {
        }
    }
}
