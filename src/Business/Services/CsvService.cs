using Business.IO;
using Data.Context;
using Data.Models;

namespace Business.Services;

public class CsvService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;

    private const string ClientsFileName = "clients.csv";
    private const string ProductsFileName = "products.csv";
    private const string RemindersFileName = "reminders.csv";
    
    public void ExportToCsv(string directory)
    {
        var clients = GetClientsWithoutUser();
        var products = GetProductsWithoutUser();
        var reminders = GetRemindersWithoutUser();

        CsvExporter.ExportToCsv(clients, Path.Combine(directory, ClientsFileName));
        CsvExporter.ExportToCsv(products, Path.Combine(directory, ProductsFileName));
        CsvExporter.ExportToCsv(reminders, Path.Combine(directory, RemindersFileName));
    }

    public void ImportFromCsv(string directory)
    {
        var clients = CsvImporter.ImportFromCsv<Client>(Path.Combine(directory, ClientsFileName)).ToList();
        var products = CsvImporter.ImportFromCsv<Product>(Path.Combine(directory, ProductsFileName)).ToList();
        var reminders = CsvImporter.ImportFromCsv<Reminder>(Path.Combine(directory, RemindersFileName)).ToList();

        SetAuthorizedUserToClients(clients);
        SetAuthorizedUserToProducts(products);
        SetAuthorizedUserToReminders(reminders);

        _context.Clients?.AddRange(clients);
        _context.Products?.AddRange(products);
        var currentUser = AuthorizationService.AuthorizedUser;
        currentUser?.Reminders.AddRange(reminders);
        if (currentUser != null) _context.Users?.Update(currentUser);
        _context.SaveChanges();
    }


    private void SetAuthorizedUserToClients(List<Client> clients)
    {
        foreach (var client in clients)
        {
            client.User = AuthorizationService.AuthorizedUser;
        }
    }

    private void SetAuthorizedUserToProducts(List<Product> products)
    {
        foreach (var product in products)
        {
            product.User = AuthorizationService.AuthorizedUser;
        }
    }

    private void SetAuthorizedUserToReminders(List<Reminder> reminders)
    {
        foreach (var reminder in reminders)
        {
            reminder.User = AuthorizationService.AuthorizedUser;
        }
    }

    private List<Client> GetClientsWithoutUser()
    {
        var clients = _context.Clients?.ToList() ?? new List<Client>();
        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].User = null;
        }

        return clients;
    }

    private List<Product> GetProductsWithoutUser()
    {
        var products = _context.Products?.ToList() ?? new List<Product>();
        for (int i = 0; i < products.Count; i++)
        {
            products[i].User = null;
        }

        return products;
    }

    private List<Reminder> GetRemindersWithoutUser()
    {
        var reminders = AuthorizationService.AuthorizedUser?.Reminders?.ToList() ?? new List<Reminder>();
        for (int i = 0; i < reminders.Count; i++)
        {
            reminders[i].User = null;
        }

        return reminders;
    }
}