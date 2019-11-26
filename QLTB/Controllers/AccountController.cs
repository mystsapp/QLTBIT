using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLTB.Data.Models;
using QLTB.Data.Repository;
using QLTB.Extensions;
using QLTB.Models;

namespace QLTB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;

           
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            //ViewBag.ChiNhanhs = unitOfWork.chiNhanhRepository.GetAll();
            //ViewBag.VanPhongs = unitOfWork.vanPhongRepository.GetAll();

            RegisterViewModel registerVM = new RegisterViewModel()
            {
                ChiNhanhs = unitOfWork.chiNhanhRepository.GetAll()
            };

            ViewBag.chinhanh = registerVM.ChiNhanhs.ToList();
            return View(registerVM);
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerVM)
        {
            //ViewBag.ChiNhanhs = unitOfWork.chiNhanhRepository.GetAll();
            //ViewBag.VanPhongs = unitOfWork.vanPhongRepository.GetAll();
            registerVM.ChiNhanhs = unitOfWork.chiNhanhRepository.GetAll();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerVM.Username,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber,
                    Name = registerVM.Name,
                    PhongBan = registerVM.PhongBan,
                    ChiNhanhId = registerVM.ChiNhanhId,
                    VanPhong = registerVM.VanPhong,
                    Khoi = registerVM.Khoi,
                    TinhTrang = registerVM.TinhTrang
                };

                var result = await userManager.CreateAsync(user, registerVM.Password);

                if (result.Succeeded)
                {
                    if(signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                    await signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false ==> use session not cookie
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerVM);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList() // get clientID, clientSecret in StartUp
            };

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    //await signInManager.SignInAsync(user, isPersistent: false); // isPersistent: false ==> use session not cookie
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid Login Attempt");

            }
            return View(model);
        }

        [AcceptVerbs("Get", "HttpPost")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUsernameInUse(string Username)
        {
            var user = await userManager.FindByNameAsync(Username);

            if (user == null)
                return Json(true);
            return Json($"Username {Username} is already in use");
        }

       
    }
}