using Core.DbContext;
using Core.Models;
using FastLink.Models;
using Logic.IHelpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FastLink.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;
        private readonly IUserHelper _userHelper;
        public HomeController(AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserHelper userHelper, ILogger<HomeController> logger)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _userHelper = userHelper;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Services()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}

        public IActionResult GenerateTrackId()
        {
            return View();
        }
        public IActionResult TrackOrder()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public async Task<JsonResult> Login(string userName, string password)
        {
            if (userName != null || password != null)
            {
                var filterSpace = userName.Replace(" ", "");
                var existingUser = _userHelper.FindByUserName(filterSpace);
                if (existingUser != null)
                {
                    var PasswordSignIn = await _signInManager.PasswordSignInAsync(existingUser, password, true, true).ConfigureAwait(false);

                    if (PasswordSignIn.Succeeded)
                    {
                        var url = "";
                        var userRole = await _userManager.GetRolesAsync(existingUser).ConfigureAwait(false);
                        if (userRole.Contains("Admin"))
                        {
                            url = "/Admin/Index";
                        }
                        return Json(new { isError = false, dashboard = url });
                    }
                }
                return Json(new { isError = true, msg = "Account does not exist,Contact your Admin" });
            }
            return Json(new { isError = true, msg = "Username and Password Required" });
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}