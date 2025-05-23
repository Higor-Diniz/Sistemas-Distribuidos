using BlogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Contrução da entidade User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).IsRequired();
            entity.Property(e => e.CreatedAt);

            // Garante que o 'Email' e o 'Username' sejam únicos
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
        });

        // Contrução da entidade Post
        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.CreatedAt);
            entity.Property(e => e.UpdatedAt);

            // Relacionamento da entidade 'Post' como a entidade 'User'
            entity.HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Contrução da entidade Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);

            // Garante que hajam nomes de categorias únicos
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Cadastrando as categorias iniciais
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Culinária", Description = "Descubra receitas, dicas de preparo e curiosidades gastronômicas do mundo todo." },
            new Category { Id = 2, Name = "Tecnologia", Description = "Fique por dentro das inovações, tendências e novidades do mundo digital." },
            new Category { Id = 3, Name = "Ciência", Description = "Explore o universo do conhecimento científico com conteúdos acessíveis e informativos." },
            new Category { Id = 4, Name = "Universo", Description = "Desvende os mistérios do cosmos! Um espaço dedicado à astronomia e às maravilhas do espaço sideral." },
            new Category { Id = 5, Name = "Design", Description = "Ideal para quem busca inspiração, aprimoramento técnico ou simplesmente aprecia boas composições visuais." }
        );
    }
}