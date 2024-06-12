using Microsoft.AspNetCore.Html;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsModule.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }     
        
        [Display(Name = "Дата публикации")]
        [Required]
        [Column(TypeName = "timestamp(6)")]
        public DateTime publishTime { get; set; } = DateTime.Now;

        [Display(Name = "Заголовок")]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "Аннотация")]
        [Required]
        [MaxLength(300)]
        public string Annotation {  get; set; }

        [Display(Name = "Текст")]
        [Required]
        public string Description { get; set; }

        public Article() 
        {
            this.Id = 0;
            this.Annotation = "No annotation";
            this.Title = "No Title";
            this.Description = "No Text";
            publishTime = DateTime.Now;
            
        }
        public Article(int id, DateTime dateTime, string title, string annotation,  string text)
        {
            this.Id = id;
            this.Annotation = annotation;
            this.Title = title;
            this.Description = text;
            this.publishTime = dateTime;
        }


    }
}
