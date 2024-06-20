namespace MemoirLane.Models
{
    public class EntryDto
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
