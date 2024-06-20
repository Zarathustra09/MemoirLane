using System.ComponentModel.DataAnnotations.Schema;

namespace MemoirLane.Models
{
    [Table("entry_tags")]
    public class EntryTag
    {
        [ForeignKey("Entry")]
        public int EntryId { get; set; }

        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Entry Entry { get; set; }
        public Tag Tag { get; set; }
    }
}
