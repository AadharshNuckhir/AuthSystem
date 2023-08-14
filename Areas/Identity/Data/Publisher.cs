using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Areas.Identity.Data
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // Navigation property to represent the genres this publisher is interested in
        [BindNever]
        public ICollection<PublisherGenre> InterestedGenres { get; set; }

        // Navigation property to represent the books published by this publisher
        [BindNever]
        public ICollection<Book> PublishedBooks { get; set; }
    }
}
