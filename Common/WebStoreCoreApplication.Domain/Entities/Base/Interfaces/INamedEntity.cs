namespace WebStoreCoreApplication.Domain.Entities.Base.Interfaces
{
    public interface INamedEntity : IBaseEntity
    {
     
        string Name { get; set; }
    }
}
