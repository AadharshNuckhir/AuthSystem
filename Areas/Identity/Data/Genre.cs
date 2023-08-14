namespace AuthSystem.Areas.Identity.Data
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        // Navigation property to represent the authors interested in this genre
        public ICollection<AuthorGenre> InterestedAuthors { get; set; }

        // Navigation property to represent the agents interested in this genre
        public ICollection<AgentGenre> InterestedAgents { get; set; }

        // Navigation property to represent the publishers interested in this genre
        public ICollection<PublisherGenre> InterestedPublishers { get; set; }
    }
}
