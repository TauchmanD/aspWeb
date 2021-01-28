using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication2.Models;
using WebApplication2.ViewModels;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Posts> Post { get; set; }
        public DbSet<Tournaments> Tournament { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public object Posts { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasMany(q => q.Comments).WithOne(a => a.Post).HasForeignKey(a => a.PostId).OnDelete(DeleteBehavior.Restrict);
            }
             );
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "ADMIN", Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "ADMINUSER",
                Email = "d@t",
                NormalizedEmail = "D@T",
                EmailConfirmed = true,
                LockoutEnabled = false,
                UserName = "d@t",
                NormalizedUserName = "D@T",
                PasswordHash = hasher.HashPassword(null, "Admin_1234"),
                SecurityStamp = string.Empty
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "ADMIN", UserId = "ADMINUSER" });
            
        }
    }
}

