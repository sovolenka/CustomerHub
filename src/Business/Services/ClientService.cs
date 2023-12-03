using Data.Context;
using Data.Models;

namespace Business.Services;

public class ClientService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;

    public Client? Add(Client client, User? user = null)
    {
        var usersClients = _context.Clients?.Where(c =>
            (c.PhoneNumber == client.PhoneNumber || c.Email == client.Email) && c.User == user);
        if (usersClients!.Any()) return null;

        if (user is not null) client.User = user;
        Client? added = _context.Clients?.Add(client).Entity;
        _context.SaveChanges();
        return added;
    }

    public Client? Remove(Client client)
    {
        Client? removed = _context.Remove(client).Entity;
        _context.SaveChanges();
        return removed;
    }

    public Client? Update(Client client)
    {
        Client? updated = _context.Clients?.Update(client).Entity;
        _context.SaveChanges();
        return updated;
    }

    public Client? GetByEmailAndPhoneNumber(string? email, string? phoneNumber)
    {
        return Enumerable.FirstOrDefault(_context.Clients!,
            client => client.Email == email && client.PhoneNumber == phoneNumber);
    }

    public IEnumerable<Client> GetAllByUser(User user)
    {
        return _context.Clients!.Where(client => client.User == user);
    }

    public static bool ClientContains(Client client, string query)
    {
        query = query.ToLower();
        return client.FirstName!.ToLower().Contains(query) ||
               client.SecondName!.ToLower().Contains(query) ||
               client.ThirdName!.ToLower().Contains(query) ||
               client.PhoneNumber!.ToLower().Contains(query) ||
               client.Email!.ToLower().Contains(query) ||
               client.Address!.ToLower().Contains(query) ||
               client.Factory!.ToLower().Contains(query) ||
               client.DateAdded.ToString().ToLower().Contains(query) ||
               client.Status.ToString().ToLower().Contains(query);
    }
}