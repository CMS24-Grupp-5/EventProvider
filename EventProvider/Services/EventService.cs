using EventProvider.Data;
using EventProvider.Models;
using Microsoft.EntityFrameworkCore;

namespace EventProvider.Services
{
    public class EventService : IEventService
    {
        private readonly AppDbContext _context;
        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllAsync() => await _context.Events.ToListAsync();
        public async Task<Event?> GetByIdAsync(int id) => await _context.Events.FindAsync(id);
        public async Task<Event> CreateAsync(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return ev;
        }

        public async Task<bool> UpdateAsync(int id, Event ev)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return false;

            existing.Title = ev.Title;
            existing.Date = ev.Date;
            existing.Location = ev.Location;
            existing.Description = ev.Description;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Events.FindAsync(id);
            if (existing == null) return false;

            _context.Events.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
