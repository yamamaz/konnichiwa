using System.Drawing.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;

namespace Web.Controllers.Authentication;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IKonnichiwaDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthController(IKonnichiwaDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }
    [HttpPost("Auth/LoginByUsername")]
    public IActionResult AuthenticateByUsername([FromBody] UserLoginDto credential)
    {
        var user = _context.Users.FirstOrDefault(x => x.UserName == credential.UserName);
        if (credential.UserName == user.UserName && credential.Password == user.Password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var expiresAt = DateTime.UtcNow.AddDays(2);
            return Ok
                (
                    new
                    {
                        access_token = CreateToken(claims, expiresAt),
                        expires_at = expiresAt
                    });
        }
        ModelState.AddModelError("Unauthorized", "Please Login to view the contents.");
        return Unauthorized(ModelState);
    }
    [HttpPost("Auth/LoginByEmail")]
    public IActionResult AuthenticateByEmail([FromBody]UserLoginByEmailDto credential)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == credential.Email);
        if (credential.Email == user.Email && credential.Password == user.Password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var expiresAt = DateTime.UtcNow.AddDays(2);
            return Ok
                (
                    new
                    {
                        access_token = CreateToken(claims, expiresAt),
                        expires_at = expiresAt
                    });
        }
        ModelState.AddModelError("Unauthorized", "Please Login to view the contents.");
        return Unauthorized(ModelState);

    }
    private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
    {
        var secretKey = Encoding.ASCII.GetBytes("super secret key");
        var jwt = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiresAt,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

}
