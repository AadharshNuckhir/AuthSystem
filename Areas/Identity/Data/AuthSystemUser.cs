using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AuthSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AuthSystemUser class
public class AuthSystemUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual Author Author { get; set; }
    public virtual Agent Agent { get; set; }
    public virtual Publisher Publisher { get; set; }
}

