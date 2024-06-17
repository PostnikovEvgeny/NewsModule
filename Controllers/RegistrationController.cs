using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using NewsModule.Models;
using NewsModule.Services;

namespace NewsModule.Controllers
{
    public class RegistrationController : Controller
    {
        RegisterService service = new RegisterService();
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]  
        public IActionResult Register(User user)
        {
            service.Register(user.Id,user.UserName,user.Email,user.PasswordHash,user.Role);
            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var token = service.Login(user.Email,user.PasswordHash);

            //return (IActionResult)Results.Ok(token);

            HttpContext context = HttpContext;
            context.Response.Cookies.Append("mycookies",token);

            return RedirectToAction("Index", "Home");
            
        }

        //[HttpPost]        
        public async Task<IActionResult> Logout()
        {          
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
