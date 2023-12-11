using System.Text;
using Business.Services;
using Data.Models;

namespace Test.Services;

public class ClientServiceTest : TestsBase
{
    private readonly ClientService _clientService;
    private readonly User _user = new User("test@example.com", Encoding.UTF8.GetBytes("pa$$word"));

    private readonly Client _client = new Client("Jon", "Doe", "Jr", "380873908258", "john.doe@example.com", "Lviv",
        "Factory",
        new DateOnly(2023, 1, 1), ClientStatus.Active);

    public ClientServiceTest() : base("ClientServiceTest.db")
    {
        _clientService = new ClientService(ContextProxy);
        SetUp();
    }

    private void SetUp()
    {
        ContextProxy.Users!.Add(_user);
        ContextProxy.SaveChanges();
    }

    private void DeleteClients()
    {
        ContextProxy.Clients!.RemoveRange(ContextProxy.Clients!);
        ContextProxy.SaveChanges();
    }


    [Fact]
    public void Add_NewClient_AddsClientToDatabase()
    {
        var newClient = new Client("Jon1", "Doe1", "Jr1", "380873908251", "john1.doe@example1.com", "Lviv",
            "Factory",
            new DateOnly(2023, 1, 1), ClientStatus.Active);

        var addedClient = _clientService.Add(newClient, _user);

        Assert.NotNull(addedClient);
        Assert.Equal(addedClient, addedClient);
        Assert.Contains(addedClient, ContextProxy.Clients!);
        DeleteClients();
    }

    [Fact]
    public void Add_DuplicateClient_ReturnsNull()
    {
        _clientService.Add(_client, _user);
        var duplicateClient = _clientService.Add(_client, _user);

        Assert.Null(duplicateClient);
        DeleteClients();
    }

    [Fact]
    public void Remove_ExistingClient_RemovesClientFromDatabase()
    {
        _clientService.Add(_client, _user);
        
        var removedClient = _clientService.Remove(_client);
        
        Assert.NotNull(removedClient);
        Assert.Equal(_client, removedClient);
        Assert.DoesNotContain(_client, ContextProxy.Clients!);
        DeleteClients();
    }

    [Fact]
    public void GetActiveClientsCount_ReturnsCorrectCount()
    {
        _client.User = _user;
        var inactiveClient = new Client("Jon1", "Doe1", "Jr1", "380873908251", "john1.doe@example1.com", "Lviv",
            "Factory",
            new DateOnly(2023, 1, 1), ClientStatus.Inactive)
        {
            User = _user
        };
        ContextProxy.Clients!.AddRange(_client, inactiveClient);
        ContextProxy.SaveChanges();

        var activeCount = _clientService.GetActiveClientsCount(_user);

        Assert.Equal(1, activeCount);
        DeleteClients();
    }

    [Fact]
    public void Update_ExistingClient_UpdatesClientInDatabase()
    {
        Client? updatedClient = new Client("Jon1", "Doe1", "Jr1", "380873908251", "john1.doe@example1.com", "Lviv",
            "Factory",
            new DateOnly(2023, 1, 1), ClientStatus.Active);

        updatedClient = _clientService.Add(_client, _user);

        updatedClient!.FirstName = "NotJon";

        var result = _clientService.Update(updatedClient);
        
        Assert.NotNull(result);
        Assert.Equal(updatedClient, result);
        var updatedFromDb = ContextProxy.Clients!.Find(updatedClient.Id);
        Assert.Equal(updatedClient.FirstName, updatedFromDb?.FirstName);
        DeleteClients();
    }
    
    [Fact]
    public void GetInactiveClientsCount_ReturnsCorrectCount()
    {
        _client.User = _user;
        var inactiveClient = new Client("Jon1", "Doe1", "Jr1", "380873908251", "john1.doe@example1.com", "Lviv",
            "Factory",
            new DateOnly(2023, 1, 1), ClientStatus.Inactive)
        {
            User = _user
        };

        ContextProxy.Clients!.AddRange(_client, inactiveClient);
        ContextProxy.SaveChanges();

        var inactiveCount = _clientService.GetInactiveClientsCount(_user);

        Assert.Equal(1, inactiveCount);
        DeleteClients();
    }

    [Fact]
    public void GetByEmailAndPhoneNumber_ExistingClient_ReturnsClient()
    {
        _clientService.Add(_client, _user);
        
        var result = _clientService.GetByEmailAndPhoneNumber(_client.Email, _client.PhoneNumber);
        
        Assert.NotNull(result);
        Assert.Equal(_client, result);
        DeleteClients();
    }

    [Fact]
    public void GetByEmailAndPhoneNumber_NonExistingClient_ReturnsNull()
    {
        var nonExistingEmail = "nonexisting@example.com";
        var nonExistingPhoneNumber = "123456789";
        
        var result = _clientService.GetByEmailAndPhoneNumber(nonExistingEmail, nonExistingPhoneNumber);
        
        Assert.Null(result);
    }
    
    [Fact]
    public void GetAllByUser_ReturnsAllClientsForUser()
    {
        var user2 = new User("test2@example.com", Encoding.UTF8.GetBytes("password"));
        ContextProxy.Users!.Add(user2);
        ContextProxy.SaveChanges();

        var client2 = new Client("John", "Doe", "Jr", "380873908258", "john.doe@example.com", "Lviv",
            "Factory",
            new DateOnly(2023, 1, 1), ClientStatus.Active)
        {
            User = user2
        };

        _clientService.Add(_client, _user);
        _clientService.Add(client2, user2);
        
        var result = _clientService.GetAllByUser(_user);
        
        Assert.NotNull(result);
        Assert.Contains(_client, result);
        Assert.DoesNotContain(client2, result);
        DeleteClients();
    }
    
    [Fact]
    public void ClientContains_ClientContainsQuery_ReturnsTrue()
    {
        var query = "John";
        _clientService.Add(_client, _user);
        
        var result = ClientService.ClientContains(_client, query);
        
        Assert.True(result);
        DeleteClients();
    }
    
    
    [Fact]
    public void ClientContains_ClientDoesNotContainQuery_ReturnsFalse()
    {
        var query = "Nonexistent";
        _clientService.Add(_client, _user);
        
        var result = ClientService.ClientContains(_client, query);
        
        Assert.False(result);
        DeleteClients();
    }
    
    [Fact]
    public void GetClientsCountByDay_ReturnsCorrectCount()
    {
        // Arrange
        var startDate = new DateOnly(2023, 1, 1);
        var endDate = new DateOnly(2023, 1, 3);

        _client.User = _user;
        var startDateClient = new Client("Jon1", "Doe1", "Jr1", "380873908251", "john1.doe@example1.com", "Lviv",
            "Factory",
            new DateOnly(2023, 1, 1), ClientStatus.Inactive)
        {
            User = _user
        };
        var endDateClient = new Client("Jon2", "Doe2", "Jr2", "380873908252", "john2.doe@example1.com", "Lviv",
            "Factory",
            new DateOnly(2023, 1, 3), ClientStatus.Inactive)
        {
            User = _user
        };

        var clients = new List<Client>
        {
            _client,
            startDateClient,
            endDateClient
        };

        ContextProxy.Clients!.AddRange(clients);
        ContextProxy.SaveChanges();

        var clientsCountByDay = _clientService.GetClientsCountByDay(_user, startDate, endDate);

        Assert.Equal(2, clientsCountByDay[startDate]);
        Assert.Equal(1, clientsCountByDay[endDate]);
        DeleteClients();
    }
    
    [Fact]
    public void IsEmailUnique_UniqueEmail_ReturnsTrue()
    {
        var email = "unique@email.com";
        _clientService.Add(_client, _user);
        
        var result = _clientService.IsEmailUnique(email, _user);
        
        Assert.True(result);
        DeleteClients();
    }
    
    [Fact]
    public void IsEmailUnique_NonUniqueEmail_ReturnsFalse()
    {
        _clientService.Add(_client, _user);
        
        var result = _clientService.IsEmailUnique(_client.Email!, _user);
        
        Assert.False(result);
        DeleteClients();
    }
    
    [Fact]
    public void IsPhoneNumberUnique_UniquePhoneNumber_ReturnsTrue()
    {
        var phoneNumber = "380873908158";
        _clientService.Add(_client, _user);
        
        var result = _clientService.IsPhoneNumberUnique(phoneNumber, _user);
        
        Assert.True(result);
        DeleteClients();
    }
    
    [Fact]
    public void IsPhoneNumberUnique_NonUniquePhoneNumber_ReturnsFalse()
    {
        _clientService.Add(_client, _user);
        
        var result = _clientService.IsPhoneNumberUnique(_client.PhoneNumber!, _user);
        
        Assert.False(result);
        DeleteClients();
    }
}