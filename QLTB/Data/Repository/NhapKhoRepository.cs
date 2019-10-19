using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface INhapKhoRepository: IRepository<NhapKho>
    {

    }
    public class NhapKhoRepository : Repository<NhapKho>, INhapKhoRepository
    {
        public NhapKhoRepository(QLTBITDbContext context) : base(context)
        {
        }
    }
}
