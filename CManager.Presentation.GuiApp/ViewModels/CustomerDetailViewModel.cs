using CManager.Application.Services;
using CManager.Core.Models;
using CManager.Presentation.GuiApp.Services;
using CommunityToolkit.Mvvm.ComponentModel; 
using CommunityToolkit.Mvvm.Input;

namespace CManager.Presentation.GuiApp.ViewModels
{
    public partial class CustomerDetailViewModel: BaseViewModel
    {
        //private readonly CustomerService _CustomerService;
        //private readonly NavigationService _navigationService;
        private Guid _customerId;

        [ObservableProperty]
        private string _firstName = string.Empty;

        [ObservableProperty]
        private string _lastName = string.Empty;

        [ObservableProperty]
        private string _email = string.Empty;

        [ObservableProperty]
        private string _phoneNumber = string.Empty;

        [ObservableProperty]
        private string _address = string.Empty;

        [ObservableProperty]    
        private string _city = string.Empty;

        [ObservableProperty]
        private string _street = string.Empty;

        [ObservableProperty]
        private string _postalCode = string.Empty;

        //initializes a new instance of CustomerDetailViewModel
        public CustomerDetailViewModel(CustomerService customerService, NavigationService navigationService)
        {
            _CustomerService = customerService;
            _navigationService = navigationService;
            Title = "Customer Details";
        }

        //loads customer Data for editing
        public void LoadCustomer(Guid customerId)
        {
            _customerId = customerId;

            if (customerId == Guid.Empty)
            {
                Title = "Create New Customer";
                ClearForm();
            }
            else
            {
                Title = "Edit Customer";
                LoadExistingCustomer(); 
            }
        }

        //loading existing customer data
        public void LoadExistingCustomer()
        {
            try
            {
                var customer = _CustomerService.GetCustomerById(_customerId);

                if (customer != null)
                {
                    FirstName = customer.FirstName;
                    LastName = customer.LastName;
                    Email = customer.Email;
                    PhoneNumber = customer.PhoneNumber;
                    Address = customer.Address;
                    City = customer.City;
                    Street = customer.Street;
                    PostalCode = customer.PostalCode;
                    ShowSuccess("Customer data loaded");
                }
                else
                {
                    ShowError("Customer not found");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Failed to load customer: {ex.Message}");
                ClearForm();
            }
        }

        //saves the customer
        [RelayCommand]
        private void Save()
        {
            //validate user input first
            if (!ValidateInput())
                return;

            try
            {
                if (_customerId == Guid.Empty)
                {
                    //create new customer and the customer object with form data
                    var newCustomer = new Customer
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        PhoneNumber = PhoneNumber,
                        Address = Address,
                        City = City,
                        Street = Street,
                        PostalCode = PostalCode,
                    };

                    // cal service method that accept customer object
                    var createdCustomer = _CustomerService.CreateCustomer(newCustomer);

                    //store the newly created id
                    _customerId = createdCustomer.Id;

                    ShowSuccess("Customer created sucessfully");
                    ClearForm();
                    _navigationService.NavigateToCustomerList();
                }
                else
                {
                    //updating the customer
                    var customerToUpdate = new Customer
                    {
                        Id = _customerId,
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        PhoneNumber = PhoneNumber,
                        Address = Address,
                        City = City,
                        Street = Street,
                        PostalCode = PostalCode,
                    };

                    //call the service for update
                    var success = _CustomerService.UpdateCustomer(customerToUpdate);

                    if (success)
                    {
                        ShowSuccess("Csutomer updated sucessfully");
                        _navigationService.NavigateToCustomerList();
                    }
                    else
                    {
                        ShowError("Failed to update customer");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Failed to save customer: {ex.Message}");
            }
        }

        //cancel the operation
        [RelayCommand]
        private void Cancel()
        {
            ClearForm();
            _navigationService.NavigateToCustomerList();
        }

        // clear the form 
        private void ClearForm()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            Street = string.Empty;
            PostalCode = string.Empty;
        }

        // validating the user input
        private bool ValidateInput()
        {
            if(string.IsNullOrWhiteSpace(FirstName))
            {
                ShowError("Firstname is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                ShowError("Lastname is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Email))
            {
                ShowError("Email is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                ShowError("Phone number is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Address))
            {
                ShowError("Address is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(City))
            {
                ShowError("City is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Street))
            {
                ShowError("Street is required");
                return false;
            }
            if (string.IsNullOrWhiteSpace(PostalCode))
            {
                ShowError("Postal code is required");
                return false;
            }
            return true;
        }
    }
}
