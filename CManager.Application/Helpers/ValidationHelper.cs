using CManager.Core.Models;

namespace CManager.Application.Helpers
{
    public static class  CustomerValidator
    {
        public static void ValidateRequiredFields(Customer customer)
        {
            if(customer == null) 
                throw new ArgumentNullException(nameof(customer));

            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new ArgumentException("First name is required.", nameof(customer.FirstName));

            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new ArgumentException("Last name is required.", nameof(customer.LastName));

            if (string.IsNullOrWhiteSpace(customer.Email))  
                throw new ArgumentException("Email is required.", nameof(customer.Email));

            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(customer.PhoneNumber));

            if  (string.IsNullOrWhiteSpace(customer.Address))
                throw new ArgumentException("Address is required.", nameof(customer.Address));

            if (string.IsNullOrWhiteSpace(customer.City))
                throw new ArgumentException("City is required.", nameof(customer.City));

            if (string.IsNullOrWhiteSpace(customer.Street))
                 throw new ArgumentException("Street is required.", nameof(customer.Street));

            if (string.IsNullOrWhiteSpace(customer.PostalCode))
                throw new ArgumentException("Postal code is required.", nameof(customer.PostalCode));

        }

        public static void ValidateEmail(Customer customer)
        {
            var Customermail = customer.Email;
            if (!customer.IsValidEmail(Customermail))
               throw new ArgumentException("Invalid email format.", nameof(customer.Email));
        }
    }
}
