using MemoirLane.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoirLane.Models
{
    [Table("comments")]
    public class Comment
    {
        public int Id { get; set; }


        [ForeignKey("Entry")]
        public int Entry_Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Entry Entry { get; set; }
        public User User { get; set; }
    }
}
