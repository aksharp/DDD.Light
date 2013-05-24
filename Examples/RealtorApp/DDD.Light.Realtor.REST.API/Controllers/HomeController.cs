using System.Web.Mvc;

namespace DDD.Light.Realtor.REST.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
