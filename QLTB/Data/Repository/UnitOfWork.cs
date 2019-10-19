using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        IBanGiaoRepository banGiaoRepository { get; }
        IChiNhanhRepository chiNhanhRepository { get; }
        IChiTietBanGiaoRepository chiTietBanGiaoRepository { get; }
        ICaiDatRepository caiDatRepository { get; }
        ILoaiPhanMemRepository loaiPhanMemRepository { get; }
        ILoaiThietBiRepository loaiThietBiRepository { get; }
        INhanVienRepository nhanVienRepository { get; }
        IPhanMemRepository phanMemRepository { get; }
        IThietBiRepository thietBiRepository { get; }
        IVanPhongRepository vanPhongRepository { get; }
        INhapKhoRepository nhapKhoRepository { get; }
        Task<int> Complete();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QLTBITDbContext _context;

        public UnitOfWork(QLTBITDbContext context)
        {
            _context = context;
            banGiaoRepository = new BanGiaoRepository(_context);
            chiNhanhRepository = new ChiNhanhRepository(_context);
            chiTietBanGiaoRepository = new ChiTietBanGiaoRepository(_context);
            caiDatRepository = new CaiDatRepository(_context);
            loaiPhanMemRepository = new LoaiPhanMemRepository(_context);
            loaiThietBiRepository = new LoaiThietBiRepository(_context);
            nhanVienRepository = new NhanVienRepository(_context);
            phanMemRepository = new PhanMemRepository(_context);
            thietBiRepository = new ThietBiRepository(_context);
            vanPhongRepository = new VanPhongRepository(_context);
            nhapKhoRepository = new NhapKhoRepository(_context);
        }
        public IBanGiaoRepository banGiaoRepository { get; }

        public IChiNhanhRepository chiNhanhRepository { get; }

        public IChiTietBanGiaoRepository chiTietBanGiaoRepository { get; }
        public ICaiDatRepository caiDatRepository { get; }

        public ILoaiPhanMemRepository loaiPhanMemRepository { get; }

        public ILoaiThietBiRepository loaiThietBiRepository { get; }

        public INhanVienRepository nhanVienRepository { get; }

        public IPhanMemRepository phanMemRepository { get; }

        public IThietBiRepository thietBiRepository { get; }

        public IVanPhongRepository vanPhongRepository { get; }

        public INhapKhoRepository nhapKhoRepository { get; }

        public async Task<int> Complete()
        {
            var a = await _context.SaveChangesAsync();
            return a;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
