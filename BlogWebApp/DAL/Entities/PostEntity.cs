namespace BlogWebApp.DAL.Entities
{
    public class PostEntity
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string postText { get; set; }
        public DateTime postCreateDate { get; set; }
    }
}
