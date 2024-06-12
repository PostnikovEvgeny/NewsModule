using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsModule.Data;
using NewsModule.Models;
using Npgsql;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewsModule.Controllers
{
    public class NewsController : Controller
    {
        // GET: NewsController
        public ActionResult Index()
        {
            List<Article> articles = new List<Article>();
            using(NewsModuleContext db = new NewsModuleContext())
            {
                articles = db.Articles.ToList();
            }
            return View(articles);
            
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            Article article = new Article();
           using(NewsModuleContext db = new NewsModuleContext())
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
            using(NewsModuleContext db = new NewsModuleContext())
            {
                db.Articles.Add(article);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
