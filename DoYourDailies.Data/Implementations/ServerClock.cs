using DoYourDailies.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DoYourDailies.Data.Implementations
{
    public class ServerClock : IServerClock
    {
        private readonly DoYourDailiesContext context;
        private TimeSpan timeOffset;
        private DateTime? lastUpdate;

        public ServerClock(DoYourDailiesContext context)
        {
            this.context = context;
            timeOffset = new TimeSpan();
            lastUpdate = new DateTime();
        }

        public DateTime GetCurrentTime()
        {
            if (DateTime.Today > (lastUpdate?.Date ?? new DateTime()))
            {
                var date = context.GetCurrentTimeFromDb();
                timeOffset = date - DateTime.Now;
                lastUpdate = DateTime.Today;
            }

            return DateTime.Now + timeOffset;
        }

        public async Task<DateTime> GetCurrentTimeAsync()
        {
            if (DateTime.Today > (lastUpdate?.Date ?? new DateTime()))
            {
                var date = await context.GetCurrentTimeFromDbAsync();
                timeOffset = date - DateTime.Now;
                lastUpdate = DateTime.Today;
            }

            return DateTime.Now + timeOffset;
        }
    }
}
