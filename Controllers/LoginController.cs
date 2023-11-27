using Microsoft.AspNetCore.Mvc;
using ShoppingKart.Repository;
using ShoppingKart.Service;
using System.Reflection;

namespace ShoppingKart.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Validate(string email, string password)
        {

            var user = _service.FindUserByEmail(email);

            if(user == null)
            {
                return Ok(new { valid = false });
            }
            else
            {
                var IsValid = _service.ValidateUser(user, password);
                var redirectUrl = Url.Action("Index", "Home");
                return Ok(new { valid = IsValid, redirectUrl });
            }
        }
    }
}
