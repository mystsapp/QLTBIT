using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Novacode;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;
using QLTB.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTB.Controllers
{
    public class BanGiaosController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public BanGiaoCreateViewModel BanGiaoCreateVM { get; set; }

        private int PageSize = 10;

        public BanGiaosController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, HostingEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            BanGiaoCreateVM = new BanGiaoCreateViewModel()
            {
                
                VanPhongs = _unitOfWork.vanPhongRepository.GetAll(),
                LoaiThietBis = _unitOfWork.loaiThietBiRepository.GetAll(),
                BanGiao = new Data.Models.BanGiao()
            };
        }

        public async Task<IActionResult> Index(int banGiaoPage = 1, string searchNguoiNhan = null, string searchNguoiLap = null, string searchVanPhong = null, string searchDate = null)
        {
            string strUrl = UriHelper.GetDisplayUrl(Request);
            BanGiaoViewModel banGiaoVM = new BanGiaoViewModel()
            {
                BanGiaos = new List<BanGiao>(),
                strUrl = strUrl
            };

            /////////////////////////////////////////////////Pagingnation/////////////////////////////////////////////////
            StringBuilder param = new StringBuilder();
            param.Append("/BanGiaos?banGiaoPage=:");

            param.Append("&searchNguoiNhan=");
            if (searchNguoiNhan != null)
            {
                param.Append(searchNguoiNhan);
            }

            param.Append("&searchNguoiLap=");
            if (searchNguoiLap != null)
            {
                param.Append(searchNguoiLap);
            }

            param.Append("&searchPhone=");
            if (searchVanPhong != null)
            {
                param.Append(searchVanPhong);
            }

            param.Append("&searchDate=");
            if (searchDate != null)
            {
                param.Append(searchDate);
            }
            /////////////////////////////////////////////////Pagingnation/////////////////////////////////////////////////

            banGiaoVM.BanGiaos = await _unitOfWork.banGiaoRepository.GetAllIncludeOneAsync(x => x.VanPhong);

            var roles = await userManager.GetRolesAsync(await userManager.GetUserAsync(User));

            var listBG = new List<BanGiao>();

            foreach (var role in roles)
            {
                var banGiao = banGiaoVM.BanGiaos.Where(x => x.VanPhong.KhuVuc == role);
                listBG.AddRange(banGiao);
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
            {
                banGiaoVM.BanGiaos = listBG;
            }

            /////////// search ///////////////
            if (searchNguoiNhan != null)
            {
                banGiaoVM.BanGiaos = banGiaoVM.BanGiaos.Where(x => x.NguoiNhan.ToLower().Contains(searchNguoiNhan.ToLower())).ToList();
            }

            if (searchNguoiLap != null)
            {
                banGiaoVM.BanGiaos = banGiaoVM.BanGiaos.Where(x => x.NguoiLap.ToLower().Contains(searchNguoiLap.ToLower())).ToList();
            }

            if (searchVanPhong != null)
            {
                banGiaoVM.BanGiaos = banGiaoVM.BanGiaos.Where(x => x.VanPhong.Name.ToLower().Contains(searchVanPhong.ToLower())).ToList();
            }

            if (searchDate != null)
            {
                try
                {
                    DateTime bgDate = Convert.ToDateTime(searchDate);
                    banGiaoVM.BanGiaos = banGiaoVM.BanGiaos.Where(x => x.NgayTao.ToShortDateString().Equals(bgDate.ToShortDateString())).ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            /////////// search ///////////////

            ///////////////////////////////////////Pagingnation/////////////////////////////////////////////////
            var count = banGiaoVM.BanGiaos.Count();

            banGiaoVM.BanGiaos = banGiaoVM.BanGiaos.OrderBy(x => x.NgayTao)
                .Skip((banGiaoPage - 1) * PageSize)
                .Take(PageSize).ToList();

            banGiaoVM.PagingInfo = new PagingInfo()
            {
                CurrentPage = banGiaoPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString()
            };
            ///////////////////////////////////////Pagingnation/////////////////////////////////////////////////

            return View(banGiaoVM);
        }

        [Authorize(Policy = "CreateCNRolePolicy")]
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);
            BanGiaoCreateVM.BanGiao.NguoiLap = user.UserName;
            BanGiaoCreateVM.NhanViens = await _unitOfWork.nhanVienRepository.GetAllIncludeOneAsync(x => x.VanPhong);

            var roles = await userManager.GetRolesAsync(user);

            var listVanPhong = new List<VanPhong>();

            var listNv = new List<NhanVien>();

            foreach (var role in roles)
            {

                //listChiNhanh.AddRange(BanGiaoCreateVM.ChiNhanhs.Where(x => x.KhuVuc == role));

                //listNv.AddRange(BanGiaoCreateVM.NhanViens.Where(x => x.ChiNhanh.KhuVuc == role));

                listNv.AddRange(BanGiaoCreateVM.NhanViens.Where(x => x.VanPhong.KhuVuc == role));
                listVanPhong.AddRange(BanGiaoCreateVM.VanPhongs.Where(x => x.KhuVuc == role));
            }
  
            
            if (!User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
            {
                BanGiaoCreateVM.VanPhongs = listVanPhong;
                BanGiaoCreateVM.NhanViens = listNv;
            }

            return View(BanGiaoCreateVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {


            if (!ModelState.IsValid)
            {
                return View(BanGiaoCreateVM);
            }

            BanGiaoCreateVM.BanGiao.NgayTao = DateTime.Now;


            _unitOfWork.banGiaoRepository.Create(BanGiaoCreateVM.BanGiao);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        // Get: Edit method
        [Authorize(Policy = "EditCNRolePolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            BanGiaoCreateVM.BanGiao = await _unitOfWork.banGiaoRepository.FindByIdIncludeVanPhong(id);

            if (BanGiaoCreateVM.BanGiao == null)
                return NotFound();

            return View(BanGiaoCreateVM);
        }

        // Post: Eidt method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != BanGiaoCreateVM.BanGiao.Id)
                return NotFound();

            if (ModelState.IsValid)
            {

                BanGiaoCreateVM.BanGiao.NgaySua = DateTime.Now;



                _unitOfWork.banGiaoRepository.Update(BanGiaoCreateVM.BanGiao);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }

            return View(BanGiaoCreateVM);
        }

        // Get: Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            BanGiaoCreateVM.BanGiao = await _unitOfWork.banGiaoRepository.FindByIdIncludeVanPhong(id);

            if (BanGiaoCreateVM.BanGiao == null)
                return NotFound();

            return View(BanGiaoCreateVM);
        }

        // Get: Delete method
        [Authorize(Policy = "DeleteCNRolePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            BanGiaoCreateVM.BanGiao = await _unitOfWork.banGiaoRepository.FindByIdIncludeVanPhong(id);

            if (BanGiaoCreateVM.BanGiao == null)
                return NotFound();

            return View(BanGiaoCreateVM);
        }

        // Post: Delete method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            BanGiao banGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(id);

            if (banGiao == null)
                return NotFound();
            else
            {
                _unitOfWork.banGiaoRepository.Delete(banGiao);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> ExportToWord(int id)
        {
            //id = 4;
            var chitiets = await _unitOfWork.chiTietBanGiaoRepository.FindByBanGiaoIdIncludeBanGiaoThietBi(id);
            var chitietPrints = chitiets.Select(x => new
            {
                x.Id,
                x.ThietBi.Name,
                x.DienGiai,
                x.SoLuong
            });
            DataTable dtChiTiets = EntityToTable.ToDataTable(chitietPrints);
            //FontFamily font = new FontFamily("Times New Roman");

            DocX doc = null;

            string webRootPath = _hostingEnvironment.WebRootPath;
            string fileName = webRootPath + @"\TemplateBG.docx";
            doc = DocX.Load(fileName);

            string ngay = chitiets.FirstOrDefault().BanGiao.NgayTao.ToString();
            doc.InsertAtBookmark(ngay, "Ngay");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.NguoiLap.ToString(), "NguoiLap");
            doc.InsertAtBookmark("P.CNTT", "PhongBanNguoiLap");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.NguoiNhan.ToString(), "NguoiNhan");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.PhongBan.ToString(), "PhongBanNguoiNhan");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.LoaiThietBi.ToString(), "ThietBi");

            var tbBG = doc.AddTable(1, 4);

            tbBG.Rows[0].Cells[0].Paragraphs[0].Append("STT").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[1].Paragraphs[0].Append("Thiết Bị").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[2].Paragraphs[0].Append("Diễn Giải").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[3].Paragraphs[0].Append("Số Lượng").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;

            if (tbBG.Rows.Count > 0)
            {
                for (int i = 0; i < dtChiTiets.Rows.Count; i++)
                {
                    var row = tbBG.InsertRow();
                    for (int j = 0; j < dtChiTiets.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            row.Cells[j].Paragraphs[0].Append((i + 1).ToString()).Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
                        }
                        else
                        {
                            row.Cells[j].Paragraphs[0].Append(dtChiTiets.Rows[i][j].ToString()).Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
                        }

                    }
                }
            }
            doc.InsertTable(tbBG);

            string lyDo = "Lý do: " + chitiets.FirstOrDefault().BanGiao.LyDo;
            doc.InsertParagraph(lyDo);

            string space = "";


            //Formatting Title  
            Novacode.Formatting titleFormat = new Novacode.Formatting();
            //Specify font family  
            //titleFormat.FontFamily = new Novacode.Font("Arial Black");
            //Specify font size  
            //titleFormat.Size = 18D;
            titleFormat.Position = 80;
            //titleFormat.FontColor = System.Drawing.Color.Orange;
            //titleFormat.UnderlineColor = System.Drawing.Color.Gray;
            //titleFormat.Italic = true;
            doc.InsertParagraph(space, false, titleFormat);

            var tbBG1 = doc.AddTable(2, 2);
            // tbBG1.Design = TableDesign.ColorfulGrid;

            // Set a blank border for the table's top/bottom borders.
            //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
            //tbBG1.SetBorder(TableBorderType.Bottom, blankBorder);
            //tbBG1.SetBorder(TableBorderType.Top, blankBorder);

            //tbBG1.SetWidths(new float[] { 300, 300 });
            //tbBG1.AutoFit = AutoFit.Fixed;

            var columnWidths = new float[] { 310f, 310f };
            // Set the table's column width and background 
            tbBG1.SetWidths(columnWidths);
            tbBG1.Design = TableDesign.None;


            tbBG1.Rows[0].Cells[0].Paragraphs[0].Append("Bên Nhận").Position(80).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[0].Cells[1].Paragraphs[0].Append("Bên Giao").Position(80).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[1].Cells[0].Paragraphs[0].Append(chitiets.FirstOrDefault().BanGiao.NguoiLap).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[1].Cells[1].Paragraphs[0].Append(chitiets.FirstOrDefault().BanGiao.NguoiNhan).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            doc.InsertTable(tbBG1);

            //Border border = new Border();
            //Color slateBlue = Color.AliceBlue;
            //border.Color = slateBlue;


            //tbBG1.SetBorder(TableBorderType.InsideH, border);

            //tbBG1.Rows[1].Cells.Last().SetBorder(TableCellBorderType.Left, new Border(BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));

            MemoryStream ms = new MemoryStream();

            MemoryStream stream = new MemoryStream();


            //Saves the Word document to  MemoryStream

            doc.SaveAs(stream);
            stream.Position = 0;
            //Download Word document in the browser
            return File(stream, "application/msword", "Result_" + DateTime.Now + ".docx");

            // return await Export(4);
        }

        [HttpPost]
        public async Task<IActionResult> ChiTietBanGiaoByBanGiaoId(int id)
        {
            var chiTiets = await _unitOfWork.chiTietBanGiaoRepository.FindByBanGiaoIdIncludeBanGiaoThietBi(id);
            if (chiTiets.Count() > 0)
            {
                return Json(new
                {
                    status = true
                });
            }

            return Json(new
            {
                status = false
            });
        }


        public async Task<IActionResult> ExportList(string stringId)
        {

            var idList = JsonConvert.DeserializeObject<List<BanGiaoCreateViewModel>>(stringId);
            //var idList = new List<BanGiaoCreateViewModel>();
            //idList.Add(new BanGiaoCreateViewModel()
            //{
            //    Id = 4
            //});
            var chitiets = new List<ChiTietBanGiao>();

            if (idList.Count == 1)
            {
                return RedirectToAction(nameof(Export), new { id = idList.FirstOrDefault().Id });
            }

            foreach (var item in idList)
            {
                var cts = await _unitOfWork.chiTietBanGiaoRepository.FindByBanGiaoIdIncludeBanGiaoThietBi(item.Id);
                if (cts.Count() > 0)
                {
                    chitiets.AddRange(cts);
                }

            }


            var chitietPrints = chitiets.Select(x => new
            {
                x.Id,
                x.ThietBi.Name,
                x.SoLuong,
                x.BanGiao.NguoiNhan

            });
            DataTable dtChiTiets = EntityToTable.ToDataTable(chitietPrints);
            //FontFamily font = new FontFamily("Times New Roman");

            DocX doc = null;

            string webRootPath = _hostingEnvironment.WebRootPath;
            string fileName = webRootPath + @"\TemplateBGList.docx";
            doc = DocX.Load(fileName);

            string ngay = chitiets.FirstOrDefault().BanGiao.NgayTao.ToString();
            doc.InsertAtBookmark(ngay, "Ngay");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.NguoiLap.ToString(), "BenGiao");
            doc.InsertAtBookmark("P.CNTT", "PhongBanBenGiao");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.VanPhong.ToString(), "PhongBanBenNhan");

            var tbBG = doc.AddTable(1, 5);


            tbBG.Rows[0].Cells[0].Paragraphs[0].Append("STT").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[1].Paragraphs[0].Append("Thiết Bị").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[2].Paragraphs[0].Append("Số Lượng").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[3].Paragraphs[0].Append("Người Nhận").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[4].Paragraphs[0].Append("Xác Nhận").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;

            //string secondCol = "", firstCol = "";

            List<string> name = new List<string>();

            if (tbBG.Rows.Count > 0)
            {
                for (int i = 0; i < dtChiTiets.Rows.Count; i++)
                {
                    var row = tbBG.InsertRow();
                    for (int j = 0; j < dtChiTiets.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            row.Cells[j].Paragraphs[0].Append((i + 1).ToString()).Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
                        }
                        else
                        {

                            row.Cells[j].Paragraphs[0].Append(dtChiTiets.Rows[i][j].ToString()).Font("Times New Roman").FontSize(11).Alignment = Alignment.center;


                            //if (i > 0)
                            //{
                            //    firstCol = dtChiTiets.Rows[i][3].ToString();
                            //    if ((i + 1) < dtChiTiets.Rows.Count)
                            //        secondCol = dtChiTiets.Rows[i + 1][3].ToString();

                            //    if (firstCol.Equals(secondCol))
                            //    {
                            //        tbBG.Rows[i].Cells[3].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_dotDash, BorderSize.one, 1, Color.Transparent));

                            //    }

                            //}


                        }

                    }
                }




            }

            var listCell = new List<string>();
            // int a = 0;

            List<int> rowFirsts = new List<int>();
            List<int> rowSeconds = new List<int>();

            foreach (var r in tbBG.Rows)
            {
                //a++;
                string val = r.Cells[3].Paragraphs[0].Text;
                listCell.Add(val);

            }

            for (int i = 0; i < listCell.Count; i++)
            {
                if ((i + 1) < listCell.Count)
                {
                    if (listCell[i].Equals(listCell[i + 1].ToString()))
                    {

                        rowFirsts.Add(i);
                        rowSeconds.Add(i + 1);

                        tbBG.Rows[i + 1].Cells[3].Paragraphs[0].Remove(false);
                        tbBG.Rows[i + 1].Cells[3].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_dotDash, BorderSize.one, 1, Color.White));

                        //tbBG.Rows[i + 1].Cells[4].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_dotDash, BorderSize.one, 1, Color.Transparent));
                    }
                }

            }



            //quaylai:
            //    for (int i = 0; i < rowSeconds.Count; i++)
            //    {

            //        for (int j = 0; j < rowFirsts.Count; j++)
            //        {
            //            if (rowSeconds[i] == rowFirsts[j])
            //            {
            //                rowSeconds.RemoveAt(i);
            //                rowFirsts.RemoveAt(j);

            //                goto quaylai;
            //            }

            //        }

            //    }

            //    for (int i = 0; i < rowSeconds.Count; i++)
            //    {
            //        tbBG.MergeCellsInColumn(3, rowFirsts[i], rowSeconds[i]);
            //        tbBG.MergeCellsInColumn(4, rowFirsts[i], rowSeconds[i]);
            //    }




            //tbBG.Rows[2].Cells[2].SetBorder(TableCellBorderType.Top, new Border(BorderStyle.Tcbs_dotDash, BorderSize.one, 1, Color.Transparent));



            // tbBG.RemoveColumn(3);

            var a = doc.InsertTable(tbBG);

            // Set a blank border for the table's top/bottom borders.
            //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
            //a.Rows[2].Cells[3].SetBorder(TableCellBorderType.Bottom, blankBorder);

            //string name1 = "", name2 = "";
            //for(int i = 0; i < tbBG.Rows.Count; i++)
            //{
            //    name1 = tbBG.Rows[i].Cells[i].ToString();
            //}

            string space = "";


            //Formatting Title  
            Novacode.Formatting titleFormat = new Novacode.Formatting();
            //Specify font family  
            //titleFormat.FontFamily = new Novacode.Font("Arial Black");
            //Specify font size  
            //titleFormat.Size = 18D;
            titleFormat.Position = 80;
            //titleFormat.FontColor = System.Drawing.Color.Orange;
            //titleFormat.UnderlineColor = System.Drawing.Color.Gray;
            //titleFormat.Italic = true;
            doc.InsertParagraph(space, false, titleFormat);

            var tbBG1 = doc.AddTable(2, 2);


            var columnWidths = new float[] { 310f, 310f };
            // Set the table's column width and background 
            tbBG1.SetWidths(columnWidths);
            tbBG1.Design = TableDesign.None;


            //tbBG1.Rows[0].Cells[0].Paragraphs[0].Append("Bên Nhận").Position(80).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[0].Cells[1].Paragraphs[0].Append("Bên Giao").Position(80).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            //tbBG1.Rows[1].Cells[0].Paragraphs[0].Append(chitiets.FirstOrDefault().BanGiao.NguoiLap).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[1].Cells[1].Paragraphs[0].Append(chitiets.FirstOrDefault().BanGiao.NguoiLap).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            doc.InsertTable(tbBG1);


            // doc.Save();
            MemoryStream ms = new MemoryStream();
            doc.SaveAs(ms);
            // document.Save(ms, SaveFormat.Docx);

            //byte[] byteArray = ms.ToArray();
            //ms.Flush();
            //ms.Close();
            //ms.Dispose();
            //Response.Clear();
            //Response.Headers.Add("Content-Disposition", "attachment; filename=chuongtrinh" + System.DateTime.Now.ToString("ddMMyyyyhhmmm") + ".docx");
            //Response.Headers.Add("Content-Length", byteArray.Length.ToString());
            //Response.ContentType = "application/ms-word";
            //Response.Body.Write(byteArray);
            //Response.StatusCode = StatusCodes.Status200OK;

            ms.Position = 0;



            // Download Word document in the browser
            return File(ms, "application/msword", "Result_" + DateTime.Now + ".docx");


            //return View();

        }

        public async Task<IActionResult> Export(int id)
        {
            var chitiets = await _unitOfWork.chiTietBanGiaoRepository.FindByBanGiaoIdIncludeBanGiaoThietBi(id);
            var chitietPrints = chitiets.Select(x => new
            {
                x.Id,
                x.ThietBi.Name,
                x.DienGiai,
                x.SoLuong
            });
            DataTable dtChiTiets = EntityToTable.ToDataTable(chitietPrints);
            //FontFamily font = new FontFamily("Times New Roman");

            DocX doc = null;

            string webRootPath = _hostingEnvironment.WebRootPath;
            string fileName = webRootPath + @"\TemplateBG.docx";
            doc = DocX.Load(fileName);

            string ngay = chitiets.FirstOrDefault().BanGiao.NgayTao.ToString();
            doc.InsertAtBookmark(ngay, "Ngay");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.NguoiLap.ToString(), "NguoiLap");
            doc.InsertAtBookmark("P.CNTT", "PhongBanNguoiLap");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.NguoiNhan.ToString(), "NguoiNhan");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.PhongBan.ToString(), "PhongBanNguoiNhan");
            doc.InsertAtBookmark(chitiets.FirstOrDefault().BanGiao.LoaiThietBi.ToString(), "ThietBi");

            var tbBG = doc.AddTable(1, 4);

            tbBG.Rows[0].Cells[0].Paragraphs[0].Append("STT").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[1].Paragraphs[0].Append("Thiết Bị").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[2].Paragraphs[0].Append("Diễn Giải").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG.Rows[0].Cells[3].Paragraphs[0].Append("Số Lượng").Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;

            if (tbBG.Rows.Count > 0)
            {
                for (int i = 0; i < dtChiTiets.Rows.Count; i++)
                {
                    var row = tbBG.InsertRow();
                    for (int j = 0; j < dtChiTiets.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            row.Cells[j].Paragraphs[0].Append((i + 1).ToString()).Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
                        }
                        else
                        {
                            row.Cells[j].Paragraphs[0].Append(dtChiTiets.Rows[i][j].ToString()).Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
                        }

                    }
                }
            }
            doc.InsertTable(tbBG);

            string lyDo = "Lý do: " + chitiets.FirstOrDefault().BanGiao.LyDo;
            doc.InsertParagraph(lyDo);

            string space = "";


            //Formatting Title  
            Novacode.Formatting titleFormat = new Novacode.Formatting();
            //Specify font family  
            //titleFormat.FontFamily = new Novacode.Font("Arial Black");
            //Specify font size  
            //titleFormat.Size = 18D;
            titleFormat.Position = 80;
            //titleFormat.FontColor = System.Drawing.Color.Orange;
            //titleFormat.UnderlineColor = System.Drawing.Color.Gray;
            //titleFormat.Italic = true;
            doc.InsertParagraph(space, false, titleFormat);

            var tbBG1 = doc.AddTable(2, 2);
            // tbBG1.Design = TableDesign.ColorfulGrid;

            // Set a blank border for the table's top/bottom borders.
            //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
            //tbBG1.SetBorder(TableBorderType.Bottom, blankBorder);
            //tbBG1.SetBorder(TableBorderType.Top, blankBorder);

            //tbBG1.SetWidths(new float[] { 300, 300 });
            //tbBG1.AutoFit = AutoFit.Fixed;

            var columnWidths = new float[] { 310f, 310f };
            // Set the table's column width and background 
            tbBG1.SetWidths(columnWidths);
            tbBG1.Design = TableDesign.None;


            tbBG1.Rows[0].Cells[0].Paragraphs[0].Append("Bên Nhận").Position(80).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[0].Cells[1].Paragraphs[0].Append("Bên Giao").Position(80).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[1].Cells[0].Paragraphs[0].Append(chitiets.FirstOrDefault().BanGiao.NguoiNhan).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[1].Cells[1].Paragraphs[0].Append(chitiets.FirstOrDefault().BanGiao.NguoiLap).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            doc.InsertTable(tbBG1);

            //Border border = new Border();
            //Color slateBlue = Color.AliceBlue;
            //border.Color = slateBlue;


            //tbBG1.SetBorder(TableBorderType.InsideH, border);

            //tbBG1.Rows[1].Cells.Last().SetBorder(TableCellBorderType.Left, new Border(BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));

            // MemoryStream ms = new MemoryStream();

            MemoryStream stream = new MemoryStream();


            // Saves the Word document to MemoryStream

            doc.SaveAs(stream);
            stream.Position = 0;
            // Download Word document in the browser
            return File(stream, "application/msword", "Result_" + DateTime.Now + ".docx");



        }
    }
}