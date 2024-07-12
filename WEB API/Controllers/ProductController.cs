using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WEB_API.Models;

namespace WEB_API.Controllers
{
        // GET: Product
        public class ProductController : ApiController
        {
            private ProductContext db = new ProductContext();

            // GET: api/Product
            public IQueryable<Product> GetProducts()
            {
                return db.Products;
            }

            // GET: api/Product/5
            public IHttpActionResult GetProduct(int id)
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }

            // PUT: api/Product/5
            public IHttpActionResult PutProduct(int id, Product product)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != product.ID)
                {
                    return BadRequest();
                }

                db.Entry(product).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
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

        // POST: api/Product
        public IHttpActionResult PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
        }

        // DELETE: api/Product/5
        public IHttpActionResult DeleteProduct(int id)
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return NotFound();
                }

                db.Products.Remove(product);
                db.SaveChanges();

                return Ok(product);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }

            private bool ProductExists(int id)
            {
                return db.Products.Count(e => e.ID == id) > 0;
            }
        }
    }