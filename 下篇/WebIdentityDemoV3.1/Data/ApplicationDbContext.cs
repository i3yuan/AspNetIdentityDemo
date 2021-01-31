using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebIdentityDemoV3._1.Models;

namespace WebIdentityDemoV3._1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //var maxKeyLength = 256;
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //自定义修改表名，以tbl开头
            builder.Entity<ApplicationUser>(b =>
            {
                b.Property(e => e.PasswordHash).HasColumnName("Password");
                b.Property(u => u.UserName).HasMaxLength(128);
                b.Property(u => u.NormalizedUserName).HasMaxLength(128);
                b.Property(u => u.Email).HasMaxLength(128);
                b.Property(u => u.NormalizedEmail).HasMaxLength(128);
                b.ToTable("TblUsers");
            });

            builder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                //定义主键
                b.HasKey(u => u.Id);
                b.ToTable("TblUserClaims");
            });

            builder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.HasKey(u => new { u.LoginProvider, u.ProviderKey });
                b.ToTable("TblUserLogins");
            });

            builder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.HasKey(u => new { u.UserId, u.LoginProvider, u.Name });
                b.ToTable("TblUserTokens");
            });

            builder.Entity<IdentityRole<Guid>>(b =>
            {
                b.HasKey(u => u.Id);
                b.ToTable("TblRoles");
            });

            builder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.HasKey(u => u.Id);
                b.ToTable("TblRoleClaims");
            });

            builder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.HasKey(u => new { u.UserId, u.RoleId });
                b.ToTable("TblUserRoles");
            });





        }
    }
}
