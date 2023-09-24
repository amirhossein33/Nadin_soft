using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using myproject.Data;

namespace myproject.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> ReadProductsDetails()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
        public async Task<List<Product>> ReadProductDetailsById(int productId)
        {
            var product = await _context.Products.ToListAsync();
            return product;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> PartialUpdateProduct(int productId, JsonPatchDocument<Product> patchDocument)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return null;
            }

            patchDocument.ApplyTo(product);

            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }









    }




}
