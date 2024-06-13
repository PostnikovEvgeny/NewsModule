using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsModule.Data;
using NewsModule.Models;
using Npgsql;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewsModule.Controllers
{
    public class NewsController : Controller
    {
        IWebHostEnvironment _env = null;
        public NewsController(IWebHostEnvironment env)
        {
            _env = env;
        }
        // GET: NewsController
        public ActionResult Index()
        {
            List<Article> articles = new List<Article>();
            using (NewsModuleContext db = new NewsModuleContext())
            {
                articles = db.Articles.ToList();
            }
            return View(articles);

        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            Article article = new Article();
            using (NewsModuleContext db = new NewsModuleContext())
            {
                article = db.Articles.Find(id);
            }
            return View(article);
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: NewsController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Article article)
        {
            using (NewsModuleContext db = new NewsModuleContext())
            {
                db.Articles.Add(article);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: NewsController/Edit/5
        public IActionResult Edit(int id)
        {
            Article? article = new Article();
            using (NewsModuleContext db = new NewsModuleContext())
            {
                if (id != null)
                {
                    article = db.Articles.FirstOrDefault(p => p.Id == id);
                }
            }
            return View(article);

        }

        // POST: NewsController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Article article)
        {
            using (NewsModuleContext db = new NewsModuleContext())
            {
                db.Articles.Update(article);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {
            Article? article = new Article();
            using (NewsModuleContext db = new NewsModuleContext())
            {
                if (id != null)
                {
                    article = db.Articles.FirstOrDefault(p => p.Id == id);
                }
            }
            return View(article);
        }

        // POST: NewsController/Delete/5
        //[ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            using (NewsModuleContext db = new NewsModuleContext())
            {
                if (id != null)
                {
                    Article? article = db.Articles.FirstOrDefault(p => p.Id == id);
                    if (article != null)
                    {
                        db.Articles.Remove(article);
                        db.SaveChanges();
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
