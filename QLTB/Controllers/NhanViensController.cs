using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public NhanVienViewModel NhanVienVM { get; set; }
        public NhanViensController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
            NhanVienVM = new NhanVienViewModel()
            {
                
                NhanVien = new Data.Models.NhanVien(),
                VanPhongs = _unitOfWork.vanPhongRepository.GetAll()
            };
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var roles = await userManager.GetRolesAsync(await userManager.GetUserAsync(User));
            var nhanviens = await _unitOfWork.nhanVienRepository.GetAllIncludeOneAsync(x => x.VanPhong);

            var listNvByRole = new List<NhanVien>();

            foreach (var role in roles)
            {
                nhanviens = nhanviens.Where(x => x.VanPhong.KhuVuc == role);
            }

            if (!User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
            {
                nhanviens = listNvByRole;
            }

            return View(nhanviens);
        }

        // Get Create method
        [Authorize("CreateCNRolePolicy")]
        public async Task<IActionResult> Create()
        {

            NhanVienVM.VanPhongs = await VanPhongByRole();

            //var roleList = new List<IdentityRole>();
            //var roleNames = await userManager.GetRolesAsync(await userManager.GetUserAsync(User));

            //foreach (var roleName in roleNames)
            //{
            //    var role = await roleManager.FindByNameAsync(roleName);
            //    roleList.Add(role);


            //}

            //ViewBag.Roles = roleList;
            //ViewBag.RoleNames = JsonConvert.SerializeObject(roleNames);

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

            //var vp = _unitOfWork.vanPhongRepository.Find(x => x.Name == NhanVienVM.NhanVien.VanPhong).FirstOrDefault();
            //NhanVienVM.NhanVien.ChiNhanhId = vp.ChiNhanhId;

            _unitOfWork.nhanVienRepository.Create(NhanVienVM.NhanVien);
            await _unitOfWork.Complete();
            SetAlert("Successfully.", "success");

            return RedirectToAction(nameof(Index));
        }

        // Get: Edit method
        [Authorize("EditCNRolePolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            NhanVienVM.NhanVien = await _unitOfWork.nhanVienRepository.FindByIdIncludeVanPhong(id);

            if (NhanVienVM.NhanVien == null)
                return NotFound();

            if (!User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
            {
                NhanVienVM.VanPhongs = await VanPhongByRole();
            }

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
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            NhanVienVM.NhanVien = await _unitOfWork.nhanVienRepository.FindByIdIncludeVanPhong(id);

            if (NhanVienVM.NhanVien == null)
                return NotFound();

            return View(NhanVienVM);
        }

        // Get: Delete method
        [Authorize("DeleteCNRolePolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            NhanVienVM.NhanVien = await _unitOfWork.nhanVienRepository.FindByIdIncludeVanPhong(id);

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

        public async Task<IEnumerable<VanPhong>> VanPhongByRole()
        {
            var roles = await userManager.GetRolesAsync(await userManager.GetUserAsync(User));

            //var listCn = _unitOfWork.chiNhanhRepository.GetAll();

            //var listChiNhanh = new List<ChiNhanh>();

            //foreach (var chinhanh in NhanVienVM.ChiNhanhs)
            //{
            //    foreach (var role in roles)
            //    {
            //        if (chinhanh.KhuVuc == role)
            //        {
            //            listChiNhanh.Add(chinhanh);
            //        }
            //    }
            //}

            var vanPhongs = _unitOfWork.vanPhongRepository.GetAll();
            var listVanPhongs = new List<VanPhong>();
            foreach (var role in roles)
            {
                var listVP = _unitOfWork.vanPhongRepository.Find(x => x.KhuVuc == role);
                listVanPhongs.AddRange(listVanPhongs);
            }


            if (!User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
            {
                vanPhongs = listVanPhongs;
            }

            return vanPhongs;

        }

        private void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }

        /// <summary>
        /// mybe delete
        /// </summary>
        /// <param name="chinhanh"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetDmVanPhongByChiNhanh(int chinhanh)
        {
            var vanphongs = _unitOfWork.vanPhongRepository.GetAll();

            if (chinhanh != 0)
            {
                vanphongs = _unitOfWork.vanPhongRepository.Find(x => x.ChiNhanhId == chinhanh);
            }

            var vps = JsonConvert.SerializeObject(vanphongs);
            return Json(new
            {
                data = vps
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetDmVanPhongByRole(string role)
        {
            
            var vanphongs = await _unitOfWork.vanPhongRepository.FindKhuVucIncludeChiNhanh(role);
            var vpcnList = new List<VPCN>();
            foreach(var vp in vanphongs)
            {
                var vpcn = new VPCN()
                {
                    Name = vp.Name,
                    NameCN = vp.ChiNhanh.Name
                };

                vpcnList.Add(vpcn);
            }
            var vps = JsonConvert.SerializeObject(vpcnList);
            return Json(new
            {
                data = vps
            });
        }

       
    }
}