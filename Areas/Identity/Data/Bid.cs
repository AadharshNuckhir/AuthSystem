
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Areas.Identity.Data
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public decimal Price { get; set; }
    }
}
