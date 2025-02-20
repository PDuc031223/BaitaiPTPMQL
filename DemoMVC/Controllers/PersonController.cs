using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    { 
        // GET: /student/
        public IActionResult Index()
        {
            return View();
        } 
        // GET: /student/Welcome/ 

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
    }
}
