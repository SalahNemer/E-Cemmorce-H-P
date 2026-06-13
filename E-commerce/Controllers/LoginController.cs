using E_commerce.DTO.Login;
using Microsoft.AspNetCore.Mvc;
using V1.Service;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly loginserves _loginService;

    public AuthController(loginserves loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] loginDto entity)
    {
        try
        {
            var result = _loginService.login(entity);

            if (result.ErrorMessage != "Successful Login")
                return BadRequest(result);

            return Ok(result); 
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "حدث خطأ اثناء التنفيذ يرجى المحاولة مرة اخرى",
                details = ex.Message
            });
        }
    }
}
