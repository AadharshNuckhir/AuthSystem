using Humanizer.Localisation;

namespace AuthSystem.Areas.Identity.Data
{
    public class AgentGenre
    {
        public int Id { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
