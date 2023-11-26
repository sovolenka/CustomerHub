using System;
using Data.Models;

namespace Presentation.Events;

public class ClientEventArgs : EventArgs
{
    public Client Client { get; set; }
    
    public ClientEventArgs(Client client)
    {
        Client = client;
    }
}