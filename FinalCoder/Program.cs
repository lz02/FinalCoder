using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;

//Product remeraMangaLarga = ProductRepository.GetById(1);
//Console.WriteLine(remeraMangaLarga.Description);

//Product buzo = ProductRepository.GetByDescription("Buzo");
//Console.WriteLine(buzo.Description);

//IEnumerable<Product> productos = ProductRepository.GetAll();
//foreach (var p in productos)
//{
//    Console.WriteLine(p.Description);
//}

//Product gorra = new Product
//{
//    Description = "Gorra",
//    Cost = 75,
//    SellPrice = 110,
//    Stock = 20,
//    UserId = 1,
//};
//int modificaciones = ProductRepository.Insert(gorra);

//var resultado = ProductRepository.GetByDescription(gorra.Description);
//Console.WriteLine($"{resultado.ID} {resultado.Description}");

//var gorra = ProductRepository.GetByDescription("Gorra");
//Console.WriteLine($"{gorra.ID} {gorra.Description}");

gorra.Description = "Gorra roja";

ProductRepository.Update(gorra);
Console.WriteLine($"{gorra.ID} {gorra.Description}");