using Data.Context;
using Data.Models;

namespace Business.Services;

public class ClientService
{
    private readonly SQLiteContext _context = SQLiteContextSingleton.Instance;

    public Client? Add(Client client, User user)
    {
        var usersClients = _context.Clients?.Where(c =>
            (c.PhoneNumber == client.PhoneNumber || c.Email == client.Email) && c.User == user);
        if (usersClients!.Any()) return null;

        client.User = user;
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

    public Client? GetByEmailAndPhoneNumber(string email, string phoneNumber)
    {
        return Enumerable.FirstOrDefault(_context.Clients!,
            client => client.Email == email && client.PhoneNumber == phoneNumber);
    }

    public IEnumerable<Client> GetAll(User user)
    {
        return _context.Clients!.Where(client => client.User == user).ToList();
    }
}