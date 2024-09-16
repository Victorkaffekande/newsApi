using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using newsApi.Enteties;

namespace newsApi;

public class NewsContext : IdentityDbContext
{
    public NewsContext(DbContextOptions options) : base(options)
    {
    }

    protected NewsContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var editorRole = SeedIdentityRole(builder, "Editor", "22050a18-a733-49cc-9511-bd69ee4283fc");
     
        var writerRole = SeedIdentityRole(builder, "Writer", "5f8692fa-f2fc-4cd8-8b5b-55c14328c654");
        var subscriberRole = SeedIdentityRole(builder, "Subscriber", "23093a60-69ae-497a-80c7-5f8dfaef9df5");
        var guestRole = SeedIdentityRole(builder, "Guest", "c7a4d8f0-fe10-4b87-a0d6-756ed808f024");

        SeedUser(builder,editorRole,"editman@mail.com", "coolPassword123AQQA");

       
        
    }

    private void SeedUser(ModelBuilder builder, IdentityRole role, string email, string password)
    {
        var hasher = new PasswordHasher<IdentityUser>();
        var user = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            Email = email,
            UserName = email,
            NormalizedUserName = email.ToUpper()
        };
        user.PasswordHash = hasher.HashPassword(user, password);

        builder.Entity<IdentityUser>().HasData(user);
        
        builder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
    }

    private IdentityRole SeedIdentityRole(ModelBuilder builder, string roleName, string id)
    {
        var role = new IdentityRole
        {
            Id = id.ToString(),
            Name = roleName,
            NormalizedName = roleName.ToUpper(),
            ConcurrencyStamp = id
        };
        builder.Entity<IdentityRole>().HasData(role);
        return role;
    }

    private void SeedData(ModelBuilder builder)
    {
    }
}