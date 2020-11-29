using WebStoreCoreApplication.Domain.DTO.Products;
using WebStoreCoreApplication.Domain.Entities;

namespace WebStoreBakulin.Services.Mapping
{
    public static class CategoryDTOMapper
    {
        public static CategoryDTO ToDTO(this Category Section) => Section is null ? null : new CategoryDTO
        {
            Id = Section.Id,
            Name = Section.Name,
            Order = Section.Order,
            ParentId = Section.ParentId,
        };

        public static Category FromDTO(this CategoryDTO Section) => Section is null ? null : new Category
        {
            Id = Section.Id,
            Name = Section.Name,
            ParentId = Section.ParentId,
            Order = Section.Order,
        };
    }
}