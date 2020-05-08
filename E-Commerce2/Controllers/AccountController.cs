using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DataAccessLayer.Enum;
using DataAccessLayer.ViewModel;
using DataBaseLayer.Models;
using E_Commerce2.Servcies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Microsoft.Extensions.Logging;

namespace E_Commerce2.Controllers

{

    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<AccountController> logger;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
                        RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.logger = logger;
            this.configuration = configuration;
        }
        public IActionResult Register()
        {


            return View(new UserVM());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserVM model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Country = model.Country.GetCountryName(),
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);

                    //if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    //{
                    //    return RedirectToAction("ListUsers", "Administration");
                    //}

                    var newemail = new DevEmailSender(configuration).SendEmailAsync(user.Email, "Email Confirmation", $"<h2>Thank you for joining us </h2><h2>to complete your registration please cclick the link  below</h2><a href='{confirmationLink}' class='btn btn-success'>Confirm your account</a>`");

                    ViewBag.ErrorTitle = "Registration successful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                            "email, by clicking on the confirmation link we have emailed you";
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        // rest of the code  
        public IActionResult Login()
        {



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {


            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(model);
                }

                if (user.EmailConfirmed == false)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                      new { userId = user.Id, token = token }, Request.Scheme);

                    logger.Log(LogLevel.Warning, confirmationLink);



                    var newemail = new DevEmailSender(configuration).SendEmailAsync(user.Email, "Email Confirmation", $"<h2>Thank you for joining us </h2><h2>to complete your registration please cclick the link  below</h2><a href='{confirmationLink}' class='btn btn-success'>Confirm your account</a>`");
                    ModelState.AddModelError(string.Empty, "Check your Email and verify your account");
                    return View(model);
                }

                //if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                //{
                //    return RedirectToAction("ListUsers", "Administration");
                //}

                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    //if(!await roleManager.RoleExistsAsync("Admin"))
                    // {
                    //     var role = new Microsoft.AspNetCore.Identity.IdentityRole();
                    //     role.Name = "Admin";
                    //   await   roleManager.CreateAsync(role);
                    // }
                    // await  userManager.AddToRoleAsync(user,"Admin");
                    if (user.IsAdmin == true)
                        return RedirectToAction("IndexMain", "home", new { area = "admin" });
                    else
                        return RedirectToAction("index", "home");

                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }
        // [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

    }

}