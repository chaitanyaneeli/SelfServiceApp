using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.Models;
using backend.DbContext;

using Microsoft.AspNetCore.Authorization;


namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {   private readonly ILogger<RegisterController> logger;

        private readonly UserOperation userOperation;  
  
        public RegisterController(UserOperation _userOperation, ILogger<RegisterController> _logger)  
        {  
            userOperation = _userOperation ;  
            logger = _logger;
        }  

        // public IActionResult Index()
        // {
        //     return View();
        // }

        // public IActionResult Privacy()
        // {
        //     return View();
        // }

         // GET /values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
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
        public IActionResult Post([FromBody]User regUser)
        {
             string code = userOperation.NewUser(regUser);

            if(code.Equals("SUCCESS")){
                
                Response.Headers.Add("reg_code", "SUCCESS");
                return Ok();
            }
            else
                
                Response.Headers.Add("reg_code", "DUPLICATE");
                return Ok(regUser);

        }

        // PUT /values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE /values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
