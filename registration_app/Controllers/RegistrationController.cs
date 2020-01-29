using registration_app.Models;
using registration_app.Services;
using registration_app.Services.Interface;
using System.ComponentModel.DataAnnotations;
using System.Web.Http;

namespace registration_app.Controllers
{
    public class RegistrationController : ApiController
    {
        IRegistration registrationRepository;
        public RegistrationController()
        {
            registrationRepository = new RegistrationRepository();
        }

        [HttpPost]
        public IHttpActionResult Post(registration model)
        {
            if (ModelState.IsValid)
            {
                return Ok(registrationRepository.Add(model));
            }
            else {
                return BadRequest();
            }
        }
    }

   
}
