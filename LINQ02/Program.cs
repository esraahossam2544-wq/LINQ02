// See https://aka.ms/new-console-template for more information
// 1. Top 3 most expensive
using LINQ.Models;
using LINQ.DataSources;
var ProductList = Source.ProductList;
var CustomerList = Source.CustomerList;

var top3 = ProductList.OrderByDescending(p => p.UnitPrice).Take(3);
foreach (var p in top3) Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

// 2. Page 2, page size = 5
var page2 = ProductList.Skip(5).Take(5);
foreach (var p in page2) Console.WriteLine(p.ProductName);

// 3. Take while price < 25 (ordered by price)
var cheap = ProductList.OrderBy(p => p.UnitPrice).TakeWhile(p => p.UnitPrice < 25);
foreach (var p in cheap) Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

// 4. ALL Seafood in stock
bool allSeafoodInStock = ProductList
    .Where(p => p.Category == "Seafood")
    .All(p => p.UnitsInStock > 0);
Console.WriteLine(allSeafoodInStock);

// 5. IDs contains 9
int[] ids = { 3, 9, 13, 18 };
Console.WriteLine(ids.Contains(9));

// 6. Group by category with count
var groups = ProductList.GroupBy(p => p.Category);
foreach (var g in groups) Console.WriteLine($"{g.Key}: {g.Count()}");

// 7. Group by category, names only
foreach (var g in ProductList.GroupBy(p => p.Category))
{
    Console.WriteLine(g.Key);
    foreach (var name in g.Select(p => p.ProductName))
        Console.WriteLine($"   {name}");
}

// 8. Categories with MORE THAN 3 products
var big = ProductList.GroupBy(p => p.Category).Where(g => g.Count() > 3).Select(g => g.Key);
foreach (var c in big) Console.WriteLine(c);

// 9. Query syntax: customers by Country
var byCountry = from c in CustomerList
                group c by c.Country into g
                select new
                {
                    Country = g.Key,
                    Count = g.Count(),
                    TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
                };
foreach (var x in byCountry)
    Console.WriteLine($"{x.Country} - {x.Count} - {x.TotalOrderValue}");

// 10. Total units in stock
Console.WriteLine(ProductList.Sum(p => p.UnitsInStock));

// 11. Cheapest and most expensive prices
Console.WriteLine($"Min: {ProductList.Min(p => p.UnitPrice)}, Max: {ProductList.Max(p => p.UnitPrice)}");

// 12. Distinct categories
foreach (var c in ProductList.Select(p => p.Category).Distinct()) Console.WriteLine(c);

// 13. In setA but NOT in setB
int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
int[] setB = { 3, 6, 9, 12, 15, 13 };
Console.WriteLine(string.Join(", ", setA.Except(setB)));

// 14. list1 but NOT list2 (case-insensitive)
string[] list1 = { "Germany", "France", "UK", "Spain" };
string[] list2 = { "france", "SPAIN", "Italy" };
Console.WriteLine(string.Join(", ", list1.Except(list2, StringComparer.OrdinalIgnoreCase)));

// 15. Dictionary keyed by ProductID
Dictionary<int, Product> dict = ProductList.ToDictionary(p => p.ProductID);
Console.WriteLine(dict[18].ProductName);

// 16. First product with price > 50
var first50 = ProductList.First(p => p.UnitPrice > 50);
Console.WriteLine(first50.ProductName);

// 17. First with price > 500, null if none
var first500 = ProductList.FirstOrDefault(p => p.UnitPrice > 500);
Console.WriteLine(first500?.ProductName ?? "null");

// 18. Multiplication table row for 7
foreach (var n in Enumerable.Range(1, 10)) Console.WriteLine($"7 x {n} = {7 * n}");

// 19. Even numbers between 1 and 30
Console.WriteLine(string.Join(", ", Enumerable.Range(1, 30).Where(n => n % 2 == 0)));

// 20. Concat first 3 product names with first 3 customer company names
var seq = ProductList.Take(3).Select(p => p.ProductName)
          .Concat(CustomerList.Take(3).Select(c => c.CustomerName));
foreach (var s in seq) Console.WriteLine(s);

// 21. Zip product with customer
var zipped = ProductList.Zip(CustomerList, (p, c) => $"{p.ProductName} sold to {c.CustomerName}");
foreach (var s in zipped) Console.WriteLine(s);
