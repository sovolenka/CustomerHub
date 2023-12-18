using Business.IO;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

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
        // var reminders = GetRemindersWithoutUser();

        CsvExporter.ExportToCsv(clients, Path.Combine(directory, ClientsFileName));
        CsvExporter.ExportToCsv(products, Path.Combine(directory, ProductsFileName));
        // CsvExporter.ExportToCsv(reminders, Path.Combine(directory, RemindersFileName));
    }

    public void ImportFromCsv(string directory)
    {
        var clients = CsvImporter.ImportFromCsv<Client>(Path.Combine(directory, ClientsFileName)).ToList();
        var products = CsvImporter.ImportFromCsv<Product>(Path.Combine(directory, ProductsFileName)).ToList();
        // var reminders = CsvImporter.ImportFromCsv<Reminder>(Path.Combine(directory, RemindersFileName)).ToList();

        SetAuthorizedUserToClients(clients);
        SetAuthorizedUserToProducts(products);
        // SetAuthorizedUserToReminders(reminders);
        
        _context.Clients?.AttachRange(clients);
        _context.Products?.AttachRange(products);
        // var currentUser = AuthorizationService.AuthorizedUser;
        // currentUser?.Reminders.AddRange(reminders);
        // if (currentUser != null) _context.Users?.Update(currentUser);
        _context.SaveChanges();
    }

    private void SetAuthorizedUserToClients(List<Client> clients)
    {
        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].User = AuthorizationService.AuthorizedUser;
        }
    }

    private void SetAuthorizedUserToProducts(List<Product> products)
    {
        for (int i = 0; i < products.Count; i++)
        {
            products[i].User = AuthorizationService.AuthorizedUser;
        }
    }

    private void SetAuthorizedUserToReminders(List<Reminder> reminders)
    {
        for (int i = 0; i < reminders.Count; i++)
        {
            reminders[i].User = AuthorizationService.AuthorizedUser;
        }
    }

    private List<Client> GetClientsWithoutUser()
    {
        var clients = _context.Clients?.Where(c => c.User == AuthorizationService.AuthorizedUser).ToList() ?? new List<Client>();
        for (int i = 0; i < clients.Count; i++)
        {
            clients[i].Id = 0;
            clients[i].User = null;
        }

        return clients;
    }

    private List<Product> GetProductsWithoutUser()
    {
        var products = _context.Products?
            .Where(c => c.User == AuthorizationService.AuthorizedUser)
            .Include(p => p.Characteristic)
            .ToList() ?? new List<Product>();
        for (int i = 0; i < products.Count; i++)
        {
            products[i].Id = 0;
            products[i].User = null;
        }

        return products;
    }

    private List<Reminder> GetRemindersWithoutUser()
    {
        var reminders = AuthorizationService.AuthorizedUser?.Reminders?.ToList() ?? new List<Reminder>();
        for (int i = 0; i < reminders.Count; i++)
        {
            reminders[i].Id = 0;
            reminders[i].User = null;
        }

        return reminders;
    }
}