using E_commerce.DTO.CategoryDTO;
using E_commerce.Model;

namespace E_commerce.Mapper
{
    public static class AddCategoryMapper
    {
        public static Category AddCategory (this AddCategoryDTO category)
        {
            return new Category
            {
                categoryName = category.categoryName 
            }; 
        }
    }
}
