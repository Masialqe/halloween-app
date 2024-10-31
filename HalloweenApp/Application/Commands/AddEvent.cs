using HalloweenApp.Application.Abstractions;
using HalloweenApp.Domain.Events;
using System.ComponentModel.DataAnnotations;

namespace HalloweenApp.Application.Commands
{
    public sealed class AddEventRequest
    {
        [Length(5, 255, ErrorMessage = "Event name has to be in 5-255.")]
        public string Name { get; set; } = string.Empty;

        [Length(5, 255, ErrorMessage = "Event desc has to be in 5-500.")]
        public string Description { get; set; } = string.Empty;
    }

    public sealed class AddEventHandler(
        IEventRepository _eventRepository)
    {
        public async Task ExecuteAsync(
            AddEventRequest newEvent, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var eventObject = Event.Create(newEvent.Name, newEvent.Description);
                await _eventRepository.SaveAsync(eventObject, cancellationToken);
            }
            catch { }
        }
    }
}
