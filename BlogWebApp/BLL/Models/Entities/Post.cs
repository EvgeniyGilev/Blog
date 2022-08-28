namespace BlogWebApp.BLL.Models.Entities
{
    public class Post
    {
        public int id { get; set; }
        public string postName { get; set; }
        public string postText { get; set; }
        public string ?postCreateDate { get; set; }

        //ссылка на пользователя
        public virtual User? User { get; set; }

        //ссылка на теги
        public virtual List<Tag> Tags { get; set; } = new();

        //ссылка на комментарии
        public virtual List<Comment> Comments { get; set; } = new();
    }
}
