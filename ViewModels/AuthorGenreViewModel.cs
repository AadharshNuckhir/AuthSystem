using AuthSystem.Areas.Identity.Data;

namespace AuthSystem.ViewModels
{
    public class AuthorGenreViewModel
    {
        public IEnumerable<Author> Authors { get; set; }
        
        public IEnumerable<AuthorGenre> AuthorGenre { get; set; }

        public IEnumerable<Genre> Genre { get; set; }

        public string GenreSearch { get; set; }
    }
}
