using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMarket.Data;
using OnlineMarket.Model;

namespace OnlineMarket.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private DataDbContext db;

    public CustomerController(DataDbContext dataDbContext)
    {
        db = dataDbContext;
    }

    [HttpPost]
    public ActionResult<Customer> Post(Customer customer)
    {
        db.Customers.Add(customer);
        db.SaveChanges();
        return Ok(customer);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Customer>> Get(string? name, string? surname, int? phone, string? email)
    {
        var search = db.Customers.AsQueryable();

        if (name != null)
        {
            search = search.Where(s => EF.Functions.ILike(s.Name, $"%{name}%"));
        }

        if (surname != null)
        {
            search = search.Where(s => EF.Functions.ILike(s.Surname, $"%{surname}%"));
        }

        if ((phone != null) && (phone > 0))
        {
            search = search.Where(s => s.Phone == phone);
        }

        if (email != null)
        {
            search = search.Where(s => EF.Functions.ILike(s.Email, $"%{email}%"));
        }

        var nat = search.ToList();
        return Ok(nat);
    }

    [HttpPut("{id}")]
    public ActionResult<Customer> Put(int id, [FromBody] Customer customer)
    {
        if (customer == null)
        {
            NotFound();
        }

        var old_customer = db.Customers.Find(id);

        if (old_customer == null)
        {
            return NotFound();
        }

        old_customer.Name = customer.Name;
        db.SaveChanges();
        return Ok(customer);
    }

    [HttpDelete("{id}")]
    public ActionResult<Customer> Delete(int id)
    {
        var del_customer = db.Customers.Find(id);
        db.Customers.Remove(del_customer);
        db.SaveChanges();
        return Ok();
    }
}