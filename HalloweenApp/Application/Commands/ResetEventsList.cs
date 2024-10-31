using HalloweenApp.Application.Abstractions;

namespace HalloweenApp.Application.Commands
{
    public sealed class ResetEventsListHandler(
         IEventRepository _eventRepository)
    {
        public async Task ExecuteAsync()
        {
            await _eventRepository.RemoveAllEventsAsync();
        }
    }
}
