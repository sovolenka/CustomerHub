﻿using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public sealed class SQLiteContext: DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Client>? Clients { get; set; }
    public DbSet<Product>? Products { get; set; }

    public string DbPath { get; }

    public SQLiteContext(string dbPath = "customerhub.db")
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, dbPath);
        Database.EnsureCreated();
    }
    
    public void DropDatabase()
    {
        Database.EnsureDeleted();
    }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DbPath}");
    }
}
