﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourDailies.Data
{
    public interface IEntity
    {
        int Id { get; }
        DateTime CreatedOn { get; }
        DateTime LastUpdatedOn { get; }
    }
}
