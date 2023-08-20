using AuthSystem.Areas.Identity.Data;

namespace AuthSystem.ViewModels
{
    public class PublisherGenreViewModel
    {
        public IEnumerable<Publisher> Publishers { get; set; }

        public IEnumerable<PublisherGenre> PublisherGenre { get; set; }

        public IEnumerable<Genre> Genre { get; set; }

        public string GenreSearch { get; set; }
    }
}
