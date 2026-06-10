using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDotNet.Dto;
using WebApiDotNet.Models;

namespace WebApiDotNet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmplyeeController : ControllerBase
    {
        ITIContext context;
        public EmplyeeController(ITIContext _Context)
        {
           context  = _Context;
        }
        [HttpGet("{id}")]
        public IActionResult Get (int id)
        {
          Employee Emp=  context.Employees.FirstOrDefault(x => x.Id == id);
            GeneralResponse gen = new GeneralResponse();
            if (Emp != null)
            {
                gen.Issuccess = true;
                gen.data = Emp;
            }
            else
            {
                gen.Issuccess = false;
                gen.data = "invalied";
            }
            return Ok(gen);

        }
    }
}
