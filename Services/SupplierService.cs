using MonsterStop.Models;
using Microsoft.EntityFrameworkCore;

public class SupplierService : ISupplierService
{
    private readonly ApplicationDbContext _context;

    public SupplierService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Supplier>> GetAllSuppliersAsync()
    {
        return await _context.Suppliers.ToListAsync();
    }

    public async Task<Supplier?> GetSupplierByIdAsync(int id)
    {
        return await _context.Suppliers.FindAsync(id);
    }

    public async Task<Supplier> AddSupplierAsync(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }

    public async Task UpdateSupplierAsync(Supplier supplier)
    {
        // Fetch the existing supplier from the database
        var existingSupplier = await _context.Suppliers.FindAsync(supplier.Id);
        if (existingSupplier == null)
        {
            // Could throw an exception or return early if desired
            return;
        }

        // Update only the properties you want to allow changing
        existingSupplier.Name = supplier.Name;
        existingSupplier.Location = supplier.Location;

        // Save changes without calling Update() to avoid tracking conflicts
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSupplierAsync(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
