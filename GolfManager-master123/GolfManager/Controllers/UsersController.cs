using WaveCenter.Model;
using WaveCenter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WaveCenter.ModelsAPI;

namespace WaveCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;

        public UsersController(UserManager<User> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult<SimpleUser>> PostUser(SimpleUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new User() 
                { 
                    UserName = user.UserName,
                    Email = user.Email, 
                    Apelido = user.Apelido,
                    Nome = user.Nome,
                    DataNascimento = user.DataNascimento,
                    NIF = user.NIF,
                    Morada = user.Morada,
                    IdTipoUser = user.IdTipoUser,
                    Ativo = true,
                }, user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user.Password = null;
            return Created("", user);
        }

        [HttpGet("{username}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserByName(string username)
        {
            User user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return new User
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        // GET: api/Users/email
        [HttpGet("/api/Users/email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            User founduser = await _userManager.FindByEmailAsync(email);

            if (founduser == null)
            {
                return NotFound();
            }

            return founduser;
        }

        // POST: api/Users/BearerToken
        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);

            return Ok(token);
        }
    }
}
