using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;

namespace QLTB.Controllers
{
    public class ChiNhanhsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChiNhanhsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.chiNhanhRepository.GetAll());
        }

        //Get Create method
        public IActionResult Create()
        {
            return View();
        }

        // Post Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiNhanh chiNhanh)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.chiNhanhRepository.Create(chiNhanh);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(chiNhanh);
        }

        // Get Edit method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var chinhanh = await _unitOfWork.chiNhanhRepository.GetByIdAsync(id);

            if (chinhanh == null)
                return NotFound();

            return View(chinhanh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChiNhanh chinhanh)
        {
            if (id != chinhanh.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _unitOfWork.chiNhanhRepository.Update(chinhanh);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }

            return View(chinhanh);
        }

        // Get Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var chinhanh = await _unitOfWork.chiNhanhRepository.GetByIdAsync(id);

            if (chinhanh == null)
                return NotFound();

            return View(chinhanh);
        }
        
        // Get Delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var chinhanh = await _unitOfWork.chiNhanhRepository.GetByIdAsync(id);

            if (chinhanh == null)
                return NotFound();

            return View(chinhanh);
        }

        // Post Delete 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var chinhanh = await _unitOfWork.chiNhanhRepository.GetByIdAsync(id);
            _unitOfWork.chiNhanhRepository.Delete(chinhanh);
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}