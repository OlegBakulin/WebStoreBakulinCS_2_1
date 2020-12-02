using Microsoft.AspNetCore.Identity;
using WebStoreCoreApplication.Domain.Entities.Identity;


namespace WebStoreBakulin.Interfaces.Services.Identity
{
    public interface IRolesClient : IRoleStore<Role> { }
}
