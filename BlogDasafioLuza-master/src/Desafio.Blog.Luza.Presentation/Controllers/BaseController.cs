using Microsoft.AspNetCore.Mvc;

namespace Desafio.Blog.Luza.Presentation.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> ResponseAsync<T>(Func<Task<T>> function)
        {
            try
            {
                var result = await function();

                if (result == null)
                    return NotFound(new { Message = "Recurso não encontrado." });

                return Ok(new { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Error = ex.Message });
            }
        }
    }
}
