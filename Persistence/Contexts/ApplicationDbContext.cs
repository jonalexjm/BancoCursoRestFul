using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime) : base (options)
        {
            /*
             * El comportamiento de seguiento controla que cualquier cambio detectado
             * se mantendra sobre la base de datos durante en savechange
             */
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;

        }
        
        public DbSet<Cliente> clientes { get; set; }
        
    }
}