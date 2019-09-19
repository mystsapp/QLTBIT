using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;

namespace QLTB.Controllers
{
    public class LoaiThietBisController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoaiThietBisController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.loaiThietBiRepository.GetAll());
        }

        //Get Create method
        public IActionResult Create()
        {
            return View();
        }

        // Post Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiThietBi loaiThietBi)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.loaiThietBiRepository.Create(loaiThietBi);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(loaiThietBi);
        }

        // Get Edit method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var loaiTb = await _unitOfWork.loaiThietBiRepository.GetByIdAsync(id);

            if (loaiTb == null)
                return NotFound();

            return View(loaiTb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoaiThietBi loaiThietBi)
        {
            if (id != loaiThietBi.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _unitOfWork.loaiThietBiRepository.Update(loaiThietBi);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(loaiThietBi);
        }

        // Get Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var loaiTb = await _unitOfWork.loaiThietBiRepository.GetByIdAsync(id);

            if (loaiTb == null)
                return NotFound();

            return View(loaiTb);
        }
        
        // Get Delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var loaiTb = await _unitOfWork.loaiThietBiRepository.GetByIdAsync(id);

            if (loaiTb == null)
                return NotFound();

            return View(loaiTb);
        }

        // Post Delete 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var loaiThietBi = await _unitOfWork.loaiThietBiRepository.GetByIdAsync(id);
            _unitOfWork.loaiThietBiRepository.Delete(loaiThietBi);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}