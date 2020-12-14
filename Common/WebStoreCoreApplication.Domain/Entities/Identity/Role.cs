using Microsoft.AspNetCore.Identity;

namespace WebStoreCoreApplication.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string Administrator = "Administrators";
        public const string User = "Users";

        public string Description { get; set; }
    }
}