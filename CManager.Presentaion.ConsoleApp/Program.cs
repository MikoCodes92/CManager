using CManager.Application.Services;
using CManager.Infrastructure.Data;
using CManager.Presentaion.ConsoleApp.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CManager.Presentaion.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        { 
           try
           {
             var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
             var appDirectory = Path.Combine(appDataPath, "CManager");
             var filePath = Path.Combine(appDirectory, "customers.json");

             Directory.CreateDirectory(appDirectory);
             
             var repository = new CustomerRepository(filePath);
             var service = new CustomerService(repository);
             var controller = new CustomerController(service);

             controller.RunMenu();
            }
           catch(Exception ex)
           {
             Console.WriteLine($"Fatal error: {ex.Message}");
             Console.WriteLine("Press any key to exit...");
             Console.ReadKey();
            }
        }
    }
}