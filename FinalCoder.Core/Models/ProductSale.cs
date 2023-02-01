namespace FinalCoder.Core.Models
{
    public class ProductSale : ModelBase
    {
        public int Stock { get; set; }
        public long ProductId { get; set; }
        public long SaleId { get; set; }
    }
}
