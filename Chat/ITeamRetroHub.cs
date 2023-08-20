using AuthSystem.Areas.Identity.Data;

namespace AuthSystem.Chat
{
    public interface ITeamRetroHub
    {
        Task ReceiveRetroItem(string message);
        Task UpdateClientsCount(int count);
    }
}
