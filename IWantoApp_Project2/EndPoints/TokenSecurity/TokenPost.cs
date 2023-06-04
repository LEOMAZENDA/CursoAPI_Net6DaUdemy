using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IWantoApp_Project2.EndPoints.TokenSecurity;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Mehods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;


    //Este endpoint adiciona um user Identity
    public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager)
    {
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        if (user == null)
            Results.BadRequest();
        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            Results.BadRequest();

        //GERANDO O TOKEN 
        var key = Encoding.ASCII.GetBytes("@ASDsfjjk#?9743sfgkkf95uçghas");
        var tokekDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, loginRequest.Email),
            }),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "IWantoApp_Project2",
            Issuer = "Issuer"
        };

        var tokenHendler = new JwtSecurityTokenHandler();
        var token = tokenHendler.CreateToken(tokekDescriptor);
        return Results.Ok(new
        {
            token = tokenHendler.WriteToken(token),
        });
    }
}