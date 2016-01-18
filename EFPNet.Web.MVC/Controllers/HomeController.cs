using System.Web.Mvc;
using EFPNet.IService;

namespace EFPNet.Web.MVC.Controllers
{
    public class HomeController : ControllerBase
    {
        private static IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //_itest.TestMetord();
            return View();
        }

        public ActionResult SuccessPage(string pagetitle, string retaction, string retcontroller, int waitseconds)
        {
            throw new System.NotImplementedException();
        }
    }
}
