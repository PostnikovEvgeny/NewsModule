using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using NewsModule.Models;
using NewsModule.Services;
using NewsModule.Data;
using NewsModule.Services.Jwt;
using System.Security.Claims;

namespace NewsModule.Controllers
{
    public class RegistrationController : Controller
    {
        NewsModuleContext context;
        RegisterService service;
        JwtProvider jwtProvider = new JwtProvider();
        PasswordHasher hasher = new PasswordHasher();
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegistrationController(NewsModuleContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context; 
            service = new RegisterService(context);
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]  
        public async Task<IActionResult> Register(User user)
        {

            //User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Role",user.Role.ToString()));
                //claims.Add(new Claim("Role",user.Role.ToString()));
                //claims.Add(new Claim(ClaimTypes.Email,user.Email.ToString()));
                await _userManager.AddClaimsAsync(user,claims);


                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            //service.Register(user.Id,user.UserName,user.Email,user.PasswordHash,user.Role);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            //var token = service.Login(user.Email,user.PasswordHash);

            //return (IActionResult)Results.Ok(token);
            var _user = await _userManager.FindByEmailAsync(user.Email);
            var result = await _signInManager.PasswordSignInAsync(_user,user.PasswordHash,false,false);
            if(result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(_user);
                var token = jwtProvider.GenerateToken(user,claims);
                HttpContext context = HttpContext;
                context.Response.Cookies.Append("mycookies", token);

            }

            return RedirectToAction("Index", "Home");
            
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
