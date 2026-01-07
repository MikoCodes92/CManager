using System;
using System.IO;
using System.Windows;
using CManager.Application.Services;
using CManager.Infrastructure.Data;
using CManager.Presentation.GuiApp.Services;
using CManager.Presentation.GuiApp.ViewModels;

namespace CManager.Presentation.GuiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavigationService _navigationService;
        public MainWindow()
        {
            InitializeComponent();
            InitializeNavigation(); 
        }

        private void InitializeNavigation()
        {
            //set up data file path
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appDirectory = Path.Combine(appDataPath, "CManager");   
            var filePath = Path.Combine(appDirectory, "customers.json");

            Directory.CreateDirectory(appDirectory);

            //Create respository and service
            var repository = new CustomerRepository(filePath);
            var customerService = new CustomerService(repository);

            //first let's Create navigation service 
            _navigationService = new NavigationService(null!, null!); //we will set these after
           
            //create view models with navigation service
            var customerListViewModel = new CustomerListViewModel(customerService, _navigationService);
            var customerDetailViewModel = new CustomerDetailViewModel(customerService, _navigationService);

            //update navigation service with view models
            typeof(NavigationService)
                .GetField("_customerListViewModel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(_navigationService, customerListViewModel);

            typeof(NavigationService)
               .GetField("_customerDetailViewModel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
               ?.SetValue(_navigationService, customerDetailViewModel);

            //set content control and navigate to customer list
            _navigationService.SetContentControl(MainContent);
        }

        private void CustomersButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateToCustomerList();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            _navigationService.NavigateToCreateCustomer();
        }
    }
}