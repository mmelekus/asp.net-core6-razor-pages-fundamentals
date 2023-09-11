using WiredBrainCoffeeAdmin.Data.Models;

namespace WiredBrainCoffeeAdmin.Data
{
    public interface ITicketService
    {
        Task<IList<HelpTicket>> GetAll();
        Task<string> Create(HelpTicket ticket);
    }
}
