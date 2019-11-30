using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;
using QLTB.Utility;

namespace QLTB.Controllers
{
    public class PhanMemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public PhanMemViewModel PhanMemVM { get; set; }
        public PhanMemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            PhanMemVM = new PhanMemViewModel()
            {
                LoaiPhanMems = _unitOfWork.loaiPhanMemRepository.GetAll(),
                PhanMem = new Data.Models.PhanMem()
            };
        }
        public async Task<IActionResult> Index()
        {
            var phanMems = _unitOfWork.phanMemRepository.PhanMemIncludeLoaiPhanMem();
            return View(await phanMems);
        }

        // Get Create method
        [Authorize("CreateRolePolicy")]
        public IActionResult Create()
        {
            return View(PhanMemVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(PhanMemVM);
            }

            _unitOfWork.phanMemRepository.Create(PhanMemVM.PhanMem);            
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        // Get: Edit method
        [Authorize("EditRolePolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            PhanMemVM.PhanMem = await _unitOfWork.phanMemRepository.FindIdIncludeLoaiPhanMem(id);

            if (PhanMemVM.PhanMem == null)
                return NotFound();

            return View(PhanMemVM);
        }

        // Post: Eidt method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != PhanMemVM.PhanMem.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _unitOfWork.phanMemRepository.Update(PhanMemVM.PhanMem);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }

            return View(PhanMemVM);
        }

        // Get: Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            PhanMemVM.PhanMem = await _unitOfWork.phanMemRepository.FindIdIncludeLoaiPhanMem(id);

            if (PhanMemVM.PhanMem == null)
                return NotFound();

            return View(PhanMemVM);
        }

        // Get: Delete method
        [Authorize("DeleteRolePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            PhanMemVM.PhanMem = await _unitOfWork.phanMemRepository.FindIdIncludeLoaiPhanMem(id);

            if (PhanMemVM.PhanMem == null)
                return NotFound();

            return View(PhanMemVM);
        }

        // Post: Delete method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            PhanMem phanMem = await _unitOfWork.phanMemRepository.GetByIdAsync(id);

            if (phanMem == null)
                return NotFound();
            else
            {
                _unitOfWork.phanMemRepository.Delete(phanMem);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}