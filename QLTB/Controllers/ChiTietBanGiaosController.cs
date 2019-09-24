using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Repository;
using QLTB.Models;

namespace QLTB.Controllers
{
    public class ChiTietBanGiaosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public ChiTietBanGiaoViewModel ChiTietBanGiaoVM { get; set; }
        public ChiTietBanGiaosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

    }
}