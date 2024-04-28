using Microsoft.AspNetCore.Mvc;
using Sample.Application.Interfaces;
using Sample.Infra.CrossCutting.Security.Models;
using System.Security;

namespace Sample.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthUserRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authenticationService.Authenticate(model);

                return Ok(result);
            }
            catch (SecurityException ex)
            {
                ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
