﻿using System.Text;
using Business.Services;
using Data.Models;

namespace Test.Services;

public class ProductServiceTest : TestsBase
{
    private readonly ProductService _productService;
    private readonly User _user;
    private readonly Characteristic _characteristic;
    private readonly Product _product;

    public ProductServiceTest() : base("ProductServiceTest.db")
    {
        _productService = new ProductService(ContextProxy);
        _user = new User("test@example.com", Encoding.UTF8.GetBytes("pa$$word"));
        _characteristic = new Characteristic("Type", "Category", "Description", "Manufacturer", "Country",
            new DateOnly(2023, 1, 1), ProductStatus.New);
        _product = new Product("Product", 10, _characteristic);
        SetUp();
    }

    private void SetUp()
    {
        ContextProxy.Users!.Add(_user);
        ContextProxy.SaveChanges();
    }
    
    private void ClearDatabase()
    {
        ContextProxy.Products!.RemoveRange(ContextProxy.Products!);
        ContextProxy.Users!.RemoveRange(ContextProxy.Users!);
        ContextProxy.SaveChanges();
    }

    [Fact]
    public void Add_NewProduct_AddsProductToDatabase()
    {
        ClearDatabase();
        ContextProxy.Users!.Add(_user);
        ContextProxy.SaveChanges();
        var addedProduct = _productService.Add(_product, _user);

        Assert.NotNull(addedProduct);
        Assert.Equal(_product, addedProduct);
    }

    [Fact]
    public void Add_WithUser_SetsUserOnProduct()
    {
        ClearDatabase();
        var addedProduct = _productService.Add(_product, _user);

        Assert.NotNull(addedProduct);
        Assert.Equal(_user, addedProduct?.User);
    }

    [Fact]
    public void Remove_ExistingProduct_RemovesProductFromDatabase()
    {
        ClearDatabase();

        var product = new Product
        {
            Name = "TestProduct",
            Price = 10,
            Characteristic = new Characteristic
            {
                ProductType = "Type",
                Category = "Category",
                Description = "Description",
                Manufacturer = "Manufacturer",
                Country = "Country",
                ManufactureDate = new DateOnly(2023, 1, 1),
                ProductStatus = ProductStatus.New
            },
            User = _user
        };
        ContextProxy.Products!.Add(product);
        ContextProxy.SaveChanges();
        
        var removedProduct = _productService.Remove(product);

        Assert.Equal(product, removedProduct);
        Assert.DoesNotContain(product, ContextProxy.Products!);
    }
}