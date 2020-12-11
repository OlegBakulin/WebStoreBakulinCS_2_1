using WebStoreCoreApplication.Domain.Entities.Base.Interfaces;

namespace WebStoreCoreApplication.Domain.Entities.Base
{
    public class OrderEntity : IOrderEntity
    {
        public int Order { get; set; }
        public int Id 
        { 
            get => throw new System.NotImplementedException(); 
            set => throw new System.NotImplementedException(); 
        }
    }
}
