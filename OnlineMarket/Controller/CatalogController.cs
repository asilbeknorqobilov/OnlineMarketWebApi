using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Data;
using OnlineMarket.Model;

namespace OnlineMarket.Controller;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private DataDbContext db;

    public CatalogController(DataDbContext dataDbContext)
    {
        db = dataDbContext;
    }

    [HttpPost]
    public ActionResult<Catalog> Post(Catalog catalog)
    {
        db.Catalogs.Add(catalog);
        db.SaveChanges();
        return Ok(catalog);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Catalog>> Get(string? name)
    {
        var search = db.Catalogs.AsQueryable();

        if (name != null)
        {
            search = search.Where(s => EF.Functions.ILike(s.Name, $"%{name}%"));
        }

        var nat = search.ToList();
        return Ok(nat);
    }

    [HttpPut("{id}")]
    public ActionResult<Catalog> Put(int id, [FromBody] Catalog catalog)
    {
        if (catalog == null)
        {
            NotFound();
        }

        var old_catalog = db.Catalogs.Find(id);

        if (old_catalog == null)
        {
            return NotFound();
        }

        old_catalog.Name = catalog.Name;
        db.SaveChanges();
        return Ok(catalog);
    }

    [HttpDelete("{id}")]
    public ActionResult<Catalog> Delete(int id)
    {
        var del_catalog = db.Catalogs.Find(id);
        db.Catalogs.Remove(del_catalog);
        db.SaveChanges();
        return Ok();
    }
}