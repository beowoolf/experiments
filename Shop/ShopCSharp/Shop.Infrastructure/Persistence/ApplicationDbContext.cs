using Microsoft.EntityFrameworkCore;
using Shop.Application.Common.Interfaces;
using Shop.Domain.Common;
using Shop.Domain.Entities;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : 
            base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (item.State)
                {                    
                    case EntityState.Modified:
                        item.Entity.LastModifiedBy = _currentUserService.UserId;
                        item.Entity.LastModified = _dateTime.Now;
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedBy = _currentUserService.UserId;
                        item.Entity.Created = _dateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
