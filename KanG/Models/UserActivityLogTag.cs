namespace KanG.Models
{
   
    public class UserActivityLogTag // เชื่อม UserActivityLog <-> Tag (Many-to-Many)
    {
        public int Id { get; set; }

        public int UserActivityLogId { get; set; }
        public UserActivityLog? UserActivityLog { get; set; }

        public int TagId { get; set; }
        public Tag? Tag { get; set; }
    }
}
