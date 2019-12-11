using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;

namespace QLTB.Controllers
{
    [Authorize(Policy = "AdminRolePolicy")] // the same admin role --> setup in startup
    public class ChiNhanhsController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public ChiNhanhsController(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.chiNhanhRepository.GetAll());
        }

        //Get Create method
        public IActionResult Create()
        {
            ViewBag.Roles = roleManager.Roles.ToList();
            return View();
        }

        // Post Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("CreateRolePolicy")]
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
        [Authorize("EditRolePolicy")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
                return NotFound();

            var chinhanh = await _unitOfWork.chiNhanhRepository.GetByIdAsync(id);

            if (chinhanh == null)
                return NotFound();

            ViewBag.Roles = roleManager.Roles.ToList();
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

            ViewBag.Roles = roleManager.Roles.ToList();
            return View(chinhanh);
        }

        // Get Delete method
        [Authorize("DeleteRolePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var chinhanh = await _unitOfWork.chiNhanhRepository.GetByIdAsync(id);

            if (chinhanh == null)
                return NotFound();

            ViewBag.Roles = roleManager.Roles.ToList();
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