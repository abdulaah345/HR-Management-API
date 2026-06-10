using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDotNet.Dto;
using WebApiDotNet.Models;

namespace WebApiDotNet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ITIContext context;
        public CategoryController(ITIContext _context)
        {
            context = _context;

        }
        [HttpGet]
        public IActionResult GetCategory()
        {
            var cat = context.Categories.Select(x => new CategorywithProduct
            {
                Id = x.Id,
                Name = x.name,
                productcount = x.products.Count(),
                products = x.products.Select(p => p.Name).ToList(),

            }).ToList();
            return Ok(cat);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetCategory(int id)
        {
            var cat=context.Categories.Where(x=>x.Id==id).Select(x=>new CategorywithProduct
            {
                Id = x.Id,
                Name=x.name,
                products=x.products.Select(p=>p.Name).ToList(),
            })
                .FirstOrDefault();
            if (cat != null)
            {
                return Ok(cat);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {

            context.Categories.Add(category);
            context.SaveChanges();
            return CreatedAtAction("GetCategory", new {id=category.Id},category);

        }
    }
}
