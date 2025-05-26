using EventProvider.Models;
using EventProvider.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventProvider.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _eventService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            return ev == null ? NotFound() : Ok(ev);
        }

        // [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Event ev)
        {
            var created = await _eventService.CreateAsync(ev);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Event ev)
        {
            var success = await _eventService.UpdateAsync(id, ev);
            return success ? NoContent() : NotFound();
        }

        // [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _eventService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
