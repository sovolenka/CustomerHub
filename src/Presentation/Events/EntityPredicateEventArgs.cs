using System;
using Data.Models;

namespace Presentation.Events;

public class EntityPredicateEventArgs : EventArgs
{
    public Predicate<Client> ClientPredicate { get; set; }
    public Predicate<Product> ProductPredicate { get; set; }

    public EntityPredicateEventArgs(Predicate<Client> clientPredicate, Predicate<Product> productPredicate)
    {
        ClientPredicate = clientPredicate;
        ProductPredicate = productPredicate;
    }
}