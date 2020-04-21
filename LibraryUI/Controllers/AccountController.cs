using AutoMapper;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using LibraryUI.Models;
using System.Web.Mvc;

namespace LibraryUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IMapper _mapper;

        public AccountController(IUserService userService,
            IRoleService roleService,
            IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogIn(LoginViewModel model)
        {
            var item = _userService.GetByEmail(model.Email);
            if (item != null)
            {
                var user = _mapper.Map<UserViewModel>(item);
                Session["CurrentUser"] = user;
                Session["FullName"] = user.FullName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("LogIn", "Account");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var user = _mapper.Map<UserDto>(model);
            user.RoleId = _roleService.GetReaderRole().Id;
            _userService.Create(user);
            return RedirectToAction("LogIn", "Account");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["CurrentUser"] = null;
            Session["FullName"] = null;
            return RedirectToAction("LogIn", "Account");
        }
    }
}