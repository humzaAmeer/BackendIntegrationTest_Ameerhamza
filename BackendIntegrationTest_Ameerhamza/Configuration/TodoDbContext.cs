using BackendIntegrationTest_Ameerhamza.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackendIntegrationTest_Ameerhamza.Configuration
{
    public class TodoDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoDbContext"/> class.
        /// Object initializer
        /// </summary>
        /// <param name="options">options</param>
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets todoTable
        /// </summary>
        public DbSet<TodoModel> todoTable { get; set; }

        /// <summary>
        /// Gets or sets todoTable
        /// </summary>
        public DbSet<CategoryDetails> categoryDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {

            optionsBuilder.UseSqlServer("Server=.;Database=todo;Trusted_Connection=true;TrustServerCertificate=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
         

                        modelBuilder.Entity<TodoModel>(entity =>
                        {

                            entity.ToTable("todoTable");
                            entity.HasKey(p => p.Id).HasName("Id");

                            entity.Property(p => p.Id)

                            .HasColumnName("id")

                            .HasColumnType("int").ValueGeneratedNever();

                            entity.Property(p => p.Todo)

                            .HasColumnName("Todo");


                            entity.Property(p => p.Completed)

                            .HasColumnName("Completed");

                            entity.Property(p => p.UserId)

            .HasColumnName("UserId");

                                         
                            entity.Property(p => p.Priority)

                .HasColumnName("Priority");


                            entity.Property(p => p.Location)

                .HasColumnName("Location");


                            entity.Property(p => p.DueDate)

                .HasColumnName("DueDate");
                        });

                        modelBuilder.Entity<CategoryDetails>(entity =>
                        {

                            entity.ToTable("CategoryDetails");
                            entity.HasKey(p => p.Id).HasName("category_Id");

                            entity.Property(p => p.Id)

                            .HasColumnName("id")

                            .HasColumnType("int").ValueGeneratedNever();

                            entity.Property(p => p.Title)

                            .HasColumnName("Title");

                            entity.Property(p => p.ParentCategoryId)

            .HasColumnName("ParentCategoryId");




                        });
        }
    }
}
