using System;
using Data.Models;

namespace Presentation.Events;

public class EntityEventArgs : EventArgs
{
    public Client? Client { get; set; }
    public Product? Product { get; set; }
    
    public EntityEventArgs(Client client)
    {
        Client = client;
    }
    
    public EntityEventArgs(Product product)
    {
        Product = product;
    }
}