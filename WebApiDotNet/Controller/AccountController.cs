using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiDotNet.Dto;
using WebApiDotNet.Models;

namespace WebApiDotNet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configration;

        public AccountController(UserManager<ApplicationUser> UserManager,IConfiguration configration)
        {
            userManager = UserManager;
            this.configration = configration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto userfromReguest)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userfromReguest.username;
                user.Email = userfromReguest.email;

                IdentityResult result = await userManager.CreateAsync(user, userfromReguest.password);
                if (result.Succeeded)
                {
                    return Ok("create");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("Passowrd", item.Description);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userfromreqest)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser userfromdb = await userManager.FindByNameAsync(userfromreqest.name);

                if (userfromdb != null)
                {

                    bool found = await userManager.CheckPasswordAsync(userfromdb, userfromreqest.password);
                    if (found == true)
                    {
                        //genetae token
                        List<Claim>userclaims= new List<Claim>();

                        userclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));


                        userclaims.Add(new Claim(ClaimTypes.NameIdentifier, userfromdb.Id));
                   
                        userclaims.Add(new Claim(ClaimTypes.Name, userfromdb.UserName));


                        var userroles =await userManager.GetRolesAsync(userfromdb);
                        foreach (var rolename in userroles)
                        {
                            userclaims.Add(new Claim(ClaimTypes.Role,rolename));

                        }
                        var SignInkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configration["JWt:secritkey"]));
 
                        SigningCredentials signingCredentials = new SigningCredentials(SignInkey, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            audience: configration["JWt:AudianceIp"],
                            issuer: configration["JWt:IssuerIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: userclaims,
                            signingCredentials: signingCredentials

                            );
                        return Ok(new
                        {

                            token= new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expirartion= DateTime.Now.AddHours(1) ,
                            
                        }
                            );

                    }


                }


                ModelState.AddModelError("name", "username or passord Invalied");


            }
            return BadRequest(ModelState);


        }
    }
}