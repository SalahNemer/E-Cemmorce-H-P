using DataBase.DBcon;
using DevetionStudetns.Error.SuccessfullyMsg;
using E_commerce.DTO.CategoryDTO;
using E_commerce.DTO.ChileCategroyDTO;
using E_commerce.Interface;
using E_commerce.Interface.Reposotiry;
using E_commerce.Mapper;
using E_commerce.Model;
using loginpage.DBcon;
using loginpage.ErrorMsgs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Repositry
{
    public class CategoryRepo : ICategory
    {
        private readonly DBC _context; 
        public CategoryRepo(DBC con)
        {
            _context = con;
        }
        public GeneralMsgDto AddCategory(AddCategoryDTO category)
        {
            if ( category == null)
            {
                return new GeneralMsgDto(IErrorMsgs.requird_felde, "This fealde is requierd", "400");
            }else
            {
                try
                {
                    _context.categories.Add(category.AddCategory());
                    var reult =  _context.SaveChanges();
                    if (reult == 1)
                    {
                        return new GeneralMsgDto(SuccessfullyMsgs.successflly_operation, "Successfully completed ", "200");

                    }
                    return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "Not completed this opration", "400");
                }
                catch (Exception ex)
                {
                    return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "Not completed this opration", "400");

                }
            }

        }
        public GeneralMsgDto UpdateCategory(AddCategoryDTO category , int id )
        {
            if (id == null)
                return new GeneralMsgDto(IErrorMsgs.not_found, "Not completed this operation", "400");

            var getData = _context.categories.FirstOrDefault(p => p.id == id);

            if (getData == null)
                return new GeneralMsgDto(IErrorMsgs.not_found, "Category not found", "404");

            try
            {
                getData.categoryName = category.categoryName;

                var result = _context.SaveChanges();

                return new GeneralMsgDto(SuccessfullyMsgs.successflly_operation, "Successfully completed", "200");
            }
            catch (Exception)
            {
                return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "Not completed this operation", "500");
            }
        }
        public GeneralMsgDto DeleteCategory(int id)
        {
            var getDate = _context.categories.Where(p => p.id == id).FirstOrDefault();
            if (getDate == null)
            {
                return new GeneralMsgDto(IErrorMsgs.not_found, "Not found category ", "400");
            }
            else
            {
                try
                {
                    _context.categories.Remove(getDate); 
                    var result = _context.SaveChanges();
                    if (result == 1)
                    {
                        return new GeneralMsgDto(SuccessfullyMsgs.successflly_operation, "Successfully completed ", "200");

                    }
                    return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "Not completed this opration", "400");
                }
                catch (Exception ex)
                {
                    return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "Not completed this opration", "400");

                }

            }
        }
        public GetCategoryDTO GetCategory(int id)
        {
            
                try
                {
                string sql = @"	 
                            select * from Category where id = @id 
                        ";

                var result = _context.Database.SqlQueryRaw<GetCategoryDTO>
                    (sql , new SqlParameter("id", id)).First();
                        return result;

                }
                catch (Exception ex)
                {
                    return null;

                }

            
        }
        public List<GetCategoryDTO> GetAlCategory()
        {
            try
            {
                string sql = @"	 
                             select * from Category
                        ";

                var result = _context.Database.SqlQueryRaw<GetCategoryDTO>
                    (sql).ToList();
                return result;

            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
