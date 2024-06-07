using Microsoft.AspNetCore.Html;
using System.ComponentModel.DataAnnotations;

namespace NewsModule.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Заголовок")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Дата публикации")]
        public string publishTime { get; set; }
        public Article() 
        {
            this.Id = 0;
            this.Title = "No Title";
            this.Description = "No Text";
            publishTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            
        }
        public Article(int id, string title, string text, DateTime dateTime)
        {
            this.Id = id;
            this.Title = title;
            this.Description = text;
            this.publishTime = dateTime.ToString("MM/dd/yyyy HH:mm");
        }


    }
}
