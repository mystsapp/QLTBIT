using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
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

        [HttpGet]
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

            model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList(); // get clientID, clientSecret in StartUp



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

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                new { ReturnUrl = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel loginViewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError("", $"Error from external provider: {remoteError}");
                return View("Login", loginViewModel);
            }

            var info = await signInManager.GetExternalLoginInfoAsync(); // get the external login infomation
            if (info == null)
            {
                ModelState.AddModelError("", "Error loading external login information.");
                return View("Login", loginViewModel);
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                                     info.ProviderKey, isPersistent: false, bypassTwoFactor: true); // check external login (check in UserLogin tbl)

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var user = await userManager.FindByEmailAsync(email);// check in the local account

                    if (user == null)
                    {
                        user = new ApplicationUser()
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name = "ExternalLogin",
                            ChiNhanhId = 9
                        };

                        await userManager.CreateAsync(user);
                    }

                    await userManager.AddLoginAsync(user, info); // add to userlogin table
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ViewBag.Titile = $"Email claim not receive from: {info.LoginProvider}";
                    ViewBag.ErrorMessage = "Please contact support on Admin@saigontourist.net";

                    return View("Error");
                }
            }
            return View("Login", loginViewModel);
        }
    }
}