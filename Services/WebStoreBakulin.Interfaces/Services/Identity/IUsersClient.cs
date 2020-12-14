using Microsoft.AspNetCore.Identity;
using WebStoreCoreApplication.Domain.Entities.Identity;


namespace WebStoreBakulin.Interfaces.Services.Identity
{
    public interface IUsersClient :
       IUserRoleStore<User>,
       IUserPasswordStore<User>,
       IUserEmailStore<User>,
       IUserPhoneNumberStore<User>,
       IUserTwoFactorStore<User>,
       IUserClaimStore<User>,
       IUserLoginStore<User>
    {
    }
}
