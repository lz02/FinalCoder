using FinalCoder.Core.Models;

namespace FinalCoder.API.Models.Responses
{
    public class SaleDetailResponse
    {
        public long SaleDetailId { get; set; }
        public Product Product { get; set; }
    }
    public class ProductSaleResponse
    {
        public long SaleId { get; set; }
        public long UserId { get; set; }
        public List<SaleDetailResponse> SaleDetails { get; set; }
    }
}
