namespace BlogWebApp.BLL.Models.Entities
{
    public class Comment
    {
        public int id { get; set; }

        public string commentTexte { get; set; }
        public string commentCreatedDate { get; set; }
        //ссылка на статью
        public virtual Post Post { get; set; } = new();

        //ссылка на пользователя
        public virtual User User { get; set; }
    }
}
