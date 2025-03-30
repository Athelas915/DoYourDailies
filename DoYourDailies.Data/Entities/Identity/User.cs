using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoYourDailies.Data.Entities.Identity
{
    public class User : IdentityUser
    {
        public AppUser? AppUser { get; set; }
    }
}
