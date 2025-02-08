using System;
using Microsoft.EntityFrameworkCore;
using PB503Project.Models;

namespace PB503Project.Data
{
	public class AppDbContext : DbContext
	{
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanItem> LoanItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=PB503Projects;User Id=SA;Password=reallyStrongPwd123;Encrypt=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)          
                .WithMany(a => a.Books)           
                .UsingEntity<Dictionary<string, object>>(
                    "AuthorBook",                 
                    j => j.HasOne<Author>().WithMany().HasForeignKey("AuthorId"),   
                    j => j.HasOne<Book>().WithMany().HasForeignKey("BookId")       
                );

            base.OnModelCreating(modelBuilder);
        }

    }
}

