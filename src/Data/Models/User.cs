﻿// <auto-generated> </auto-generated>
#nullable enable


namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;


    /// <summary>
    /// Represents a registered user of the application.
    /// </summary>
    /// <remarks>
    /// In the database, this class is used to store information about registered users,
    /// including their email, password hash, and associated products, clients, and reminders.
    /// </remarks>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class with specified parameters.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="passwordHash">The hash of the user's password.</param>
        public User(string email, byte[] passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the hash of the user's password.
        /// </summary>
        [Required]
        public byte[]? PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the list of products associated with the user.
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();

        /// <summary>
        /// Gets or sets the list of clients associated with the user.
        /// </summary>
        public List<Client> Clients { get; set; } = new List<Client>();

        /// <summary>
        /// Gets or sets the list of reminders associated with the user.
        /// </summary>
        public List<Reminder> Reminders { get; set; } = new List<Reminder>();
    }
}
