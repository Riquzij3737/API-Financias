using ApiFinacias.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinacias.Controllers;

[Controller]
[Route("/Api/")]
public class ViewController : Controller
{
    [Route("Help")]
    public IActionResult Help()
    {
        return View();
    }

    [Route("Help/Error")]
    public IActionResult Error()
    {     
        return View();
    }
}
