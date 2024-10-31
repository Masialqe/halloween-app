using HalloweenApp.Domain.Events;
using Microsoft.AspNetCore.SignalR;

namespace HalloweenApp.Sample
{
    public sealed class ClickerHub : Hub
    {
        private static CounterInfo _counterInfo = new();

        public async Task Increment()
        {
            _counterInfo.CounterValue++;

            await Clients.All.SendAsync("UpdateCount", _counterInfo);

        }
        public async Task GetCurrentCount()
        {
            await Clients.Caller.SendAsync("UpdateCount", _counterInfo);
        }
    }
}
