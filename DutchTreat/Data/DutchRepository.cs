using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext ctx;

        public DutchRepository(DutchContext ctx)
        {
            this.ctx = ctx;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return ctx.Products
                        .OrderBy(p => p.Title)
                        .ToList();
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            return ctx.Products
                        .Where(p => p.Category == category)
                        .ToList();
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }
    }
}