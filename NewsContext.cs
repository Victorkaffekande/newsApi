using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using newsApi.Enteties;

namespace newsApi;

public class NewsContext : IdentityDbContext<NewsUser>
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public NewsContext(DbContextOptions options) : base(options)
    {
    }

    protected NewsContext()
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SetupRelations(builder);

        //role seed
        var editorRole = SeedIdentityRole(builder, "Editor");
        var writerRole = SeedIdentityRole(builder, "Writer");
        var subscriberRole = SeedIdentityRole(builder, "Subscriber");
        //var guestRole = SeedIdentityRole(builder, "Guest"); 

        //user seed

        SeedUser(builder, editorRole, "editor@mail.com", "passwrd");

        var writer = SeedUser(builder, writerRole, "writer@mail.com", "passwrd");
        var writer2 = SeedUser(builder, writerRole, "otherWriter@mail.com", "passwrd");
        
        var subscriber = SeedUser(builder, subscriberRole, "subscriber@mail.com", "passwrd");
        var subscriber2 = SeedUser(builder, subscriberRole, "otherSubscriber@mail.com", "passwrd");
        


        //article Seed
        SeedArticle(builder, "Very cool article", "very cool content", 1, writer);
        SeedArticle(builder, "Article 2", "Lots of article 2 content here", 2, writer2);

        //comments seed
        SeedComment(builder,1,1,"I like this :)",subscriber.Id);
        SeedComment(builder,2,1,"This is fake news",subscriber2.Id);
    }

    private IdentityUser SeedUser(ModelBuilder builder, IdentityRole role, string email, string password)
    {
        var hasher = new PasswordHasher<NewsUser>();
        var user = new NewsUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            UserName = email,
            NormalizedUserName = email.ToUpper(),
        };
        user.PasswordHash = hasher.HashPassword(user, password);

        builder.Entity<NewsUser>().HasData(user);

        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = role.Id,
            UserId = user.Id
        });

        return user;
    }

    private IdentityRole SeedIdentityRole(ModelBuilder builder, string roleName)
    {
        var id = Guid.NewGuid().ToString();
        var role = new IdentityRole
        {
            Id = id,
            Name = roleName,
            NormalizedName = roleName.ToUpper(),
            ConcurrencyStamp = id
        };
        builder.Entity<IdentityRole>().HasData(role);
        return role;
    }

    private void SeedArticle(ModelBuilder builder, string title, string content, int id, IdentityUser author)
    {
        var article = new Article()
        {
            Id = id,
            AuthorId = author.Id,
            Content = content,
            Title = title,
            CreatedAt = DateTime.Now
        };
        builder.Entity<Article>().HasData(article);
    }
    private void SeedComment(ModelBuilder builder, int id, int articleId, string content, string authorId)
    {
        var comment = new Comment()
        {
            Id = id,
            ArticleId = articleId,
            Content = content,
            AuthorId = authorId
        };
        builder.Entity<Comment>().HasData(comment);
    }
    private void SetupRelations(ModelBuilder builder)
    {
        builder.Entity<Article>().HasKey(x => x.Id);
        builder.Entity<Article>().HasMany<Comment>(x => x.Comments).WithOne(x => x.Article);
        builder.Entity<Article>().HasOne<NewsUser>(x => x.Autor).WithMany(x => x.Articles)
            .HasForeignKey(x => x.AuthorId);


        builder.Entity<Comment>().HasKey(x => x.Id);
        builder.Entity<Comment>().HasOne<Article>(x => x.Article).WithMany(x => x.Comments)
            .HasForeignKey(x => x.ArticleId);
        builder.Entity<Comment>().HasOne<NewsUser>(x => x.Author).WithMany(x => x.Comments)
            .HasForeignKey(x => x.AuthorId);
    }

  
}