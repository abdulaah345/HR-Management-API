using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDotNet.Models;

namespace WebApiDotNet.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ITIContext context;

        public ProductController(ITIContext _context)
        {
            context = _context;

        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {

            List<Product> pro = context.Products.ToList();
            return Ok(pro);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetProductbyid(int id)
        {

            Product product = context.Products.FirstOrDefault(b => b.id == id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return CreatedAtAction("GetProductbyid", new { id = product.id }, product);
        }

        [HttpPut]
        public IActionResult EditProduct(int id, Product product)
        {
            Product pro = context.Products.FirstOrDefault(b=>b.id==id);

            if(pro!=null)
            {
                pro.Name= product.Name;
                pro.Description= product.Description;
                pro.price= product.price;
                context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
    }


