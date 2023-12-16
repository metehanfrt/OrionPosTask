using Microsoft.AspNetCore.Mvc;

namespace OrionPosTask.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        
    }
}
