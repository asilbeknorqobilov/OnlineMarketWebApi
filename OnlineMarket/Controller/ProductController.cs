using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Data;
using OnlineMarket.Model;

namespace OnlineMarket.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private DataDbContext db;

    public ProductController(DataDbContext dataDbContext)
    {
        db = dataDbContext;
    }

    [HttpPost]
    public ActionResult<Product> Post(Product product)
    {
        db.Products.Add(product);
        db.SaveChanges();
        return Ok(product);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get(string? name, int? price, int? amount)
    {
        var search = db.Products.AsQueryable();
        if (name != null)
        {
            search = search.Where(x => EF.Functions.ILike(x.Name, $"%{name}%"));
        }

        if ((price != null) && (price > 0))
        {
            search = search.Where(x => x.Price < price);
        }

        if ((amount != null) && (amount > 0))
        {
            search = search.Where(x => x.Amount == amount);
        }

        var nat = search.ToList();
        return Ok(nat);
    }


    [HttpPut("{id}")]
    public ActionResult<Product> Put(int id, [FromBody] Product product)
    {
        if (product == null)
        {
            NotFound();
        }

        var old_product = db.Products.Find(id);

        if (old_product == null)
        {
            return NotFound();
        }

        old_product.Name = product.Name;
        old_product.Price = product.Price;
        old_product.Amount = product.Amount;
        old_product.CatalogId = product.CatalogId;
        db.SaveChanges();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public ActionResult<Product> Delete(int id)
    {
        var del_product = db.Products.Find(id);
        db.Products.Remove(del_product);
        db.SaveChanges();
        return Ok();
    }
}