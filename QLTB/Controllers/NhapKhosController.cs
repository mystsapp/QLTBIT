using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Models;

namespace QLTB.Controllers
{
    public class NhapKhosController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        [BindProperty]
        public NhapKhoListViewModel NhapKhoListVM { get; set; }
        public NhapKhosController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            this.userManager = userManager;
            NhapKhoListVM = new NhapKhoListViewModel()
            {
                NhapKhos = new List<NhapKho>(),
                NhapKho = new NhapKho()
            };
        }
        public async Task<IActionResult> Index(string searchFromDate = null, string searchToDate = null)
        {
            NhapKhoListVM.NhapKhos = _unitOfWork.nhapKhoRepository.GetAll();

            if (searchFromDate != null && searchToDate != null)
            {
                DateTime fromDate = DateTime.Parse(searchFromDate);
                DateTime toDate = DateTime.Parse(searchToDate);
                NhapKhoListVM.NhapKhos = NhapKhoListVM.NhapKhos.Where(x => x.NgayNhapKho >= fromDate && x.NgayNhapKho <= toDate.AddDays(1));
            }

            var roles = await userManager.GetRolesAsync(await userManager.GetUserAsync(User));

            List<NhapKho> nhapKhos = new List<NhapKho>();

            if (!User.IsInRole("Admin") && !User.IsInRole("Super Admin"))
            {
                foreach (var role in roles)
                {
                    nhapKhos.AddRange(NhapKhoListVM.NhapKhos.Where(x => x.KhuVuc.Equals(role)));
                }

                NhapKhoListVM.NhapKhos = nhapKhos;
            }
            return View(NhapKhoListVM);
        }

        public async Task<IActionResult> LiquidateList(string stringId)
        {

            var idList = JsonConvert.DeserializeObject<List<NhapKhoListViewModel>>(stringId);
            foreach (var item in idList)
            {
                var nhapKho = await _unitOfWork.nhapKhoRepository.GetByIdAsync(item.Id);
                nhapKho.ThanhLy = true;
                nhapKho.NgayThanhLy = DateTime.Now;
                nhapKho.NguoiThanhLy = "Admin";
                await _unitOfWork.Complete();
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Details(int id)
        {
            NhapKhoListVM.NhapKho = await _unitOfWork.nhapKhoRepository.GetByIdAsync(id);

            return PartialView(NhapKhoListVM);
        }

        [Authorize("DeleteCNRolePolicy")]
        public async Task<IActionResult> DeleteList(string ids)
        {

            var idList = JsonConvert.DeserializeObject<List<NhapKhoListViewModel>>(ids);
            foreach (var item in idList)
            {
                var nhapKho = await _unitOfWork.nhapKhoRepository.GetByIdAsync(item.Id);

                var chitiet = _unitOfWork.chiTietBanGiaoRepository.GetById(nhapKho.CTBGId);
                if (chitiet != null)
                {
                    _unitOfWork.chiTietBanGiaoRepository.Delete(chitiet);
                }

                _unitOfWork.nhapKhoRepository.Delete(nhapKho);
                await _unitOfWork.Complete();
            }
            return Json(new
            {
                status = true
            });

        }
    }
}