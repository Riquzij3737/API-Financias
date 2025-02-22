using Microsoft.AspNetCore.Mvc;

namespace ApiFinancias.Controllers
{
    [Controller]
    [Route("/Api/[controller]")]
    public class DefinitionsController : Controller
    {
        [HttpPost("GetDefinitions")]
        public IActionResult GetKeys([FromQuery] string Key)
        {
            if (Key == "ChaveMuitoSegura")
            {
                return Ok("Chave Correta");
            }
            else
            {
                return BadRequest("Chave inválida");
            }
        }
    }
}
