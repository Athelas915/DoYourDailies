﻿using DoYourDailies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourDailies.Data.Entities
{
    public class ASAPTask
    {
        public int Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime LastUpdatedOn { get; private set; }
        public required string Name { get; set; }
        public bool Completed { get; set; }

        public required int AppUserId { get; set; }
        public required AppUser AppUser { get; set; }
    }
}
