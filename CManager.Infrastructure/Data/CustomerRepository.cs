using CManager.Core.Interfaces;
using CManager.Core.Models;

namespace CManager.Infrastructure.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new();
        private readonly String _filePath;

        public CustomerRepository(String filePath)
        {
            _filePath = filePath;
            _customers = LoadCustomers();
        }
        public void Add(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            if (GetByEmail(customer.Email) != null)
            {
                throw new ArgumentException($"A customer with email {customer.Email} already exists.");
            }
            _customers.Add(customer);
        }

        public List<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetById(Guid id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public Customer? GetByEmail(string email)
        {
            return _customers.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
        public bool Update(Customer customer)
        {
            var existingCustomer = GetById(customer.Id);
            if (existingCustomer == null)
            {
                return false;
            }
            _customers.Remove(existingCustomer);
            _customers.Add(customer);
            return true;
        }
        public bool DeleteById(Guid id)
        {
            var customer = GetById(id);
            if (customer == null)
            {
                return false;
            }
            _customers.Remove(customer);
            return true;
        }

        public bool DeleteByEmail(string email)
        {
            var customer = GetByEmail(email);
            if (customer == null)
            {
                return false;
            }
            _customers.Remove(customer);
            return true;
        }

        public void SaveChanges()
        {
            JsonFileHelper.WriteToJsonFile(_filePath, _customers);
        }

        private List<Customer> LoadCustomers()
        {
            return JsonFileHelper.ReadFromJsonFile<Customer>(_filePath);
        }
    }
}
