using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;


namespace HappyVet.Areas.Identity.Data
{
    public class AplicationUser : IdentityUser

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
