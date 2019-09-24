using Sapient.MedicineTracking.App.Models;
using System;
using Xunit;

namespace Sapient.MedicineTracking.Tests.Models
{
    public class MedicineShould
    {
        [Fact]
        public void Dto_TestData_ResultIsTrue()
        {
            // Assign
            var dto = new Medicine()
            {
                Name = "TestName",
                Brand = "TestBrand",
                Quantity = 1,
                Price = 1,
                ExpiryDate = DateTime.Today.Date,
                Notes = "TestNotes"
            };

            // Act & Assert
            Assert.True(dto.Name == "TestName");
            Assert.True(dto.Brand == "TestBrand");
            Assert.True(dto.Quantity == 1);
            Assert.True(dto.Price == 1);
            Assert.True(dto.Notes == "TestNotes");
        }
    }
}
