using System.ComponentModel;

namespace AuthSystem.Areas.Identity.Data
{
    public class RetrospectiveItem
    {
        public RetrospectiveItem()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
