namespace BlogWebApp.BLL.Models.Entities
{
    public class Post
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string postText { get; set; }
        public string postCreateDate { get; set; }
    }
}
