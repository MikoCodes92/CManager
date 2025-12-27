namespace CManager.Core.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; } = string.Empty;
        public String LastName { get; set; } = string.Empty;
        public String Email { get; set; } = string.Empty;
        public String PhoneNumber { get; set; } = string.Empty;    
        public String Address { get; set; } = string.Empty;
        public String City { get; set; }= string.Empty;
        public String Street { get; set; }= string.Empty;
        public String PostalCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   IsValidEmail(Email) &&
                   !string.IsNullOrWhiteSpace(PhoneNumber) &&
                   !string.IsNullOrWhiteSpace(Address) &&
                   !string.IsNullOrWhiteSpace(City) &&
                   !string.IsNullOrWhiteSpace(Street) &&
                   !string.IsNullOrWhiteSpace(PostalCode);
        }   
    }
}
