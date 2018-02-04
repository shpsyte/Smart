using Core.Domain.Finance;
using Core.Domain.Finance.Views;
using Core.Domain.Identity;
using Core.Domain.PersonAndData;
using Data.Map;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Data.Context
{
    public class SmartContext : IdentityDbContext<ApplicationUser>
    {

       
        //public DbSet<VRevenue> VRevenue { get; set; }

           


     
        public SmartContext(DbContextOptions<SmartContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
             
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>().ToTable("User", "Security");
            //builder.Entity<ApplicationUser>().HasKey(a => a.Id);
            //builder.Entity<IdentityRole>().ToTable("Role", "Security");
            //builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "Security");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "Security");
            //builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Security");
            //builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Security");
            //builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Security");
            //builder.Entity<IdentityUserRole<string>>().HasKey(a => new { a.UserId, a.RoleId });
            //builder.Entity<IdentityUserClaim<string>>().HasKey(a => new { a.UserId, a.Id });
            //builder.Entity<IdentityUserLogin<string>>().HasKey(a => new { a.UserId, a.ProviderKey });
            //builder.Entity<IdentityRoleClaim<string>>().HasKey(a => new { a.RoleId, a.Id });
            //builder.Entity<IdentityUserToken<string>>().HasKey(a => new { a.UserId });

            


            #region AutoMap
            // Interface that all of our Entity maps implement
            var mappingInterface = typeof(IMapConfiguration<>);
            // Types that do entity mapping
            var mappingTypes = typeof(SmartContext).GetTypeInfo().Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
            // Get the generic Entity method of the ModelBuilder type
            var entityMethod = typeof(ModelBuilder).GetMethods()
                .Single(x => x.Name == "Entity" &&
                        x.IsGenericMethod &&
                        x.ReturnType.Name == "EntityTypeBuilder`1");
            
            foreach (var mappingType in mappingTypes)
            {
                // Get the type of entity to be mapped
                var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();
                // Get the method builder.Entity<TEntity>
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);
                // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
                var entityBuilder = genericEntityMethod.Invoke(builder, null);
                // Create the mapping type and do the mapping
                var mapper = Activator.CreateInstance(mappingType);
                mapper.GetType().GetMethod("Map").Invoke(mapper, new[] { entityBuilder });
            }
            #endregion


        }
    }
}
