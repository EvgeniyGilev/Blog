namespace BlogWebApp.DAL.Entities
{
    public class CommentEntity
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string commentTextle { get; set; }
        public DateTime commentCreatedDate { get; set; }
    }
}
