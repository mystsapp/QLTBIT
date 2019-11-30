using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [BindProperty]
        public NhapKhoListViewModel NhapKhoListVM { get; set; }
        public NhapKhosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            NhapKhoListVM = new NhapKhoListViewModel()
            {
                NhapKhos = new List<NhapKho>(),
                NhapKho = new NhapKho()
            };
        }
        public async Task<IActionResult> Index(string searchFromDate = null, string searchToDate = null)
        {
            NhapKhoListVM.NhapKhos = await _unitOfWork.nhapKhoRepository.GetAllIncludeOneAsync(x => x.ChiTietBanGiao);

            if (searchFromDate != null && searchToDate != null)
            {
                DateTime fromDate = DateTime.Parse(searchFromDate);
                DateTime toDate = DateTime.Parse(searchToDate);
                NhapKhoListVM.NhapKhos = NhapKhoListVM.NhapKhos.Where(x => x.NgayNhapKho >= fromDate && x.NgayNhapKho <= toDate.AddDays(1));
            }

            return View(NhapKhoListVM);
        }

        public async Task<IActionResult> LiquidateList(string stringId)
        {

            var idList = JsonConvert.DeserializeObject<List<NhapKhoListViewModel>>(stringId);
            foreach(var item in idList)
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

        [Authorize("DeleteRolePolicy")]
        public async Task<IActionResult> DeleteList(string ids)
        {

            var idList = JsonConvert.DeserializeObject<List<NhapKhoListViewModel>>(ids);
            foreach (var item in idList)
            {
                var nhapKho = await _unitOfWork.nhapKhoRepository.GetByIdAsync(item.Id);

                _unitOfWork.chiTietBanGiaoRepository.Delete(_unitOfWork.chiTietBanGiaoRepository.GetById(nhapKho.CTBGId));
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