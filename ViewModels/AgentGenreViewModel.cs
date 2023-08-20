using AuthSystem.Areas.Identity.Data;

namespace AuthSystem.ViewModels
{
    public class AgentGenreViewModel
    {
        public IEnumerable<Agent> Agents { get; set; }
        
        public IEnumerable<AgentGenre> AgentGenre { get; set; }

        public IEnumerable<Genre> Genre { get; set; }

        public string GenreSearch { get; set; }
    }
}
