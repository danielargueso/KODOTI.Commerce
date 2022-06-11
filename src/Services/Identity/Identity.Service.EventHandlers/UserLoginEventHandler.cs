using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Identity.Persistence.Database;
using Identity.Service.EventHandlers.Commands;
using Identity.Service.EventHandlers.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Service.EventHandlers
{
    public class UserLoginEventHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
	{
        private readonly SignInManager<Domain.ApplicationUser> _signInManager;
        private readonly IdentityServiceDbContext _context;
        private readonly IConfiguration _configuration;
        public UserLoginEventHandler(SignInManager<Domain.ApplicationUser> signInManager, IdentityServiceDbContext context, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<IdentityAccess> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var result = new IdentityAccess();
            var user = await _context.Users.SingleAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);
            var response = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (response.Succeeded)
            {
                result.Succeeded = true;
                await GenerateToken(user, result);
            }

            return result;
        }
        private async Task GenerateToken(Domain.ApplicationUser user, IdentityAccess identity)
        {
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
            };

            var roles = await _context.Roles
                .Where(r => r.UserRoles.Any(u => u.UserId == user.Id))
                .ToListAsync();

            roles.ForEach(role =>
                claims.Add(new Claim(ClaimTypes.Role, role.Name))
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    ),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            identity.AccessToken = tokenHandler.WriteToken(createdToken);
        }
    }
}

