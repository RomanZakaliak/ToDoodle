using System;
using System.Collections.Generic;
using Baze.Models;

namespace Baze.Models
{
    public class ManageUsersViewModel
    {
        public ApplicationUser[] Administrators { get; set; }

        public ApplicationUser[] Everyone { get; set; } 
    }
}
