using LM.Domain.UnitOfWork;
using LM.Domain.Valuables;
using LM.Responses;
using LM.Responses.Extensions;
using LM.VUTTR.Api.Domain.Users.Repository.Contracts;
using LM.VUTTR.Api.Dtos.Users;
using LM.VUTTR.Api.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LM.VUTTR.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        IUserRepository UserRepository { get; }

        IOptions<JTWSettings> Options { get; }

        IUnitOfWork Uow { get; }

        public AccountController(IUserRepository userRepository, IOptions<JTWSettings> options, IUnitOfWork uow)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto dto)
        {
            var response = Response<UserCreatedDto>.Create();

            var email = Email.Create(dto.Email);

            if (email.HasError)
                return BadRequest(response.WithMessages(email.Messages));

            var user = Domain.Users.User.Create(email, dto.Password);

            if (user.HasError)
                return BadRequest(response.WithMessages(user.Messages));

            await UserRepository.AddAsync(user);

            if (!await Uow.CommitAsync())
                return StatusCode(500, response.WithCriticalError("Falha ao tentar salvar o usuário."));

            return Ok(response.SetValue(new UserCreatedDto(user)));
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var response = Response<UserLoggedInDto>.Create();

            var user = await UserRepository.FindAsync(Email.Create(dto.Email));

            if (!user.HasValue)
                return StatusCode(401, response.WithBusinessError(nameof(dto.Email), "Email e/ou senha inválidos."));

            if (!user.Value.CanAuthenticate(dto.Password))
                return StatusCode(401, response.WithBusinessError(nameof(dto.Email), "Email e/ou senha inválidos."));

            var userLoggedInDto = new UserLoggedInDto(user);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Expires = userLoggedInDto.ExpireDate,
                Subject = new ClaimsIdentity(userLoggedInDto.Claims.Select(c => new Claim(c.Type, c.Value))),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Options.Value.Secret)), SecurityAlgorithms.HmacSha256Signature)
            });

            userLoggedInDto.SetToken(tokenHandler.WriteToken(token));

            return Ok(response.SetValue(userLoggedInDto));
        }
    }
}