using CwoPqsCrosswalk.Models;
using Microsoft.AspNetCore.Mvc;

namespace CwoPqsCrosswalk.Controllers
{
    public class OfficerController : Controller
    {
        // 
        // GET: /Officer/

        public IActionResult Index()
        {
            return View(new Officer());
        }
    }
}
