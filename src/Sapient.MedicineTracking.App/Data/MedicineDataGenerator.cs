using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sapient.MedicineTracking.App.Models;

namespace Sapient.MedicineTracking.App.Data
{
    [ExcludeFromCodeCoverage]
    public class MedicineDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var contextOptionsFromDi = serviceProvider.GetRequiredService<DbContextOptions<MedicineContext>>();
            using (var context = new MedicineContext(contextOptionsFromDi))
            {
                // check if data already present
                if (context.Medicines.Any())
                {
                    return;
                }

                // Seed Data if absent
                context.Medicines.AddRange(
                    new Medicine()
                    {
                        Id = 91,
                        Name = "Med1",
                        Brand = "brand1",
                        Price = (decimal) 29.12,
                        Quantity = 10,
                        ExpiryDate = new DateTime(2020,01,01).Date,
                        Notes = "medicine number 1"
                    },
                    new Medicine()
                    {
                        Id = 92,
                        Name = "Med2",
                        Brand = "brand2",
                        Price = (decimal) 29.13,
                        Quantity = 20,
                        ExpiryDate = new DateTime(2020, 01, 02).Date,
                        Notes = "medicine number 2"
                    });

                context.SaveChangesAsync();
            }
        }
    }
}
