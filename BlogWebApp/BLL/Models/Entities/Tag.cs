namespace BlogWebApp.BLL.Models.Entities
{
    public class Tag
    {
        public int id { get; set; }
        public string tagText { get; set; }

        //ссылка на статьи
        public virtual List<Post> Posts { get; set; } = new();
    }
}
