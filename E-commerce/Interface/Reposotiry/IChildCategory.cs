using E_commerce.DTO.CategoryDTO;
using E_commerce.DTO.ChileCategroyDTO;
using loginpage.DBcon;

namespace E_commerce.Interface.Reposotiry
{
    public interface IChildCategory
    {
        public GeneralMsgDto AddChildCategory (AddChileCategroyDTO category);
        public GeneralMsgDto UpdateChildCategory(AddChileCategroyDTO category, int id); 
        public GeneralMsgDto DeleteChildCategory(int id);
        public GetChildCategoryDTO GetChildCategory(int id);
        public List<GetChildCategoryDTO> GetAlChildCategory( );
        public List<GetChildCategoryDTO> GetChildCategoryByCategoryId(int id);

    }
}
