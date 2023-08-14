namespace AuthSystem.Areas.Identity.Data
{
    public class AuthorGenre
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
