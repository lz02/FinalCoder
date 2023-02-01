using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;

/* Obtener un PRODUCTO por su ID */
//Product remeraMangaLarga = ProductRepository.GetById(1);
//Console.WriteLine(remeraMangaLarga.Description);
/* ============================================================== */

/* Obtener un PRODUCTO por su Nombre/Descripcion */
//Product buzo = ProductRepository.GetByDescription("Buzo");
//Console.WriteLine(buzo.Description);
/* ============================================================== */

/* Listar todos los registros de la tabla Producto */
//IEnumerable<Product> productos = ProductRepository.GetAll();
//foreach (var p in productos)
//{
//    Console.WriteLine(p.Description);
//}
/* ============================================================== */

/* INSERTAR un Producto */
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
/* ============================================================== */

/* ACTUALIZAR un Producto */
//var _gorra = ProductRepository.GetByDescription("Gorra");
//Console.WriteLine($"{gorra.ID} {gorra.Description}");

//_gorra.Description = "Gorra roja";
//ProductRepository.Update(gorra);

//Console.WriteLine($"{gorra.ID} {gorra.Description}");
/* ============================================================== */

/* ELIMINAR un Producto */
var _gorra = ProductRepository.GetByDescription("Gorra");
Console.WriteLine(ProductRepository.Delete(_gorra));
/* ============================================================== */
