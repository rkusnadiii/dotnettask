using examplemvc.Models.Request;
using Microsoft.AspNetCore.Mvc;
using examplemvc.Models;
using examplemvc.Data;


namespace examplemvc.Controllers;

public class LoginController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public LoginController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

        [HttpGet("/Home/Login")]
        public IActionResult Login()
        {
            return View("/Views/CRUD/Login.cshtml");
        }

        [HttpPost("/Home/Login")]
        public IActionResult Login(string username, string password)
        {
           var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                DisplaySuccessMessage("Login successful!");
                HttpContext.Session.SetString("user", "admin");
                return RedirectToAction("Read", "Home");
            }
            else
            {
                DisplaySuccessMessage("Login failed. Invalid username or password.");
                return RedirectToAction("Login");
            }
        }

        [HttpGet("/logout")]
        public IActionResult LogoutAction()
        {
            HttpContext.Session.Remove("user");

            return RedirectToAction("LoginPage");
        }

        [HttpGet("/Home/Register")]
        public IActionResult Register()
        {
            return View("/Views/CRUD/Register.cshtml");
        }
        [HttpPost("/Home/Register")]
        public IActionResult Register(string registerUsername, string registerPassword)
        {
            try
            {
                var newUser = new User()
                {
                    Username = registerUsername,
                    Password = registerPassword
                };

                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();

                DisplaySuccessMessage("Registration Success!");

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Registration Failed: {ex.Message}");

                return RedirectToAction("Register");
            }
        }
        [HttpGet("Home/ErrorLogin")]
        public IActionResult ErrorLogin()
        {
            return View("/Views/CRUD/ErrorLogin.cshtml");
        }
            public void DisplaySuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

            public void DisplayErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }
}