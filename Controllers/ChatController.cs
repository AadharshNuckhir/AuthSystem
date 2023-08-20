using AuthSystem.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Controllers
{
    //[AllowOnlyPublisherRoles]
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
