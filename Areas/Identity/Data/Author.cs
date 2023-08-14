using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Areas.Identity.Data
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int? AgentId { get; set; } // Nullable foreign key
        public Agent Agent { get; set; } // Navigation property

        // Navigation property to represent the genres this author writes in
        [BindNever]
        public ICollection<AuthorGenre> WritingGenres { get; set; }

        // Navigation property to represent the books authored by this author
        [BindNever]
        public ICollection<Book> Books { get; set; }
    }
}
