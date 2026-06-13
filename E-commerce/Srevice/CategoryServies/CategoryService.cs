using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using E_commerce.DTO.CategoryDTO;
using E_commerce.DTO.ChileCategroyDTO;
using E_commerce.Interface;
using E_commerce.Interface.Reposotiry;
using loginpage.DBcon;
using loginpage.ErrorMsgs;

namespace E_commerce.Srevice
{
    public class CategoryService
    {
        private readonly ICategory _context;
        private readonly DBC _contextDB;

        public CategoryService(ICategory context , DBC con) {
            _context = context; 
            _contextDB = con;
        }
        public GeneralMsgDto AddCategory(AddCategoryDTO category)
        {
            var getCategory = _contextDB.categories.Where(p => p.categoryName == category.categoryName).ToList().Count;
            if (getCategory > 0) {
                return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "The data must not be duplicated", "400");
            }
            return _context.AddCategory(category); 
        }
        public GeneralMsgDto UpdateCategory(AddCategoryDTO category, int id)
        {
            if (_contextDB.categories.Any(p => p.id != id && p.categoryName == category.categoryName))
            {
                return new GeneralMsgDto(
                    IErrorMsgs.data_must_no_be_duplicated,
                    "Category name already exists",
                    "409"
                );
            }
            return _context.UpdateCategory(category , id);
        }
        public GeneralMsgDto DeleteCategory(int id)
        {
            return _context.DeleteCategory(id);
        }
        public GetCategoryDTO GetCategory(int id)
        {
            return _context.GetCategory(id);
        }
        public List<GetCategoryDTO> GetAlCategory()
        {
            return _context.GetAlCategory();

        }


    }
}
