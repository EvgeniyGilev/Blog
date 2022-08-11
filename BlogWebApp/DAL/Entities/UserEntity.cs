namespace BlogWebApp.DAL.Entities
{
    public class UserEntity
    {
        public int id { get; set; }
        public string userName { get; set; }
        public int roleId { get; set; }
        public string userPassword { get; set; }
        public string userLogin { get; set; }
        public DateTime userCreatedDate { get; set; }

    }
}
