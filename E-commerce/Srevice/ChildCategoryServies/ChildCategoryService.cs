using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using E_commerce.DTO.CategoryDTO;
using E_commerce.DTO.ChileCategroyDTO;
using E_commerce.Interface;
using E_commerce.Interface.Reposotiry;
using E_commerce.Repositry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;

namespace E_commerce.Srevice
{
    public class ChildCategoryService
    {
        private readonly IChildCategory _context;
        private readonly DBC _contextDB;

        public ChildCategoryService(IChildCategory context , DBC con) {
            _context = context; 
            _contextDB = con;
        }
        public GeneralMsgDto AddChildCategory(AddChileCategroyDTO childCategory)
        {
            var getCategory = _contextDB.childCategories.Where(p => p.childCategoryName == childCategory.childCategoryName && p.categoryId == childCategory.categoryId).ToList().Count;
            if (getCategory > 0) {
                return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "The data must not be duplicated", "400");
            }
            return _context.AddChildCategory(childCategory); 
        }
        public GeneralMsgDto UpdateChildCategory(AddChileCategroyDTO childCategory, int id)
        {
            if (_contextDB.childCategories.Any(p => p.id != id && p.childCategoryName == childCategory.childCategoryName && p.categoryId == childCategory.categoryId))
            {
                return new GeneralMsgDto(
                    IErrorMsgs.not_completed_opration,
                    "Category name already exists",
                    "409"
                );
            }
            return _context.UpdateChildCategory(childCategory , id);
        }
        public GeneralMsgDto DeleteChildCategory(int id)
        {
            return _context.DeleteChildCategory(id);
        }
        public GetChildCategoryDTO GetChildCategory(int id)
        {
            return _context.GetChildCategory(id);
        }
        public List<GetChildCategoryDTO> GetAlChildCategory()
        {
            return _context.GetAlChildCategory();

        }
        public List<GetChildCategoryDTO> GetChildCategoryByCategoryId(int id)
        {
            return _context.GetChildCategoryByCategoryId(id);
        }



    }
}
