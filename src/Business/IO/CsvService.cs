using Business.Services;
using Data.Context;
using Data.Models;

namespace Business.IO;

public class CsvService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;
    private readonly UserService _userService = new();
    private readonly ClientService _clientService = new();
    private readonly ProductService _productService = new();

    public void ExportToCsv(string directory)
    {
        var users = _context.Users?.ToList() ?? new List<User>();
        CsvExporter.ExportToCsv(users, directory + "\\Users.csv");

        var clients = _context.Clients?.ToList() ?? new List<Client>();
        CsvExporter.ExportToCsv(clients, directory + "\\Clients.csv");

        var products = _context.Products?.ToList() ?? new List<Product>();
        CsvExporter.ExportToCsv(products, directory + "\\Products.csv");
    }
    
    public void ImportFromCsv(string directory)
    {
        var users = CsvImporter.ImportFromCsv<User>(directory + "\\Users.csv");
        foreach (var user in users)
        {
            try
            {
                _userService.Add(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        var clients = CsvImporter.ImportFromCsv<Client>(directory + "\\Clients.csv");
        foreach (var client in clients)
        {
            try
            {
                _clientService.Add(client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        var products = CsvImporter.ImportFromCsv<Product>(directory + "\\Products.csv");
        foreach (var product in products)
        {
            try
            {
                _productService.Add(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}