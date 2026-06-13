using E_commerce.DTO.ChileCategroyDTO;
using E_commerce.Model;

namespace E_commerce.Mapper
{
    public static class AddChileCategroyMapper
    {
        public static ChildCategory AddChileCategroy (this AddChileCategroyDTO addChileCategroy)
        {
            return new ChildCategory
            {
                childCategoryName = addChileCategroy.childCategoryName , 
                categoryId = addChileCategroy.categoryId ,
            };
        }
    }
}
