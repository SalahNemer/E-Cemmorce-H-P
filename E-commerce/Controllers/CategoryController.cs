using E_commerce.DTO.CategoryDTO;
using E_commerce.Model;
using E_commerce.Srevice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _context;
        public CategoryController (CategoryService context)
        {
            _context = context;
        }
        [HttpGet("GetAllCategory")]
        public IActionResult GetAllCategory()
        {
            var getResult = _context.GetAlCategory();
            if (getResult == null || !getResult.Any())
            {
                return NotFound("لا توجد اي بيانات للعرض");
            }
            else
            {
                return Ok(getResult);
            }
        }
        [HttpGet("GetCategoryById/{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var getResult = _context.GetCategory(id);
            if (getResult == null )
            {
                return NotFound("لا توجد اي بيانات للعرض");
            }
            else
            {
                return Ok(getResult);
            }
        }
        [HttpPost("AddCategory")]
        public IActionResult AddCategory (AddCategoryDTO categoryDTO)
        {
            try
            {
                var result =  _context.AddCategory(categoryDTO);
                if (result.ErrorCode == "200" )
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpPut("UpdateCategoy/{id}")]
        public IActionResult UpdateCategory (AddCategoryDTO category, int id)
        {
            try
            {
                var result = _context.UpdateCategory(category , id);
                if (result.ErrorCode == "200")
                    return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                throw new Exception("حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى \n", ex);
            }
        }
        [HttpDelete("DelteCategory/{id}")]
        public IActionResult DeleteCategory (int id)
        {
            try
            {
                var result = _context.DeleteCategory( id);
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
