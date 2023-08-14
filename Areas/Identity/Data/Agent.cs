using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Areas.Identity.Data
{
    public class Agent
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AgencyName { get; set; }

        // Navigation property to represent the genres this agent is interested in
        [BindNever]
        public ICollection<AgentGenre?> InterestedGenres { get; set; }
    }
}
