using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


using backend.Models;
using backend.DbContext;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {   private readonly ILogger<LoginController> logger;

        private readonly UserOperation userOperation;  

        private IConfiguration config;
  
        public LoginController(UserOperation _userOperation, ILogger<LoginController> _logger, IConfiguration _config)  
        {  
            userOperation = _userOperation ;  
            logger = _logger;
            config = _config;
        }  


        // GET /values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST /values
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]User loginUser)
        {
            var authUser = AuthenticateUser(loginUser);

            if(authUser != null){

                var tokenString = GenerateJSONWebToken(authUser);

                Response.Headers.Add("jwt_token", tokenString);
                return Ok(authUser);
            }
            else
                return Ok();


        }

        private User AuthenticateUser(User loginUser)
        {
            User dbUser = userOperation.Get(loginUser.EmpId);

            if(dbUser == null){
                Response.Headers.Add("login_code", "FAIL");
                return null;
            }
            else{
                if(dbUser.Password == loginUser.Password){
                    User loggedUser = new User(dbUser._id, dbUser.EmpId, dbUser.Name, dbUser.Email);
                    Response.Headers.Add("login_code", "SUCCESS");

                    return loggedUser;
                    
                }
                else {
                    Response.Headers.Add("login_code", "FAIL");
                    return null;
                }
            } 
        }

        private string GenerateJSONWebToken(User loggedUser)    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var claims = new[] {    
                             new Claim("Name", loggedUser.Name), 
                             new Claim("EmpID", loggedUser.EmpId),    
   
                         }; 

            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"],    
                        claims, expires: DateTime.Now.AddMinutes(60),    
                        signingCredentials: credentials
                        );    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        }    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
