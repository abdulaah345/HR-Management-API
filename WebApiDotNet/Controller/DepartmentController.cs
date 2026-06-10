using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDotNet.Dto;
using WebApiDotNet.Models;

namespace WebApiDotNet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        ITIContext context;
        public DepartmentController( ITIContext _Context)
        {
            context = _Context; 
        }
        [HttpGet("p")]
        [Authorize]
        public IActionResult GetDeptDetails()
        {
        
             var deptlistDto = context.Departments.Select(d=>new DeptWithEmpCountDto
            {
                ID=d.Id,
                Name=d.Name,
                EmpCount = d.Emps.Count()

            }).ToList();

            return Ok(deptlistDto);
            //List<DeptWithEmpCountDto> deptlistDto = new List<DeptWithEmpCountDto>();
            //foreach (Department item in departmentList)
            //{
            //    DeptWithEmpCountDto deptdto = new DeptWithEmpCountDto();
            //    deptdto.ID = item.Id;
            //    deptdto.Name = item.Name;
            //    deptdto.EmpCount = item.Emps.Count();

            //    deptlistDto.Add(deptdto);
            //}

            //return deptlistDto;
        }
        [HttpGet]
            public IActionResult DisplayAllDep()
        {
            List<Department> departmentList = context.Departments.ToList();
       
            return Ok(departmentList);
        }
        [HttpGet("{Name:alpha}")]
        public IActionResult GetBYName(string Name)
        {
            Department depart=context.Departments.FirstOrDefault(x => x.Name == Name);  
            return Ok(depart);
        }
        
        
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            Department depart=context.Departments.FirstOrDefault(x => x.Id == id);
            return Ok(depart);
        }
        [HttpPost]
        public IActionResult AddDept(Department Dept)
        {
            context.Departments.Add(Dept);
            context.SaveChanges();  
            return CreatedAtAction("GetById", new {id=Dept.Id},Dept);
        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateDept(int id,Department Dept)
        {
            Department depnew = context.Departments.FirstOrDefault(x => x.Id == id);
            if (depnew != null)
            {

            depnew.Name = Dept.Name;
            depnew.ManagarName = Dept.ManagarName;
            context.SaveChanges();
            return NoContent();
            }
            else
            {
                return NotFound("Department not found");
            }
        }
    }
}
