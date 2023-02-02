using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;

// Traer Usuario (recibe un int)
User user = UsersRepository.GetById(1);

// Traer Productos (recibe un id de usuario y, devuelve una lista con todos los productos cargado por ese usuario)
IEnumerable<Product> products = ProductsRepository.GetAllByUser(user.ID);

// Traer ProductosVendidos (recibe el id del usuario y devuelve una lista de productos vendidos por ese usuario)
IEnumerable<ProductSale> salesDetailsByUser = ProductSalesRepository.GetAllSalesByUser(user.ID);

/* Internamente ProductsRepository.GetAllSalesByUser hace uso de
 * ProductSalesRepository.GetAllSalesByUser para obtener las IDs
 * de los Productos y luego obtener el objeto Producto correspondiente. */
IEnumerable<Product> productSalesByUser = ProductsRepository.GetAllSalesByUser(user.ID);

// Traer Ventas (recibe el id del usuario y devuelve un a lista de Ventas realizadas por ese usuario)
IEnumerable<Sale> salesByUser = SalesRepository.GetByUserId(user.ID);

// Inicio de sesión (recibe un usuario y contraseña y devuelve un objeto Usuario)
User user2 = UsersRepository.LoginWithUsername(user.UserName, user.Password);