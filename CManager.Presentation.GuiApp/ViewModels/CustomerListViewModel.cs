using CManager.Application.Services;
using CManager.Core.Models;
using CManager.Presentation.GuiApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net;

namespace CManager.Presentation.GuiApp.ViewModels
{
    public partial class CustomerListViewModel: BaseViewModel
    {
        private readonly CustomerService _customerService;
        private readonly NavigationService _navigationService;

        [ObservableProperty]
        private CustomerViewModel? _selectedCustomer;

        //get the collection of the customers
        public ObservableCollection<CustomerViewModel> Customers { get; } = new();

        // gets and sets a value to indicate viewmodel is busy or not
        [ObservableProperty]
        private bool _isBusy;

        //initializes a new instance of customerlistviewmodel
        public CustomerListViewModel(CustomerService customerService, NavigationService navigationService)
        {
            _customerService = customerService;
            _navigationService = navigationService;
            Title = "Customer List";

            //load the customer on initialization
            LoadCustomer();
        }

        //loads all the custmers from the service
        [RelayCommand]
        private void LoadCustomer()
        {
            IsBusy = true;
            StatusMessage = "Loading Customers...";

            try
            {
                Customers.Clear();
                var customers = _customerService.GetAllCustomer();

                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerViewModel(customer));
                }

                ShowSuccess($"Loaded {Customers.Count} customers");
            }
            catch (Exception ex)
            {
                ShowError($"Failed to load customer: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        //delete the selected customer
        [RelayCommand(CanExecute = nameof(CanDeleteCustomer))]
        private void DeleteCustomer()
        {
            if (SelectedCustomer == null)
                return;
            IsBusy = true;
            StatusMessage = "Deleting Customer...";
            try
            {
                var success = _customerService.DeleteCustomerByEmail(SelectedCustomer.Email);
                if (success)
                {
                    Customers.Remove(SelectedCustomer);
                    SelectedCustomer = null;
                    ShowSuccess("Customer deleted sucessfully");
                }
                else
                {
                    ShowError("Failed to delete customer");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Failed to delete customer: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //determines if a customer can be deleted or not
        private bool CanDeleteCustomer()
        {
            return SelectedCustomer != null;
        }

        //views details of thew selected customer
        [RelayCommand(CanExecute = nameof(CanViewDetails))]
        private void ViewDetails()
        {
            if(SelectedCustomer != null)
            {
                _navigationService.NavigateToEditCustomer(SelectedCustomer.Id);
            }
        }

        //Determine if details can be viewed 
        private bool CanViewDetails()
        {
            return SelectedCustomer != null;
        }

        //called when selectedcustomer changes
        partial void OnSelectedCustomerChange(CustomerViewModels? value)
        {
            DeleteCustomerCommand.NotifyCanExecuteChanged();
            ViewDetailsCommand.NotifyCanExecuteChanged();
        }
    }

    // view model wrapper for customer model
    public partial class CustomerViewModel: ObservableObject
    {
        [ObservableProperty]
        private Guid _id;
        [ObservableProperty]
        private string _firstname = string.Empty;
        [ObservableProperty]
        private string _lastname = string.Empty;
        [ObservableProperty]
        private string _phonenumber = string.Empty;
        [ObservableProperty]
        private string _email = string.Empty;
        [ObservableProperty]
        private string _address = string.Empty;
        [ObservableProperty]
        private string _city = string.Empty;
        [ObservableProperty]
        private string _street = string.Empty;
        [ObservableProperty]
        private string _postalcode = string.Empty;
        [ObservableProperty]
        private DateTime _createdAt;

        //initializes a new instance of the customerviewmodel
        public CustomerViewModel(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            Id = customer.Id;
            Firstname = customer.FirstName;
            Lastname = customer.LastName;
            Email = customer.Email;
            Phonenumber = customer.PhoneNumber;
            Address = customer.Address;
            City = customer.City;
            Street = customer.Street;
            Postalcode = customer.PostalCode;
            CreatedAt = customer.CreatedAt;
        }

        //Default constructor for xaml
        public CustomerViewModel() 
        { 
            //Default constructor for xaml binding
        }
    }
}
