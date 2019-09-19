using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;

namespace QLTB.Controllers
{
    public class LoaiPhanMemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoaiPhanMemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.loaiPhanMemRepository.GetAll());
        }

        //Get Create method
        public IActionResult Create()
        {
            return View();
        }

        // Post Create method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiPhanMem loaiPhanMem)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.loaiPhanMemRepository.Create(loaiPhanMem);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(loaiPhanMem);
        }

        // Get Edit method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var loaiPM = await _unitOfWork.loaiPhanMemRepository.GetByIdAsync(id);

            if (loaiPM == null)
                return NotFound();

            return View(loaiPM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoaiPhanMem loaiPhanMem)
        {
            if (id != loaiPhanMem.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _unitOfWork.loaiPhanMemRepository.Update(loaiPhanMem);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(loaiPhanMem);
        }

        // Get Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var loaiPm = await _unitOfWork.loaiPhanMemRepository.GetByIdAsync(id);

            if (loaiPm == null)
                return NotFound();

            return View(loaiPm);
        }

        // Get Delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var loaiPm = await _unitOfWork.loaiPhanMemRepository.GetByIdAsync(id);

            if (loaiPm == null)
                return NotFound();

            return View(loaiPm);
        }

        // Post Delete 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var loaiPm = await _unitOfWork.loaiPhanMemRepository.GetByIdAsync(id);
            _unitOfWork.loaiPhanMemRepository.Delete(loaiPm);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}