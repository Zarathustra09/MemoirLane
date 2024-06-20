using System.ComponentModel.DataAnnotations.Schema;

namespace MemoirLane.Models
{
    [Table("tags")]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<EntryTag> EntryTags { get; set; }
    }
}
