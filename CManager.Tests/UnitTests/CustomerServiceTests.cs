using CManager.Application.Services;
using CManager.Core.Interfaces;
using CManager.Core.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CManager.Tests.UnitTests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepository;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_mockRepository.Object);
        }

        [Fact]
        public void CreateCustomer_WithValidData_ShouldCreateCustomer()
        {
            // Arrange
            var firstName = "Alice";
            var lastName = "Peter";
            var email = "alicepeter@gmail.com";
            var phoneNumber = "1234567890";
            var address = "123 Main St";
            var city = "Metropolis";
            var street = "Main";
            var postalCode = "12345";

            _mockRepository
                .Setup(r => r.GetByEmail(email))
                .Returns((Customer?)null);

            _mockRepository.Setup(r => r.Add(It.IsAny<Customer>()));
            _mockRepository.Setup(r => r.SaveChanges());

            var newCustomer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                City = city,
                Street = street,
                PostalCode = postalCode
            };

            // Act
            var result = _customerService.CreateCustomer(newCustomer);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(firstName, result.FirstName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(email.ToLower(), result.Email);
            Assert.Equal(phoneNumber, result.PhoneNumber);
            Assert.Equal(address, result.Address);
            Assert.Equal(city, result.City);
            Assert.Equal(street, result.Street);
            Assert.Equal(postalCode, result.PostalCode);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.True(result.CreatedAt <= DateTime.UtcNow);

            _mockRepository.Verify(r => r.Add(It.IsAny<Customer>()), Times.Once);
            _mockRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void CreateCustomer_WithExistingEmail_ShouldThrowException()
        {
            // Arrange
            var existingEmail = new Customer
            {
                Id = Guid.NewGuid(),
                Email = "existing@gmail.com"
            };

            _mockRepository
                .Setup(r => r.GetByEmail("existing@gmail.com"))
                .Returns(existingEmail);

            var newCustomer = new Customer
            {
                FirstName = "alice",
                LastName = "peter",
                Email = "existing@gmail.com",
                PhoneNumber = "1234567890",
                Address = "123 Main",
                City = "Metropolis",
                Street = "Main",
                PostalCode = "12345"
            };

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() =>
             _customerService.CreateCustomer(newCustomer));

            //Assert
            Assert.Equal("A customer with the provided email already exists.", exception.Message);
        }

    }
}
