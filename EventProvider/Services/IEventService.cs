using EventProvider.Models;

namespace EventProvider.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task<Event> CreateAsync(Event ev);
        Task<bool> UpdateAsync(int id, Event ev);
        Task<bool> DeleteAsync(int id);
    }
}
