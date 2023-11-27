using Microsoft.AspNetCore.Mvc;
using ShoppingKart.Data;
using ShoppingKart.Models;
using ShoppingKart.Repository;
using System.Reflection.Metadata.Ecma335;

namespace ShoppingKart.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegisterService registerService;

        public RegisterController(IRegisterService service)
        {
            registerService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] UserModel user)
        {
            try
            {
                if(user == null) 
                { 
                    return BadRequest("User details receiving as null.");
                }

                var RowsAffected = registerService.InsertUser(user);

                if (RowsAffected > 0)
                {
                    var redirectUrl = Url.Action("Index", "Register"); //"../Login/Index";
                    return Json(new { status = true, redirectUrl });
                }
                else
                {
                    return Conflict("Email address already exists.");
                }

            }
            catch (Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }
    }
}
