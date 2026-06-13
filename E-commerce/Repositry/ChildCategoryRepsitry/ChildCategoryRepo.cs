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
    public class ChildCategoryRepo : IChildCategory
    {
        private readonly DBC _context; 
        public ChildCategoryRepo(DBC con)
        {
            _context = con;
        }
        public GeneralMsgDto AddChildCategory(AddChileCategroyDTO childCategory)
        {
            if (childCategory == null)
            {
                return new GeneralMsgDto(IErrorMsgs.requird_felde, "This fealde is requierd", "400");
            }else
            {
                try
                {
                    _context.childCategories.Add(childCategory.AddChileCategroy());
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
        public GeneralMsgDto UpdateChildCategory(AddChileCategroyDTO childCategory , int id )
        {
            if (id == null)
                return new GeneralMsgDto(IErrorMsgs.not_found, "Not completed this operation", "400");

            var getData = _context.childCategories.FirstOrDefault(p => p.id == id);

            if (getData == null)
                return new GeneralMsgDto(IErrorMsgs.not_found, "Category not found", "404");

            try
            {
                getData.childCategoryName = childCategory.childCategoryName;
                getData.categoryId = childCategory.categoryId;

                var result = _context.SaveChanges();

                return new GeneralMsgDto(SuccessfullyMsgs.successflly_operation, "Successfully completed", "200");
            }
            catch (Exception)
            {
                return new GeneralMsgDto(IErrorMsgs.not_completed_opration, "Not completed this operation", "500");
            }
        }
        public GeneralMsgDto DeleteChildCategory(int id)
        {
            var getDate = _context.childCategories.Where(p => p.id == id).FirstOrDefault();
            if (getDate == null)
            {
                return new GeneralMsgDto(IErrorMsgs.not_found, "Not found category ", "400");
            }
            else
            {
                try
                {
                    _context.childCategories.Remove(getDate); 
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
        public GetChildCategoryDTO GetChildCategory(int id)
        {
            
                try
                {
                string sql = @"	 
                             select 
                             child.id,
                             child.childCategoryName ,
                             main.categoryName
                            from ChildCategory child
                            join Category main on main.id = child.categoryId 
                            where child.id = @id

                        ";

                var result = _context.Database.SqlQueryRaw<GetChildCategoryDTO>
                    (sql , new SqlParameter("id", id)).First();
                        return result;

                }
                catch (Exception ex)
                {
                    return null;

                }

            
        }
        public List<GetChildCategoryDTO> GetAlChildCategory()
        {
            try
            {
                string sql = @"	 
                              select 
                             child.id,
                             child.childCategoryName ,
                             main.categoryName
                            from ChildCategory child
                            join Category main on main.id = child.categoryId 
                        ";

                var result = _context.Database.SqlQueryRaw<GetChildCategoryDTO>
                    (sql).ToList();
                return result;

            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public List<GetChildCategoryDTO> GetChildCategoryByCategoryId(int id)
        {

            try
            {
                string sql = @"	 
                             select 
                             child.id,
                             child.childCategoryName ,
                             main.categoryName
                            from ChildCategory child
                            join Category main on main.id = child.categoryId 

                            where main.id = @id

                        ";

                var result = _context.Database.SqlQueryRaw<GetChildCategoryDTO>
                    (sql, new SqlParameter("id", id)).ToList();
                return result;

            }
            catch (Exception ex)
            {
                return null;

            }


        }
    }
}
