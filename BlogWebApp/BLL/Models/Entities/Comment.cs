namespace BlogWebApp.BLL.Models.Entities
{
    public class Comment
    {
        public int id { get; set; }

        public string commentTexte { get; set; }
        public string commentCreatedDate { get; set; }
        //ссылка на роли
        public virtual List<Post> Posts { get; set; } = new();

        //ссылка на пользователя
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
