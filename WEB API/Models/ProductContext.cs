using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WEB_API.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("adoConnectionString")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}