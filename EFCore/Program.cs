// See https://aka.ms/new-console-template for more information
using EFCore.Models;
var context = new HplusSportsContext();

//var salesPeople = context.Salespeople.Where(x => x.LastName.StartsWith("S"));
//salesPeople.ToList().ForEach(s => Console.WriteLine(s.FullName));

/*
var customer = context.Customers.First();
var salesperson = context.Salespeople.First();
var product = context.Products.First();

var newOrder = new Order()
{
    Customer = customer,
    Salesperson = salesperson,
    OrderItems = { new OrderItem() { Product = product } },
    OrderDate = DateTime.Now
};

context.Orders.Add(newOrder);
context.SaveChanges();
Console.WriteLine($"{newOrder.OrderId} - {newOrder.LastUpdate}");
*/

//var perishableProducts = context.PerishableProducts.ToList();
//var products = context.Products.ToList();

//perishableProducts.ForEach(p => Console.WriteLine($"{p.Variety} {p.ProductName} " ));

var sqlContext = new HplusSportsContext();
var sqliteContext = new SqliteDBContext();

sqliteContext.Orders.AddRange(sqlContext.Orders);
sqliteContext.Customers.AddRange(sqlContext.Customers);
sqliteContext.OrderItems.AddRange(sqlContext.OrderItems);
sqliteContext.Products.AddRange(sqlContext.Products);
sqliteContext.SalesGroups.AddRange(sqlContext.SalesGroups);
sqliteContext.Salespeople.AddRange(sqlContext.Salespeople);
sqliteContext.PerishableProducts.AddRange(sqlContext.PerishableProducts);
sqliteContext.SaveChanges();
Console.WriteLine("Complete copy data from sql server to sql lite");

Console.ReadKey();
