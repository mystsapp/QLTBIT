using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Utility;

namespace QLTB.Controllers
{
    public class ThongKesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public ThongKesController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TheoVP()
        {
            var listVP = _unitOfWork.vanPhongRepository.GetAll();

            var roles = await userManager.GetRolesAsync(await userManager.GetUserAsync(User));

            List<VanPhong> vanPhongs = new List<VanPhong>();

            foreach (var role in roles)
            {
                var cns = _unitOfWork.chiNhanhRepository.GetAll().Where(x => x.KhuVuc == role);

                foreach (var cn in cns)
                {


                    var b = listVP.Where(x => x.ChiNhanhId == cn.Id);
                    vanPhongs.AddRange(b);
                }
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
            {

                listVP = vanPhongs;
            }

            ViewBag.VPs = listVP;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TheoVPPartial(string searchFromDate = null, string searchToDate = null, string vP = null)
        {
            var chitiets = await _unitOfWork.chiTietBanGiaoRepository.GetAllIncludeAsync(x => x.BanGiao, y => y.ThietBi);

            if (searchFromDate != null && searchToDate != null)
            {
                DateTime fromDate = DateTime.Parse(searchFromDate);
                DateTime toDate = DateTime.Parse(searchToDate);
                chitiets = chitiets.Where(x => x.NgayGiao >= fromDate && x.NgayGiao <= toDate.AddDays(1));
            }



            List<ChiTietBanGiao> chiTietBanGiaos = new List<ChiTietBanGiao>();


            if (!string.IsNullOrEmpty(vP))
            {

                var a = chitiets.Where(x => x.BanGiao.VanPhong.Equals(vP));
                if (a.Count() > 0)
                {
                    chiTietBanGiaos.AddRange(a);
                }
            }


            chitiets = chiTietBanGiaos;



            return PartialView(chitiets);
        }


        [HttpPost]
        public async Task<IActionResult> ExportVP(string searchFromDate = null, string searchToDate = null, string vP = null)
        {
            DateTime fromDate = DateTime.Parse(searchFromDate);
            DateTime toDate = DateTime.Parse(searchToDate);

            var vanPhong = await _unitOfWork.vanPhongRepository.FindIncludeChiNhanh(vP);

            var arrayName = vanPhong.ChiNhanh.Name.Split(" ");
            string name = arrayName[1] + " " + arrayName[2];



            byte[] fileContents;

            ExcelPackage ExcelApp = new ExcelPackage();
            ExcelWorksheet xlSheet = ExcelApp.Workbook.Worksheets.Add("Report");
            // Định dạng chiều dài cho cột
            xlSheet.Column(1).Width = 10;// STT
            xlSheet.Column(2).Width = 25;// Ten thiet bi
            xlSheet.Column(3).Width = 20;// Phong ban
            xlSheet.Column(4).Width = 10;// Ma so
            xlSheet.Column(5).Width = 25;// Ngay nhan
            xlSheet.Column(6).Width = 20;// Nguoi nhan
            xlSheet.Column(7).Width = 20;// Ghi chu

            xlSheet.Cells[1, 1].Value = "CÔNG TY TNHH MỘT THÀNH VIÊN";
            xlSheet.Cells[2, 1].Value = " DỊCH VỤ LỮ HÀNH SAIGONTOURIST ";
            xlSheet.Cells[1, 1, 1, 4].Merge = true;
            xlSheet.Cells[2, 1, 2, 4].Merge = true;
            setCenterAligment(1, 1, 1, 4, xlSheet);
            setCenterAligment(2, 1, 2, 4, xlSheet);
            //xlSheet.Row(1).Height = 40;
            if (vanPhong.ChiNhanh.MaChiNhanh != "STS")
            {
                xlSheet.Cells[3, 1].Value = "CHI NHÁNH CÔNG TY TNHH MỘT THÀNH VIÊN ";
                xlSheet.Cells[4, 1].Value = " DỊCH VỤ LỮ HÀNH SAIGONTOURIST TẠI " + name + " - VP " + vanPhong.Name;

                xlSheet.Cells[3, 1, 3, 4].Merge = true;
                setCenterAligment(3, 1, 3, 4, xlSheet);
                setFontBold(3, 1, 3, 4, 12, xlSheet);

                xlSheet.Cells[4, 1, 4, 4].Merge = true;
                setCenterAligment(4, 1, 4, 4, xlSheet);
                setFontBold(4, 1, 4, 4, 12, xlSheet);

                xlSheet.Cells[5, 1].Value = "*************";
                xlSheet.Cells[5, 1, 5, 4].Merge = true;
                setCenterAligment(5, 1, 5, 4, xlSheet);
            }
            else
            {
                xlSheet.Cells[3, 1].Value = "VĂN PHÒNG STS: " + vP;

                xlSheet.Cells[3, 1, 3, 4].Merge = true;
                setCenterAligment(3, 1, 3, 4, xlSheet);
                setFontBold(3, 1, 3, 4, 12, xlSheet);

                xlSheet.Cells[4, 1].Value = "*************";
                xlSheet.Cells[4, 1, 4, 4].Merge = true;
                setCenterAligment(4, 1, 4, 4, xlSheet);
            }





            xlSheet.Cells[1, 5].Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
            xlSheet.Cells[1, 5, 1, 7].Merge = true;
            setCenterAligment(1, 5, 1, 7, xlSheet);

            xlSheet.Cells[2, 5].Value = "Độc lập - Tự do - Hạnh phúc";
            xlSheet.Cells[2, 5, 2, 7].Merge = true;
            setCenterAligment(2, 5, 2, 7, xlSheet);

            xlSheet.Cells[3, 5].Value = "*************";
            xlSheet.Cells[3, 5, 3, 7].Merge = true;
            setCenterAligment(3, 5, 3, 7, xlSheet);


            xlSheet.Cells[6, 1].Value = "KIỂM KÊ CÔNG CỤ LAO ĐỘNG VP " + vanPhong.Name;
            xlSheet.Cells[6, 1].Style.Font.SetFromFont(new Font("Times New Roman", 15, FontStyle.Bold));
            xlSheet.Cells[6, 1, 6, 7].Merge = true;
            setCenterAligment(6, 1, 6, 7, xlSheet);


            if (fromDate == toDate)
            {
                xlSheet.Cells[7, 1].Value = "Từ ngày: " + fromDate.ToString("dd/MM/yyyy");
            }
            else
            {
                xlSheet.Cells[7, 1].Value = "Từ ngày: " + fromDate.ToString("dd/MM/yyyy") + " đến " + toDate.ToString("dd/MM/yyyy");
            }
            xlSheet.Cells[7, 1].Style.Font.SetFromFont(new Font("Times New Roman", 15, FontStyle.Bold));
            xlSheet.Cells[7, 1, 7, 7].Merge = true;
            setCenterAligment(7, 1, 7, 7, xlSheet);

            // dinh dang tu ngay den ngay
            //if (tungay == denngay)
            //{
            //    fromTo = "Ngày: " + tungay;
            //}
            //else
            //{
            //    fromTo = "Từ ngày: " + tungay + " đến ngày: " + denngay;
            //}

            //xlSheet.Cells[3, 1].Value = fromTo;
            //xlSheet.Cells[3, 1, 3, 7].Merge = true;
            //xlSheet.Cells[3, 1].Style.Font.SetFromFont(new Font("Times New Roman", 14, FontStyle.Bold));
            //setCenterAligment(3, 1, 3, 7, xlSheet);

            // Tạo header
            xlSheet.Cells[9, 1].Value = "STT";
            xlSheet.Cells[9, 2].Value = "Tên thiết bị";
            xlSheet.Cells[9, 3].Value = "Bộ phận";
            xlSheet.Cells[9, 4].Value = "Mã số";
            xlSheet.Cells[9, 5].Value = "Ngày sử dụng";
            xlSheet.Cells[9, 6].Value = "Người dùng";
            xlSheet.Cells[9, 7].Value = "Ghi chú";

            //xlSheet.Cells[9, 1, 9, 7].Style.Font.SetFromFont(new Font("Times New Roman", 12, FontStyle.Bold));

            // do du lieu tu table
            int dong = 9;


            DataTable dt = await _unitOfWork.thongKeRepository.TheoVP_Report(searchFromDate, searchToDate, vP);


            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dong++;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {

                        //xlSheet.Cells[dong, 1].Value = i + 1;
                        //var a = dt.Rows[i][j];
                        //xlSheet.Cells[dong, j + 2].Value = a;

                        //if (String.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                        //{
                        //    xlSheet.Cells[dong, j + 1].Value = "";
                        //}
                        //else
                        //{
                            //if (j == 0)
                                xlSheet.Cells[dong, 1].Value = i + 1;
                            //else
                            //{
                                var a = dt.Rows[i][j];
                                xlSheet.Cells[dong, j + 2].Value = a;
                            //}
                        //}
                    }
                }
            }
            else
            {
                SetAlert("Không có dữ liệu trong bảng.", "warning");
                return RedirectToAction("TheoVP", "ThongKes");
            }
            dong++;
            //// Merger cot 4,5 ghi tổng tiền
            //setRightAligment(dong, 3, dong, 3, xlSheet);
            //xlSheet.Cells[dong, 1, dong, 2].Merge = true;
            //xlSheet.Cells[dong, 1].Value = "Tổng tiền: ";

            // Sum tổng tiền

            //xlSheet.Cells[dong, 5].Value = "TC";
            //xlSheet.Cells[dong, 6].Formula = "SUM(F6:F" + (6 + dt.Rows.Count - 1) + ")";
            //xlSheet.Cells[dong, 7].Formula = "SUM(G6:G" + (6 + dt.Rows.Count - 1) + ")";

            // định dạng số

            //NumberFormat(dong, 6, dong, 7, xlSheet);


            setBorder(9, 1, 9 + dt.Rows.Count, 7, xlSheet);
            setFontBold(9, 1, 9, 7, 12, xlSheet);
            setFontSize(10, 1, 10 + dt.Rows.Count, 7, 12, xlSheet);



            // dinh dang giua cho cot stt
            setCenterAligment(9, 1, 9 + dt.Rows.Count, 1, xlSheet);

            // setBorder(dong, 5, dong, 15, xlSheet);
            //setFontBold(dong, 5, dong, 15, 12, xlSheet);

            // dinh dạng ngay thang cho cot ngay di , ngay ve
            DateFormat(10, 5, 10 + dt.Rows.Count, 5, xlSheet);

            // canh giưa cot  ngay di, ngay ve, so khach 
            //setCenterAligment(6, 3, 6 + dt.Rows.Count, 3, xlSheet);
            // setCenterAligment(6, 4, 6 + dt.Rows.Count, 4, xlSheet);
            // dinh dạng number cot doanh so
            // NumberFormat(6, 9, 6 + dt.Rows.Count, 9, xlSheet);
            // NumberFormat(6, 12, 6 + dt.Rows.Count, 12, xlSheet);

            //xlSheet.View.FreezePanes(6, 20);

            try
            {
                fileContents = ExcelApp.GetAsByteArray();
                return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "TheoNgayVP_" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + ".xlsx");

            }
            catch (Exception)
            {

                throw;
            }


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////


        private static void NumberFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                range.Style.Numberformat.Format = "#,#0";
            }
        }
        private static void DateFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Numberformat.Format = "dd/MM/yyyy";
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }
        private static void DateTimeFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }
        private static void setRightAligment(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }
        }
        private static void setCenterAligment(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }
        private static void setFontSize(int fromRow, int fromColumn, int toRow, int toColumn, int fontSize, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Font.SetFromFont(new Font("Times New Roman", fontSize, FontStyle.Regular));
            }
        }
        private static void setFontBold(int fromRow, int fromColumn, int toRow, int toColumn, int fontSize, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Font.SetFromFont(new Font("Times New Roman", fontSize, FontStyle.Bold));
            }
        }
        private static void setBorder(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }
        }
        private static void PhantramFormat(int fromRow, int fromColumn, int toRow, int toColumn, ExcelWorksheet sheet)
        {
            using (var range = sheet.Cells[fromRow, fromColumn, toRow, toColumn])
            {
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                range.Style.Numberformat.Format = "0 %";
            }
        }

        public double MeasureTextHeight(string text, ExcelFont font, int width)
        {
            if (string.IsNullOrEmpty(text)) return 0.0;
            var bitmap = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(bitmap);

            var pixelWidth = Convert.ToInt32(width * 7.5);  //7.5 pixels per excel column width
            var drawingFont = new Font(font.Name, font.Size);
            var size = graphics.MeasureString(text, drawingFont, pixelWidth);

            //72 DPI and 96 points per inch.  Excel height in points with max of 409 per Excel requirements.
            return Math.Min(Convert.ToDouble(size.Height) * 72 / 96, 409);
        }

        private void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

    }
}