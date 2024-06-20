using MemoirLane.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoirLane.Models
{
    [Table("entries")]
    public class Entry
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public DateTime Updated_At { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<EntryTag> EntryTags { get; set; }
    }
}
