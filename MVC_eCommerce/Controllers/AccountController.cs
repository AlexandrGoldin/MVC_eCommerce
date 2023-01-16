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

        // GET: Account/UserProfile
        [Authorize]
        [HttpGet]
        public ActionResult UserProfile()
        {
            string userName = User.Identity.Name;
            EmploeeProfileVM model;
            Tbl_Emploee tbl_Emploee = _unitOfWork.GetRepositoryInstance<Tbl_Emploee>().GetAllRecords().FirstOrDefault(x => x.UserName == userName);
            model = AutoMapper.Mapper.Map<EmploeeProfileVM>(tbl_Emploee);
            return View("UserProfile", model);
        }

        //[HttpPost]
        //[Authorize]
        public ActionResult UserProfile(EmploeeProfileVM model)
        {
            bool userNameIsChanged = false;
            if (!ModelState.IsValid)
            {
                return View("UserProfile", model);
            }
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match");
                    return View("UserProfile", model);
                }
            }
            if (string.IsNullOrWhiteSpace(model.Password))
                model.Password = _unitOfWork.GetRepositoryInstance<Tbl_Emploee>().GetFirstorDefault(model.Id).Password;

            string userName = User.Identity.Name;
            if (userName != model.UserName)
            {
                userName = model.UserName;
                userNameIsChanged = true;

                if (_unitOfWork.GetRepositoryInstance<Tbl_Emploee>().GetAllRecords().Any(x => x.UserName == model.UserName))
                {
                    ModelState.AddModelError("", $"Username{model.UserName} alredy exists");
                    model.UserName = "";
                    return View("UserProfile", model);
                }
            }

            using (GenericUnitOfWork _unitOfWork = new GenericUnitOfWork())
            {
                _unitOfWork.GetRepositoryInstance<Tbl_Emploee>().Update(AutoMapper.Mapper.Map<Tbl_Emploee>(model));
                _unitOfWork.SaveChanges();
            }

            TempData["SM"] = "You have edited your profile!";
            if (!userNameIsChanged)
                return View("UserProfile", model);

            else
                return RedirectToAction("Logout");
        }
    }
}