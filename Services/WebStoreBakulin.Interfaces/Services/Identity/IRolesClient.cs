using Microsoft.AspNetCore.Identity;
using WebStoreCoreApplication.Domain.Entities.Identity;


namespace WebStore.Interfaces.Services.Identity
{
    public interface IRolesClient : IRoleStore<IdentityRole> { }
}
