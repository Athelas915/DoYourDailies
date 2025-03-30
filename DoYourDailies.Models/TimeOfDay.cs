using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourDailies.Models
{
    public struct TimeOfDay
    {
        public TimeOfDay()
        {
            Hour = 0;
            Minute = 0;
            Second = 0;
        }

        public TimeOfDay(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        public TimeOfDay(int seconds)
        {
            var hours = seconds / 3600;
            var remainingSeconds1 = seconds - hours * 3600;
            var minutes = remainingSeconds1 / 60;
            var remainingSeconds2 = remainingSeconds1 - minutes * 60;

            Hour = hours;
            Minute = minutes;
            Second = remainingSeconds2;
        }

        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }

        public int TotalSeconds => Hour * 3600 + Minute * 60 + Second;
    }
}
