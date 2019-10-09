﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Novacode;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;
using QLTB.Utility;

namespace QLTB.Controllers
{
    public class ChiTietBanGiaosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public ChiTietBanGiaoViewModel ChiTietBanGiaoVM { get; set; }
        public ChiTietBanGiaosController(IUnitOfWork unitOfWork, HostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            ChiTietBanGiaoVM = new ChiTietBanGiaoViewModel()
            {
                BanGiao = new Data.Models.BanGiao(),
                ThietBis = _unitOfWork.thietBiRepository.GetAll(),
                ChiTietBanGiao = new Data.Models.ChiTietBanGiao()
            };
        }
        public async Task<IActionResult> Index(int id, string strUrl)
        {
            ChiTietBanGiaoIndexViewModel chiTietBanGiaoIndexVM = new ChiTietBanGiaoIndexViewModel();
            chiTietBanGiaoIndexVM.ChiTietBanGiaos = await _unitOfWork.chiTietBanGiaoRepository.FindByBanGiaoIdIncludeBanGiaoThietBi(id);
            chiTietBanGiaoIndexVM.BanGiao = _unitOfWork.banGiaoRepository.GetById(id);
            chiTietBanGiaoIndexVM.StrUrl = strUrl;
            return View(chiTietBanGiaoIndexVM);
        }

        // Get Create method
        public async Task<IActionResult> Create(int banGiaoId, string strUrl)
        {
            ChiTietBanGiaoVM.strUrl = strUrl;
            ChiTietBanGiaoVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            return View(ChiTietBanGiaoVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST(int banGiaoId)
        {
            if (!ModelState.IsValid)
            {
                return View(ChiTietBanGiaoVM);
            }
            ChiTietBanGiaoVM.ChiTietBanGiao.BanGiaoId = banGiaoId;
            _unitOfWork.chiTietBanGiaoRepository.Create(ChiTietBanGiaoVM.ChiTietBanGiao);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index), new { id = banGiaoId, strUrl = ChiTietBanGiaoVM.strUrl });
        }
        
        // Get Edit method
        public async Task<IActionResult> Edit(int banGiaoId, int id, string strUrl)
        {
            ChiTietBanGiaoVM.strUrl = strUrl;
            ChiTietBanGiaoVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            ChiTietBanGiaoVM.ChiTietBanGiao = await _unitOfWork.chiTietBanGiaoRepository.GetByIdAsync(id);

            return View(ChiTietBanGiaoVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int banGiaoId)
        {
            if (!ModelState.IsValid)
            {
                return View(ChiTietBanGiaoVM);
            }

            ChiTietBanGiaoVM.ChiTietBanGiao.BanGiaoId = banGiaoId;
            _unitOfWork.chiTietBanGiaoRepository.Update(ChiTietBanGiaoVM.ChiTietBanGiao);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index), new { id = banGiaoId, strUrl = ChiTietBanGiaoVM.strUrl });
        }

        // Get Details method
        public async Task<IActionResult> Details(int banGiaoId, int id, string strUrl)
        {
            ChiTietBanGiaoVM.strUrl = strUrl;
            ChiTietBanGiaoVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            ChiTietBanGiaoVM.ChiTietBanGiao = await _unitOfWork.chiTietBanGiaoRepository.GetByIdAsync(id);

            return View(ChiTietBanGiaoVM);
        }
        
        // Get Delete method
        public async Task<IActionResult> Delete(int banGiaoId, int id, string strUrl)
        {
            ChiTietBanGiaoVM.strUrl = strUrl;
            ChiTietBanGiaoVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            ChiTietBanGiaoVM.ChiTietBanGiao = await _unitOfWork.chiTietBanGiaoRepository.GetByIdAsync(id);

            return View(ChiTietBanGiaoVM);
        }

        // Post: Delete Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id, int banGiaoId)
        {

            //ChiTietBanGiaoVM.ChiTietBanGiao.BanGiaoId = banGiaoId;
            _unitOfWork.chiTietBanGiaoRepository.Delete(_unitOfWork.chiTietBanGiaoRepository.GetById(id));
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index), new { id = banGiaoId, strUrl = ChiTietBanGiaoVM.strUrl });
        }

        public async Task<IActionResult> ExportList(string stringId)
        {

            var idList = JsonConvert.DeserializeObject<List<ChiTietBanGiaoViewModel>>(stringId);
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
                var cts = await _unitOfWork.chiTietBanGiaoRepository.FindIdIncludeBanGiaoThietBi(item.Id);
                if (cts != null)
                {
                    chitiets.Add(cts);
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

            string ngay = chitiets.FirstOrDefault().NgayGiao.ToString();
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

           // List<string> name = new List<string>();

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
            var chitiets = await _unitOfWork.chiTietBanGiaoRepository.FindIdIncludeBanGiaoThietBi(id);
            List<ChiTietBanGiao> listChiTiet = new List<ChiTietBanGiao>();
            listChiTiet.Add(chitiets);
            var chitietPrints = listChiTiet.Select(x => new
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

            string ngay = chitiets.NgayGiao.ToString();
            doc.InsertAtBookmark(ngay, "Ngay");
            doc.InsertAtBookmark(chitiets.BanGiao.NguoiLap.ToString(), "NguoiLap");
            doc.InsertAtBookmark("P.CNTT", "PhongBanNguoiLap");
            doc.InsertAtBookmark(chitiets.BanGiao.NguoiNhan.ToString(), "NguoiNhan");
            doc.InsertAtBookmark(chitiets.BanGiao.PhongBan.ToString(), "PhongBanNguoiNhan");
            doc.InsertAtBookmark(chitiets.BanGiao.LoaiThietBi.ToString(), "ThietBi");

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

            string lyDo = "Lý do: " + chitiets.BanGiao.LyDo;
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
            tbBG1.Rows[1].Cells[0].Paragraphs[0].Append(chitiets.BanGiao.NguoiLap).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
            tbBG1.Rows[1].Cells[1].Paragraphs[0].Append(chitiets.BanGiao.NguoiNhan).Bold().Font("Times New Roman").FontSize(11).Alignment = Alignment.center;
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