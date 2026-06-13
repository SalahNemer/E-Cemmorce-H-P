using E_commerce.DTO.CategoryDTO;
using E_commerce.DTO.ChileCategroyDTO;
using loginpage.DBcon;

namespace E_commerce.Interface.Reposotiry
{
    public interface ICategory
    {
        public GeneralMsgDto AddCategory (AddCategoryDTO category);
        public GeneralMsgDto UpdateCategory(AddCategoryDTO category, int id); 
        public GeneralMsgDto DeleteCategory(int id);
        public GetCategoryDTO GetCategory (int id);
        public List<GetCategoryDTO> GetAlCategory ( );
    }
}
