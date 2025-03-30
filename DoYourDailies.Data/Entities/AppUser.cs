using DoYourDailies.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourDailies.Data.Entities
{
    public class AppUser : IEntity
    {
        public int Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime LastUpdatedOn { get; private set; }

        public required string UserId { get; set; }
        public required User User { get; set; }

        public ICollection<DailyTask>? DailyTasks { get; set; }
        public ICollection<WeeklyTask>? WeeklyTask { get; set; }
        public ICollection<MonthlyTask>? MonthlyTask { get; set; }
        public ICollection<YearlyTask>? YearlyTask { get; set; }
        public ICollection<OneTimeTask>? OneTimeTask { get; set; }
        public ICollection<ASAPTask>? ASAPTask { get; set; }
    }
}
