﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;
using QLTB.Utility;

namespace QLTB.Controllers
{
    [Authorize(Policy = "AdminRolePolicy")] // the same admin role --> setup in startup
    public class VanPhongsController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public VanPhongViewModel VanPhongVM { get; set; }
        public VanPhongsController(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            _unitOfWork = unitOfWork;
            VanPhongVM = new VanPhongViewModel()
            {
                ChiNhanhs = _unitOfWork.chiNhanhRepository.GetAll(),
                VanPhong = new Data.Models.VanPhong()
            };
        }
        public async Task<IActionResult> Index()
        {
            var vanPhongs = _unitOfWork.vanPhongRepository.VanPhongIncludeChiNhanh();
            return View(await vanPhongs);
        }

        // Get Create method
        [Authorize("CreateRolePolicy")]
        public IActionResult Create()
        {
            ViewBag.Roles = roleManager.Roles.ToList();
            return View(VanPhongVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(VanPhongVM);
            }

            _unitOfWork.vanPhongRepository.Create(VanPhongVM.VanPhong);            
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        // Get: Edit method
        [Authorize("EditRolePolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            VanPhongVM.VanPhong = await _unitOfWork.vanPhongRepository.FindIdIncludeChiNhanh(id);

            if (VanPhongVM.VanPhong == null)
                return NotFound();

            ViewBag.Roles = roleManager.Roles.ToList();
            return View(VanPhongVM);
        }

        // Post: Eidt method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != VanPhongVM.VanPhong.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _unitOfWork.vanPhongRepository.Update(VanPhongVM.VanPhong);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }

            return View(VanPhongVM);
        }

        // Get: Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            VanPhongVM.VanPhong = await _unitOfWork.vanPhongRepository.FindIdIncludeChiNhanh(id);

            if (VanPhongVM.VanPhong == null)
                return NotFound();

            return View(VanPhongVM);
        }

        // Get: Delete method
        [Authorize("DeleteRolePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            VanPhongVM.VanPhong = await _unitOfWork.vanPhongRepository.FindIdIncludeChiNhanh(id);

            if (VanPhongVM.VanPhong == null)
                return NotFound();

            return View(VanPhongVM);
        }

        // Post: Delete method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            VanPhong vanPhong = await _unitOfWork.vanPhongRepository.GetByIdAsync(id);

            if (vanPhong == null)
                return NotFound();
            else
            {
                _unitOfWork.vanPhongRepository.Delete(vanPhong);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}