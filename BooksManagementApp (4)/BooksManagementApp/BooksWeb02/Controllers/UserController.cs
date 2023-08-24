using BooksWeb02.Models;
using ConceptArchitect.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BooksWeb02.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService user)
        {
            this.userService = user;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Login(string id)
        {
            var user = await userService.GetUser(id);
            return View(user);
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            await userService.Add(user);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}