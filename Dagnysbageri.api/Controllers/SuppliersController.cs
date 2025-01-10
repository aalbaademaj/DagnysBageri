using Dagnysbageri.api.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dagnysbageri.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly DataContext _context;
    public SuppliersController(DataContext context)
    {
        _context = context;

    }

    [HttpGet("{name}")]
    public async Task<ActionResult> ListSuppliers(string name)
    {
        var suppliers = await _context.Suppliers
        .Include(sp => sp.SupplierProducts)
        .ThenInclude(p => p.Product)
        .Where(s => s.Name.ToUpper() == name.ToUpper())
        .Select(s => new
        {
            s.SupplierId,
            Supplier = s.Name,
            s.Address,
            s.ContactPerson,
            s.Phone,
            s.Email,
            Products = s.SupplierProducts
            .Select(sp => new
            {
                sp.Product.ProductName,
                sp.Product.Description,
                sp.Price
            })
        }).ToListAsync();

        return Ok(new { success = true, StatusCode = 200, data = suppliers });
    }


}
