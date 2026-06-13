using E_commerce.DTO.CategoryDTO;
using E_commerce.DTO.ChileCategroyDTO;
using E_commerce.Model;
using E_commerce.Srevice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildCategoryController : ControllerBase
    {
        private readonly ChildCategoryService _context;
        public ChildCategoryController (ChildCategoryService context)
        {
            _context = context;
        }
        [HttpGet("GetAllChildCategory")]
        public IActionResult GetAllCategory()
        {
            var getResult = _context.GetAlChildCategory();
            if (getResult == null || !getResult.Any())
            {
                return NotFound("لا توجد اي بيانات للعرض ");
            }
            else
            {
                return Ok(getResult);
            }
        }
        [HttpGet("GetChildCategoryById/{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var getResult = _context.GetChildCategory(id);
            if (getResult == null )
            {
                return NotFound("لا توجد اي بيانات للعرض ");
            }
            else
            {
                return Ok(getResult);
            }
        }
        [HttpGet("GetChildCategoryByCategroyId/{id:int}")]
        public IActionResult GetChildCategoryByCategroyId(int id)
        {
            var getResult = _context.GetChildCategoryByCategoryId(id);
            if (getResult == null)
                return NotFound("لا توجد اي بيانات للعرض ");

            return Ok(getResult);
        }
        [HttpPost("AddChildCategory")]
        public IActionResult AddCategory (AddChileCategroyDTO categoryDTO)
        {
            try
            {
                var result =  _context.AddChildCategory(categoryDTO);
                if (result.ErrorCode == "200" )
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpPut("UpdateChildCategoy/{id}")]
        public IActionResult UpdateChildCategory(AddChileCategroyDTO category, int id)
        {
            try
            {
                var result = _context.UpdateChildCategory(category , id);
                if (result.ErrorCode == "200")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpDelete("DelteChildCategory/{id}")]
        public IActionResult DeleteChildCategory(int id)
        {
            try
            {
                var result = _context.DeleteChildCategory( id);
                if (result.ErrorCode == "200")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }

    }
}
