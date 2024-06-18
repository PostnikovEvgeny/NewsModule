using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NewsModule.Data;
using NewsModule.Models;
using Npgsql;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewsModule.Controllers
{
    public class NewsController : Controller
    {
        NewsModuleContext context;

        IWebHostEnvironment _env = null;
        public NewsController(IWebHostEnvironment env, NewsModuleContext context)
        {
            _env = env;
            this.context=context;
            
        }

        public ActionResult Index()
        {

            List<Article> articles = new List<Article>();
            using (context)
            {
                articles = context.Articles.ToList();
            }
            return View(articles);

        }

        public ActionResult Details(int id)
        {
            Article article = new Article();
            using (context)
            {
                article = context.Articles.Find(id);
            }
            return View(article);
        }

        [Authorize] 
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Article article)
        {
            using (context)
            {
                context.Articles.Add(article);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize("OnlyAdmin")]
        public IActionResult Edit(int id)
        {
            Article? article = new Article();
            using (context)
            {
                if (id != null)
                {
                    article = context.Articles.FirstOrDefault(p => p.Id == id);
                }
            }
            return View(article);

        }

        [HttpPost]
        [Authorize("OnlyAdmin")]
        public async Task<IActionResult> Edit(Article article)
        {
            using (context)
            {
                context.Articles.Update(article);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [Authorize("OnlyAdmin")]
        public ActionResult Delete(int id)
        {
            Article? article = new Article();
            using (context)
            {
                if (id != null)
                {
                    article = context.Articles.FirstOrDefault(p => p.Id == id);
                }
            }
            return View(article);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize("OnlyAdmin")]
        public IActionResult ConfirmDelete(int id)
        {
            using (context)
            {
                if (id != null)
                {
                    Article? article = context.Articles.FirstOrDefault(p => p.Id == id);
                    if (article != null)
                    {
                        context.Articles.Remove(article);
                        context.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult UploadImage(List<IFormFile> files)
        {
            var filepath = "";
            foreach(IFormFile photo in Request.Form.Files)
            {
                string serverMapPath = Path.Combine(_env.WebRootPath, "Image", photo.FileName);
                using(var stream = new FileStream(serverMapPath,FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                filepath = "https://localhost:7146/" + "Image/" + photo.FileName;
            }
            return Json(new { url = filepath });
        }
    }
}
