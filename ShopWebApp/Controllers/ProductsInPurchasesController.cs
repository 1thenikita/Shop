using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ShopWebApp.Models;

namespace ShopWebApp.Controllers
{
    public class ProductsInPurchasesController : ApiController
    {
        private DBEntities db = new DBEntities();

        // GET: api/ProductsInPurchases
        public IQueryable<ProductsInPurchase> GetProductsInPurchases()
        {
            return db.ProductsInPurchases;
        }

        // GET: api/ProductsInPurchases/5
        [ResponseType(typeof(ProductsInPurchase))]
        public async Task<IHttpActionResult> GetProductsInPurchase(int id)
        {
            ProductsInPurchase productsInPurchase = await db.ProductsInPurchases.FindAsync(id);
            if (productsInPurchase == null)
            {
                return NotFound();
            }

            return Ok(productsInPurchase);
        }

        // PUT: api/ProductsInPurchases/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductsInPurchase(int id, ProductsInPurchase productsInPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productsInPurchase.ID)
            {
                return BadRequest();
            }

            db.Entry(productsInPurchase).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsInPurchaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductsInPurchases
        [ResponseType(typeof(ProductsInPurchase))]
        public async Task<IHttpActionResult> PostProductsInPurchase(ProductsInPurchase productsInPurchase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductsInPurchases.Add(productsInPurchase);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productsInPurchase.ID }, productsInPurchase);
        }

        // DELETE: api/ProductsInPurchases/5
        [ResponseType(typeof(ProductsInPurchase))]
        public async Task<IHttpActionResult> DeleteProductsInPurchase(int id)
        {
            ProductsInPurchase productsInPurchase = await db.ProductsInPurchases.FindAsync(id);
            if (productsInPurchase == null)
            {
                return NotFound();
            }

            db.ProductsInPurchases.Remove(productsInPurchase);
            await db.SaveChangesAsync();

            return Ok(productsInPurchase);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductsInPurchaseExists(int id)
        {
            return db.ProductsInPurchases.Count(e => e.ID == id) > 0;
        }
    }
}