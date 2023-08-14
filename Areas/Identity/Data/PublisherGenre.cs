namespace AuthSystem.Areas.Identity.Data
{
    public class PublisherGenre
    {
        public int Id { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
