using FinalCoder.Core.Models;
using FinalCoder.Core.Repositories;

static string UserToString(User user)
{
    return $"ID: {user.ID}\n" +
        $"Nombre: {user.Name}\n" +
        $"Apellido: {user.Surname}\n" +
        $"Nombre de usuario: {user.UserName}\n" +
        $"Email: {user.Email}\n" +
        $"Contraseña: {user.Password}\n";
}
static string ProductToString(Product product)
{
    return $"ID: {product.ID}\n" +
        $"Descripciones: {product.Description}\n" +
        $"Costo: {product.Cost}\n" +
        $"Precio de Venta: {product.SellPrice}\n" +
        $"Stock: {product.Stock}\n" +
        $"ID Usuario: {product.UserId}\n";
}
static string ProductsToString(Product[] products)
{
    string result = "Productos:\n";
    for (int i = 0; i < products.Length; i++)
    {
        result += $"Produto {i + 1}\n\n" +
            $"{ProductToString(products[i])}\n";
    }
    return result;
}
static string ProductSaleToString(ProductSale productSale)
{
    return $"ID: {productSale.ID}\n" +
        $"ID Producto: {productSale.ProductId}\n" +
        $"ID Venta: {productSale.SaleId}\n" +
        $"Stock: {productSale.Stock}\n";
}
static string ProductSalesToString(ProductSale[] productSales)
{
    string result = "Productos Vendidos:\n";
    for (int i = 0; i < productSales.Length; i++)
    {
        result += $"Detalle {i + 1}\n\n" +
            $"{ProductSaleToString(productSales[i])}\n";
    }
    return result;
}
static string SaleToString(Sale sale)
{
    return $"ID: {sale.ID}\n" +
        $"Descripcion: {sale.Description}\n" +
        $"ID Usuario: {sale.UserId}\n";
}
static string SalesToString(Sale[] sales)
{
    string result = "Ventas:\n";
    for (int i = 0; i < sales.Length; i++)
    {
        result += $"Venta {i + 1}\n\n" +
            $"{SaleToString(sales[i])}\n";
    }
    return result;
}

// Traer Usuario (recibe un int)
long userId = 1;
User user = UsersRepository.GetById(userId);
Console.WriteLine($"Usuario {userId}:");
Console.WriteLine(UserToString(user));

// Traer Productos (recibe un id de usuario y, devuelve una lista con todos los productos cargado por ese usuario)
IEnumerable<Product> products = ProductsRepository.GetAllByUser(user.ID);
Console.WriteLine(ProductsToString(products.ToArray()));

// Traer ProductosVendidos (recibe el id del usuario y devuelve una lista de productos vendidos por ese usuario)
IEnumerable<ProductSale> salesDetailsByUser = ProductSalesRepository.GetAllByUser(user.ID);
Console.WriteLine(ProductSalesToString(salesDetailsByUser.ToArray()));

/* Internamente ProductsRepository.GetAllSalesByUser hace uso de
 * ProductSalesRepository.GetAllSalesByUser para obtener las IDs
 * de los Productos y luego obtener el objeto Producto correspondiente. */
IEnumerable<Product> productSalesByUser = ProductsRepository.GetAllSalesByUser(user.ID);
Console.WriteLine(ProductsToString(productSalesByUser.ToArray()));

// Traer Ventas (recibe el id del usuario y devuelve un a lista de Ventas realizadas por ese usuario)
IEnumerable<Sale> salesByUser = SalesRepository.GetAllByUser(user.ID);
Console.WriteLine(SalesToString(salesByUser.ToArray()));

// Inicio de sesión (recibe un usuario y contraseña y devuelve un objeto Usuario)
Console.WriteLine("Ingrese Nombre de Usuario:");
string username = Console.ReadLine();
Console.WriteLine("Ingrese Contraseña:");
string password = Console.ReadLine();

User loggedUser = UsersRepository.LoginWithUsername(username, password);
try
{
    string userData = UserToString(loggedUser);

    Console.WriteLine("\nINICIO DE SESION EXITOSO\nDatos del Usuario:\n");
    Console.WriteLine(userData);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
