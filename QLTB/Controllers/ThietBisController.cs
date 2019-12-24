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
    public class ThietBisController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public ThietBiViewModel ThietBiVM { get; set; }

        public ThietBisController(IUnitOfWork unitOfWork, HostingEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            ThietBiVM = new ThietBiViewModel()
            {
                LoaiThietBis = _unitOfWork.loaiThietBiRepository.GetAll(),
                ThietBi = new Data.Models.ThietBi()
            };
        }
        public async Task<IActionResult> Index()
        {
            var thietBi = _unitOfWork.thietBiRepository.ThietBiWithLoaiThietBi();
            return View(await thietBi);
        }

        // Get Create method
        [Authorize("CreateRolePolicy")]
        public IActionResult Create()
        {
            return View(ThietBiVM);
        }

        // Post: Create Method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(ThietBiVM);
            }

            _unitOfWork.thietBiRepository.Create(ThietBiVM.ThietBi);
            await _unitOfWork.thietBiRepository.Save();

            // Image being saved
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var thietBiFromDb = _unitOfWork.thietBiRepository.GetById(ThietBiVM.ThietBi.Id);

            if (files.Count != 0)
            {
                // Image has been uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, ThietBiVM.ThietBi.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                thietBiFromDb.HinhAnh = @"\" + SD.ImageFolder + @"\" + ThietBiVM.ThietBi.Id + extension;
            }
            else
            {
                // when user does not up load image
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultThietBiImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + ThietBiVM.ThietBi.Id + ".png");

                thietBiFromDb.HinhAnh = @"\" + SD.ImageFolder + @"\" + ThietBiVM.ThietBi.Id + ".png";
            }

            await _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        // Get: Edit method
        [Authorize("EditCNRolePolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            ThietBiVM.ThietBi = await _unitOfWork.thietBiRepository.FindIdWithLoaiThietBi(id);

            if (ThietBiVM.ThietBi == null)
                return NotFound();

            return View(ThietBiVM);
        }

        // Post: Eidt method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                //var thietBiFromDb = _unitOfWork.thietBiRepository.GetById(ThietBiVM.ThietBi.Id);
                var thietBiFromDb = _unitOfWork.thietBiRepository.GetSingleNoTracking(x => x.Id == ThietBiVM.ThietBi.Id); // sua loi tracking
                var extension_old = Path.GetExtension(thietBiFromDb.HinhAnh);

                if (files.Count > 0 && files[0] != null)
                {
                    // if user upload a new image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);


                    if (System.IO.File.Exists(Path.Combine(uploads, ThietBiVM.ThietBi.Id + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, ThietBiVM.ThietBi.Id + extension_old));
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, ThietBiVM.ThietBi.Id + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    ThietBiVM.ThietBi.HinhAnh = @"\" + SD.ImageFolder + @"\" + ThietBiVM.ThietBi.Id + extension_new;
                }
                else
                    ThietBiVM.ThietBi.HinhAnh = @"\" + SD.ImageFolder + @"\" + ThietBiVM.ThietBi.Id + extension_old;

                _unitOfWork.thietBiRepository.Update(ThietBiVM.ThietBi);

                //if (ThietBiVM.ThietBi.HinhAnh != null)
                //    thietBiFromDb.HinhAnh = ThietBiVM.ThietBi.HinhAnh;

                //thietBiFromDb.TenThietBi = ThietBiVM.ThietBi.TenThietBi;
                //thietBiFromDb.Gia = ThietBiVM.ThietBi.Gia;
                //thietBiFromDb.LoaiThietBiId = ThietBiVM.ThietBi.LoaiThietBiId;
                //thietBiFromDb.DienGiai = ThietBiVM.ThietBi.DienGiai;
                //thietBiFromDb.BaoHanh = ThietBiVM.ThietBi.BaoHanh;
                //thietBiFromDb.TrangThai = ThietBiVM.ThietBi.TrangThai;

                await _unitOfWork.thietBiRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(ThietBiVM);
        }

        // Get: Details method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            ThietBiVM.ThietBi = await _unitOfWork.thietBiRepository.FindIdWithLoaiThietBi(id);

            if (ThietBiVM.ThietBi == null)
                return NotFound();

            return View(ThietBiVM);
        }

        // Get: Delete method
        [Authorize("DeleteRolePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            ThietBiVM.ThietBi = await _unitOfWork.thietBiRepository.FindIdWithLoaiThietBi(id);

            if (ThietBiVM.ThietBi == null)
                return NotFound();

            return View(ThietBiVM);
        }

        // Post: Delete method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            ThietBi thietBi = await _unitOfWork.thietBiRepository.GetByIdAsync(id);

            if (thietBi == null)
                return NotFound();
            else
            {
                string upload = Path.Combine(webRootPath, SD.ImageFolder);
                var extenstion = Path.GetExtension(thietBi.HinhAnh);

                if(System.IO.File.Exists(Path.Combine(upload, thietBi.Id + extenstion)))
                {
                    System.IO.File.Delete(Path.Combine(upload, thietBi.Id + extenstion));
                }

                _unitOfWork.thietBiRepository.Delete(thietBi);
                await _unitOfWork.Complete();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}