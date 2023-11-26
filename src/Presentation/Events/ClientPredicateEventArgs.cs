using System;
using Data.Models;

namespace Presentation.Events;

public class ClientPredicateEventArgs : EventArgs
{
    public Predicate<Client> Predicate { get; set; }

    public ClientPredicateEventArgs(Predicate<Client> predicate)
    {
        Predicate = predicate;
    }
}