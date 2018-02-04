using Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Map
{

    public class IdentityMap : IMapConfiguration<ApplicationUser>
    {
        public void Map(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("User", "Security");
            builder.HasKey(p => p.Id);
            
        }

    }
    public class IdentityRoleMap : IMapConfiguration<IdentityRole>
    {
        public void Map(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("Role", "Security");
            builder.HasKey(p => p.Id);
        }
    }
    public class IdentityUserClaimMap : IMapConfiguration<IdentityUserClaim<string>>
    {
        public void Map(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.ToTable("UserClaim", "Security");
            builder.HasKey(a => a.Id);
        }
    }
    public class IdentityUserRoleMap : IMapConfiguration<IdentityUserRole<string>>
    {
        public void Map(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.ToTable("UserRole", "Security");
            builder.HasKey(a => new { a.UserId, a.RoleId });
        }
    }
    public class IdentityUserLoginMap : IMapConfiguration<IdentityUserLogin<string>>
    {
        public void Map(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.ToTable("UserLogin", "Security");
            builder.HasKey(a => new { a.LoginProvider, a.ProviderKey });
        }
    }
    public class IdentityRoleClaimMap : IMapConfiguration<IdentityRoleClaim<string>>
    {
        public void Map(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.ToTable("RoleClaim", "Security");
            builder.HasKey(a => a.Id);
        }
    }
    public class IdentityUserTokenMap : IMapConfiguration<IdentityUserToken<string>>
    {
        public void Map(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.ToTable("UserToken", "Security");
            builder.HasKey(a => new {  a.UserId, a.LoginProvider, a.Name });
        }
    }
}
