using CManager.Core.Models;

namespace CManager.Core.Interfaces
{
    /// <summary>
    /// Repository interface for customer data operations
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Adds a new customer to the repository
        /// </summary>
        /// <param name="customer">Customer to add</param>
        void Add(Customer customer);

        /// <summary>
        /// Retrieves all customers from the repository
        /// </summary>
        /// <returns>List of all customers</returns>
        List<Customer> GetAll();

        /// <summary>
        /// Retrieves a customer by their unique identifier
        /// </summary>
        /// <param name="id">Customer's GUID</param>
        /// <returns>The customer if found, null otherwise</returns>
        Customer? GetById(Guid id);

        /// <summary>
        /// Retrieves a customer by their email address
        /// </summary>
        /// <param name="email">Customer's email</param>
        /// <returns>The customer if found, null otherwise</returns>
        Customer? GetByEmail(string email);

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="customer">Customer with updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        bool Update(Customer customer);

        /// <summary>
        /// Deletes a customer by their unique identifier
        /// </summary>
        /// <param name="id">Customer's GUID</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        bool DeleteById(Guid id);

        /// <summary>
        /// Deletes a customer by their email address
        /// </summary>
        /// <param name="email">Customer's email</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        bool DeleteByEmail(string email);

        /// <summary>
        /// Saves all changes to persistent storage
        /// </summary>
        void SaveChanges();
    }
}