using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            List<Article> dispArt = new List<Article>();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=NewsDB;User Id=postgres;Password=12345;");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM NewsTable";
            NpgsqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                var article = new Article();
                article.Id = Convert.ToInt32(sdr["id"]);
                article.Title = sdr["title"].ToString();
                article.publishTime = sdr["publishtime"].ToString();
                article.Description = sdr["text"].ToString();
                dispArt.Add(article);
            }
            conn.Close();
            return View(dispArt);
            
        }

        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            //var art2 = new Article(1, "ad", "asd", DateTime.Now);
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=NewsDB;User Id=postgres;Password=12345;");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM NewsTable WHERE id = {id}";
            NpgsqlDataReader sdr = cmd.ExecuteReader();
            var article = new Article();
            while (sdr.Read())
            {
                article.Id = Convert.ToInt32(sdr["id"]);
                article.Title = sdr["title"].ToString();
                article.publishTime = sdr["publishtime"].ToString();
                article.Description = sdr["text"].ToString();
                
            }
            conn.Close();
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
            article.publishTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=NewsDB;User Id=postgres;Password=12345;");
            conn.Open();
            
            await using var cmdI = new NpgsqlCommand($"INSERT INTO NewsTable (title,publishtime,text) VALUES ('{article.Title}','{article.publishTime}','{article.Description}')", conn);
            await cmdI.ExecuteNonQueryAsync();
            conn.Close();

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
