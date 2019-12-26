using QLTB.Data.Interfaces;
using QLTB.Data.Models;
using QLTB.Models;
using QLTB.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QLTB.Data.Repository
{
    public interface IThongKeRepository : IRepository<ChiTietBanGiao>
    {
        //Task<DataTable> TheoVP_Report(string searchFromDate, string searchToDate, string vP);
    }
    public class ThongKeRepository : Repository<ChiTietBanGiao>, IThongKeRepository
    {
        public ThongKeRepository(QLTBITDbContext context) : base(context)
        {
        }

        //public async Task<DataTable> TheoVP_Report(string searchFromDate, string searchToDate, string vP)
        //{
        //    var chitiets = await GetAllIncludeAsync(x => x.BanGiao, y => y.ThietBi);

        //    //if (searchFromDate != null && searchToDate != null)
        //    //{
        //    DateTime fromDate = DateTime.Parse(searchFromDate);
        //    DateTime toDate = DateTime.Parse(searchToDate);
        //    chitiets = chitiets.Where(x => x.NgayGiao >= fromDate && x.NgayGiao <= toDate.AddDays(1));
        //    //}
            

        //    List<ChiTietBanGiao> chiTietBanGiaos = new List<ChiTietBanGiao>();


        //    if (!string.IsNullOrEmpty(vP))
        //    {

        //        var a = chitiets.Where(x => x.BanGiao.VanPhong.Equals(vP));
        //        if (a.Count() > 0)
        //        {
        //            chiTietBanGiaos.AddRange(a);
        //        }
        //    }
        //    chitiets = chiTietBanGiaos;
        //    chitiets = chitiets.Where(x => x.ChuyenSuDung.ToString().Equals("true"));
        //    List<TheoVPViewModel> listVM = new List<TheoVPViewModel>();

        //    foreach (var chitiet in chitiets)
        //    {
        //        TheoVPViewModel theoVPViewModel = new TheoVPViewModel()
        //        {

        //            TenThietBi = chitiet.ThietBi.Name,
        //            BoPhan = chitiet.BanGiao.PhongBan,
        //            MaSo = chitiet.MaSo,
        //            NgaySuDung = chitiet.NgayGiao,
        //            NguoiDung = chitiet.BanGiao.NguoiNhan,
        //            GhiChu = chitiet.GhiChu
        //        };
                
        //        listVM.Add(theoVPViewModel);
        //    }



        //    DataTable dt = new DataTable();

        //    var count = chitiets.Count();

        //    dt = EntityToTable.ToDataTable(listVM);
        //    if (dt.Rows.Count > 0)
        //        return dt;
        //    else
        //        return null;

        //}
    }
}
