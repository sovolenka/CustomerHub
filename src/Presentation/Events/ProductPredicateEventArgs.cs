using Data.Models;
using System;

namespace Presentation.Events;

public class ProductPredicateEventArgs : EventArgs
{
    public Predicate<Product>? Predicate { get; set; }

    public ProductPredicateEventArgs(Predicate<Product>? predicate)
    {
        this.Predicate = predicate;
    }
}
