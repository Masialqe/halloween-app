using HalloweenApp.Application.Abstractions;
using HalloweenApp.Domain.Events;

namespace HalloweenApp.Application.Commands
{
    public sealed class LoadEventsHandler(
        IEventRepository _eventRepository)
    {
        public async Task<List<Event>> ExecuteAsync()
        {
            try
            {
                var result = await _eventRepository.GetAllAsync();

                return result.ToList();
            }
            catch
            {
                return new List<Event>();
            }
        }
    }
}
