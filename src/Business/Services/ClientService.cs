using Data.Context;
using Data.Models;

namespace Business.Services;

public class ClientService
{
    private readonly SQLiteContext _context;

    public ClientService()
    {
        _context = SQLiteContextSingleton.Instance;
    }

    public ClientService(SQLiteContext context)
    {
        _context = context;
    }

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
        Client removed = _context.Remove(client).Entity;
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
        return _context.Clients!.Where(client => client.User == user).ToList();
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

    public int GetActiveClientsCount(User user)
    {
        return _context.Clients?.Count(client => client.User == user && client.Status == ClientStatus.Active) ?? 0;
    }

    public int GetInactiveClientsCount(User user)
    {
        return _context.Clients?.Count(client => client.User == user && client.Status == ClientStatus.Inactive) ?? 0;
    }

    public Dictionary<DateOnly, int> GetClientsCountByDay(User user, DateOnly startDate, DateOnly endDate)
    {
        var clientsByDay = _context.Clients?
            .Where(client => client.User == user && client.DateAdded >= startDate && client.DateAdded <= endDate)
            .AsEnumerable()
            .GroupBy(client => client.DateAdded)
            .ToDictionary(
                group => group.Key,
                group => group.Count()
            );

        var result = new Dictionary<DateOnly, int>();
        for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (!clientsByDay!.ContainsKey(date))
            {
                result[date] = 0;
            }
            else
            {
                result[date] = clientsByDay[date];
            }
        }

        return result;
    }
}