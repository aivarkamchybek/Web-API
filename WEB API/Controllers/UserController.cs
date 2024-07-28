using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WEB_API.Models;

namespace WEB_API.Controllers
{
    public class UserController : Controller
    {

        public class RegisterController : ApiController
        {
            private ProductContext _context;

            public RegisterController()
            {
                _context = new ProductContext();
            }

            // POST api/register
            public async Task<IHttpActionResult> Register([FromBody] User model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (_context.Users.Any(u => u.UserName == model.UserName))
                {
                    return Conflict();
                }

                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                return CreatedAtRoute("DefaultApi", new { id = model.UserID }, model);
            }
        }
    }
}