using System.Web.Mvc;

namespace DraftCraft.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult FileUploadLimitExceeded()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}