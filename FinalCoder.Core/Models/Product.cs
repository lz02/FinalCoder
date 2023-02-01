namespace FinalCoder.Core.Models
{
    public class Product : ModelBase
    {
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal SellPrice { get; set; }
        public int Stock { get; set; }

        public long UserId { get; set; }
    }
}
