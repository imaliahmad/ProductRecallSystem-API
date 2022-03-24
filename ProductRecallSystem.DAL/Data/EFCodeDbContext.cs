using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductRecallSystem.BOL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRecallSystem.DAL.Data
{
    public class EFCodeDbContext:IdentityDbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-T90JGBG\SQLEXPRESS; Database=ProductRecallSystem-API; Trusted_Connection=True;");
        }

        public DbSet<Manufacturers> Manufacturers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Recalls> Recalls { get; set; }
        public DbSet<Announcements> Announcements { get; set; }
        public DbSet<AppUsers> AppUsers { get; set; }
    }
}
