using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLTB.Controllers
{
    public class BanGiaosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public BanGiaoCreateViewModel BanGiaoCreateVM { get; set; }

        private int PageSize = 3;

        public BanGiaosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            BanGiaoCreateVM = new BanGiaoCreateViewModel()
            {
                ChiNhanhs = _unitOfWork.chiNhanhRepository.GetAll(),
                VanPhongs = _unitOfWork.vanPhongRepository.GetAll(),
                LoaiThietBis = _unitOfWork.loaiThietBiRepository.GetAll(),
                NhanViens = _unitOfWork.nhanVienRepository.GetAll(),
                BanGiao = new Data.Models.BanGiao()
            };
        }

        public async Task<IActionResult> Index(int banGiaoPage = 1, string searchNguoiNhan = null, string searchNguoiLap = null, string searchVanPhong = null, string searchDate = null)
        {
            BanGiaoViewModel banGiaoVM = new BanGiaoViewModel()
            {
                BanGiaos = new List<BanGiao>()
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

            banGiaoVM.BanGiaos = await _unitOfWork.banGiaoRepository.BanGiaoIncludeChiNhanh();

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
                banGiaoVM.BanGiaos = banGiaoVM.BanGiaos.Where(x => x.VanPhong.ToLower().Contains(searchVanPhong.ToLower())).ToList();
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
            var count = banGiaoVM.BanGiaos.Count;

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

        public IActionResult Create()
        {
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            BanGiaoCreateVM.BanGiao = await _unitOfWork.banGiaoRepository.FindIdIncludeChiNhanh(id);

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

            BanGiaoCreateVM.BanGiao = await _unitOfWork.banGiaoRepository.FindIdIncludeChiNhanh(id);

            if (BanGiaoCreateVM.BanGiao == null)
                return NotFound();

            return View(BanGiaoCreateVM);
        }

        // Get: Delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            BanGiaoCreateVM.BanGiao = await _unitOfWork.banGiaoRepository.FindIdIncludeChiNhanh(id);

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
    }
}