using Microsoft.EntityFrameworkCore;
using Trainee_Details.Models;

namespace Trainee_Details.HostedServices
{
    public class ApplyMigrationService
    {
        private readonly TraineeDbContext db;
        public ApplyMigrationService(TraineeDbContext db)
        {
            this.db = db;
        }
        public async Task ApplyAsync()
        {
            var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}
