using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;
using QLTB.Utility;

namespace QLTB.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public NhanVienViewModel NhanVienVM { get; set; }
        public NhanViensController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            NhanVienVM = new NhanVienViewModel()
            {
                ChiNhanhs = _unitOfWork.chiNhanhRepository.GetAll(),
                NhanVien = new Data.Models.NhanVien(),
                VanPhongs = _unitOfWork.vanPhongRepository.GetAll()
            };
        }
        public async Task<IActionResult> Index()
        {
            var nhanviens = _unitOfWork.nhanVienRepository.NhanVienIncludeChiNhanh();
            return View(await nhanviens);
        }

        // Get Create method
        public IActionResult Create()
        {
            return View(NhanVienVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(NhanVienVM);
            }

            _unitOfWork.nhanVienRepository.Create(NhanVienVM.NhanVien);            
            await _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
        }

        // Get: Edit method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            NhanVienVM.NhanVien = await _unitOfWork.nhanVienRepository.FindIdIncludeChiNhanh(id);

            if (NhanVienVM.NhanVien == null)
                return NotFound();

            return View(NhanVienVM);
        }

        // Post: Eidt method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != NhanVienVM.NhanVien.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _unitOfWork.nhanVienRepository.Update(NhanVienVM.NhanVien);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }

            return View(NhanVienVM);
        }

        // Get: Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            NhanVienVM.NhanVien = await _unitOfWork.nhanVienRepository.FindIdIncludeChiNhanh(id);

            if (NhanVienVM.NhanVien == null)
                return NotFound();

            return View(NhanVienVM);
        }
        
        // Get: Delete method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            NhanVienVM.NhanVien = await _unitOfWork.nhanVienRepository.FindIdIncludeChiNhanh(id);

            if (NhanVienVM.NhanVien == null)
                return NotFound();

            return View(NhanVienVM);
        }

        // Post: Delete method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            NhanVien nhanVien = await _unitOfWork.nhanVienRepository.GetByIdAsync(id);

            if (nhanVien == null)
                return NotFound();
            else
            {
                _unitOfWork.nhanVienRepository.Delete(nhanVien);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public JsonResult GetDmVanPhongByChiNhanh(int chinhanh)
        {
            var vanphongs = _unitOfWork.vanPhongRepository.Find(x => x.ChiNhanhId == chinhanh);
            return Json(new
            {
                data = JsonConvert.SerializeObject(vanphongs)
            });
        }
    }
}