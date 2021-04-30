using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace GuildCars.Models.Tables
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
    }
}

