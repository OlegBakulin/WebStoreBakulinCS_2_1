using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreBakulin.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
