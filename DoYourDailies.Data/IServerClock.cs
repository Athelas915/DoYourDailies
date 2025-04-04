﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourDailies.Data
{
    public interface IServerClock
    {
        DateTime GetCurrentTime();
        Task<DateTime> GetCurrentTimeAsync();
    }
}
