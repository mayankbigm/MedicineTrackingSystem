using Microsoft.EntityFrameworkCore;

namespace Sapient.MedicineTracking.App.Models
{
    public class MedicineContext : DbContext
    {
        public MedicineContext(DbContextOptions<MedicineContext> options) : base(options)
        {
        }

        public DbSet<Medicine> Medicines { get; set; }
    }
}
