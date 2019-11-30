using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Repository;
using QLTB.Models;

namespace QLTB.Controllers
{
    public class CaiDatsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public CaiDatViewModel CaiDatVM { get; set; }
        public CaiDatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CaiDatVM = new CaiDatViewModel()
            {
                BanGiao = new Data.Models.BanGiao(),
                PhanMems = _unitOfWork.phanMemRepository.GetAll(),
                CaiDat = new Data.Models.CaiDat()
            };
        }
        public async Task<IActionResult> Index(int id, string strUrl)
        {
            CaiDatIndexViewModel caiDatIndexVM = new CaiDatIndexViewModel();
            caiDatIndexVM.CaiDats = await _unitOfWork.caiDatRepository.FindByBanGiaoIdIncludeBanGiaoPhanMem(id);
            caiDatIndexVM.BanGiao = _unitOfWork.banGiaoRepository.GetById(id);
            caiDatIndexVM.StrUrl = strUrl;
            return View(caiDatIndexVM);
        }

        [Authorize("CreateRolePolicy")]
        // Get Create method
        public async Task<IActionResult> Create(int banGiaoId, string strUrl)
        {
            CaiDatVM.strUrl = strUrl;
            CaiDatVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            return View(CaiDatVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST(int banGiaoId)
        {
            if (!ModelState.IsValid)
            {
                return View(CaiDatVM);
            }
            CaiDatVM.CaiDat.BanGiaoId = banGiaoId;
            _unitOfWork.caiDatRepository.Create(CaiDatVM.CaiDat);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index), new { id = banGiaoId, strUrl = CaiDatVM.strUrl });
        }

        // Get Edit method
        [Authorize("EditRolePolicy")]
        public async Task<IActionResult> Edit(int banGiaoId, int id, string strUrl)
        {
            CaiDatVM.strUrl = strUrl;
            CaiDatVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            CaiDatVM.CaiDat = await _unitOfWork.caiDatRepository.GetByIdAsync(id);

            return View(CaiDatVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int banGiaoId)
        {
            if (!ModelState.IsValid)
            {
                return View(CaiDatVM);
            }

            CaiDatVM.CaiDat.BanGiaoId = banGiaoId;
            _unitOfWork.caiDatRepository.Update(CaiDatVM.CaiDat);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index), new { id = banGiaoId, strUrl = CaiDatVM.strUrl });
        }

        // Get Details method
        public async Task<IActionResult> Details(int banGiaoId, int id, string strUrl)
        {
            CaiDatVM.strUrl = strUrl;
            CaiDatVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            CaiDatVM.CaiDat = await _unitOfWork.caiDatRepository.GetByIdAsync(id);

            return View(CaiDatVM);
        }

        // Get Delete method
        [Authorize("DeleteRolePolicy")]
        public async Task<IActionResult> Delete(int banGiaoId, int id, string strUrl)
        {
            CaiDatVM.strUrl = strUrl;
            CaiDatVM.BanGiao = await _unitOfWork.banGiaoRepository.GetByIdAsync(banGiaoId);
            CaiDatVM.CaiDat = await _unitOfWork.caiDatRepository.GetByIdAsync(id);

            return View(CaiDatVM);
        }

        // Post: Delete Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id, int banGiaoId)
        {

            //ChiTietBanGiaoVM.ChiTietBanGiao.BanGiaoId = banGiaoId;
            _unitOfWork.caiDatRepository.Delete(_unitOfWork.caiDatRepository.GetById(id));
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index), new { id = banGiaoId, strUrl = CaiDatVM.strUrl });
        }

    }
}