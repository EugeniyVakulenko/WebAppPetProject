using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.DTO;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Domain.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authenticate([FromBody] UserDTO user)
        {
            try
            {
                var result = await _authService.Authenticate(user);
                if (result == null) throw new ArgumentNullException();
                _logger.LogInformation($"User {user.UserName} succesfully logged in");
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex);
            }
            catch (WrongCredentialsException ex)
            {
                _logger.LogError(ex, "User entered wrong credentials");
                return NotFound(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while user authentication");
                return BadRequest();
            }
        }
    }
}