using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Data;
using OnlineMarket.Model;

namespace OnlineMarket.Controller;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private DataDbContext db;

    public OrderController(DataDbContext dataDbContext)
    {
        db = dataDbContext;
    }

    [HttpPost]
    public ActionResult<Order> Post(Order order)
    {
        db.Orders.Add(order);
        db.SaveChanges();
        return Ok(order);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Order>> Get(int? customerId, int? productId, int? amount)
    {
        var search = db.Orders.AsQueryable();

        if ((customerId != null) && (customerId > 0))
        {
            search = search.Where(s => s.CustomerId == customerId);
        }

        if ((productId != null) && (productId > 0))
        {
            search = search.Where(s => s.ProductId == productId);
        }

        if ((amount != null) && (amount > 0))
        {
            search = search.Where(s => s.Amount < amount);
        }

        var nat = search.ToList();
        return Ok(nat);
    }

    [HttpPut("{id}")]
    public ActionResult<Order> Put(int id, [FromBody] Order order)
    {
        if (order == null)
        {
            NotFound();
        }

        var old_order = db.Orders.Find(id);
        if (old_order == null)
        {
            return NotFound();
        }

        old_order.CustomerId = order.CustomerId;
        old_order.ProductId = order.ProductId;
        old_order.Amount = order.Amount;
        db.SaveChanges();
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public ActionResult<Order> Delete(int id)
    {
        var del_order = db.Orders.Find(id);
        db.Orders.Remove(del_order);
        db.SaveChanges();
        return Ok();
    }
}