using CManager.Core.Models;

namespace CManager.Core.Interfaces
{
    /// <summary>
    /// Service interface for customer business operations
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="fullName">Customer's full name</param>
        /// <param name="email">Customer's email (must be unique)</param>
        /// <param name="phoneNumber">Customer's phone number</param>
        /// <param name="Address">Customer's Street</param>
        /// <param name="City">Customer's City</param>
        /// <param name="Street">Customer's Street</param>
        /// <returns>The created customer</returns>
        Customer CreateCustomer(Customer customer);

        /// <summary>
        /// Retrieves all customers
        /// </summary>
        /// <returns>List of all customers</returns>
        List<Customer> GetAllCustomers();

        /// <summary>
        /// Retrieves a specific customer by email
        /// </summary>
        /// <param name="email">Customer's email address</param>
        /// <returns>The customer if found, null otherwise</returns>
        Customer? GetCustomerByEmail(string email);

        /// <summary>
        /// Retrieves a specific customer by ID
        /// </summary>
        /// <param name="id">Customer's unique identifier</param>
        /// <returns>The customer if found, null otherwise</returns>
        Customer? GetCustomerById(Guid id);

        /// <summary>
        /// Deletes a customer by email
        /// </summary>
        /// <param name="email">Customer's email address</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        bool DeleteCustomerByEmail(string email);

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="customer">Customer with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        bool UpdateCustomer(Customer customer);
    }
}