using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyStore.Core.Domain;
using MyStore.Core.Repositories;

namespace MyStore.Infrastructure.EF.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly MyStoreContext _context;

        public EfProductRepository(MyStoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _context.Products.ToListAsync();

        public async Task<IEnumerable<Product>> BrowseAsync(string name = "")
        {
            var products = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            return await products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            if (_context.Database.IsInMemory())
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                
                return;
            }
            
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            _context.Products.Remove(await GetAsync(id));
            await _context.SaveChangesAsync();
        }
    }
}