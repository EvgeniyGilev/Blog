namespace BlogWebApp.BLL.Models.Entities
{
    public class Comment
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string commentTextle { get; set; }
        public string commentCreatedDate { get; set; }
    }
}
