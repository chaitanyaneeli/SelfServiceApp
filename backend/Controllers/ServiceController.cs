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
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {   private readonly ILogger<ServiceController> logger;

        private readonly ServiceOperation serviceOperation;  
  
        public ServiceController(ServiceOperation _serviceOperation, ILogger<ServiceController> _logger)  
        {  
            serviceOperation = _serviceOperation ;  
            logger = _logger;
        }  

        [HttpGet]
        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<string>> Get()
        {
            //Console.WriteLine("Entered GET");

            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == "EmpID"))
            {
                string empID = currentUser.FindFirst(c => c.Type == "EmpID").Value;

                return Ok(serviceOperation.Get(empID));
            }
            else
            {
                Response.Headers.Add("service_code", "UNAUTHORIZED");
                return Ok();
            }

        }


        // GET /values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST /values
        [HttpPost]
        [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post([FromBody]Service service)
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == "EmpID"))
            { 
                service.EmpID = currentUser.FindFirst(c => c.Type == "EmpID").Value;
                
                string code = serviceOperation.NewService(service);

                if(code.Equals("SUCCESS")){
                
                    Response.Headers.Add("service_code", "SUCCESS");
                    return Ok();
                }
                else {
                
                    Response.Headers.Add("service_code", "FAIL");
                    return Ok();
                }
            }
            else {

                Response.Headers.Add("service_code", "UNAUTHORIZED");
                return Ok();
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
