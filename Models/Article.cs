using System.ComponentModel.DataAnnotations;

namespace NewsModule.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public string dateTime { get; set; }
        public Article() 
        {
            this.Id = 0;
            this.Title = "No Title";
            this.Text = "No Text";
            dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            
        }
        public Article(int id, string title, string text, DateTime dateTime)
        {
            this.Id = id;
            this.Title = title;
            this.Text = text;
            this.dateTime = dateTime.ToString("MM/dd/yyyy HH:mm");
        }


    }
}
