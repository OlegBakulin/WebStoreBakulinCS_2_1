using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace WebStoreCoreApplication.Domain.Entities.Identity

{
    public class User : IdentityUser
    {
        public const string Administrator = "Administrator";
        public const string DefaultAdminPassword = "AdPass";

        public string Description { get; set; }
    }
}
