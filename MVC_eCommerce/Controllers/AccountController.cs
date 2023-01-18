using MVC_eCommerce.DAL;
using MVC_eCommerce.Models.Account;
using MVC_eCommerce.Repository;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_eCommerce.Controllers
{
    public class AccountController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        // GET: Account
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        // GET: Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginEmploeeVM model)
        {
            if (ModelState.IsValid)
            {
                using (GenericUnitOfWork _unitOfWork = new GenericUnitOfWork())
                {
                    if (_unitOfWork.GetRepositoryInstance<Tbl_Emploee>().GetAllRecords().Any(x => x.UserName.Equals(model.UserName)
                        && x.Password.Equals(model.Password)))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        return Redirect("~/Admin/Admin/Index");
                    }

                    ModelState.AddModelError("CustomError", "Invalid UserName or Password");
                    return View(model);
                }
            }
            return View(model);
        }          

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}