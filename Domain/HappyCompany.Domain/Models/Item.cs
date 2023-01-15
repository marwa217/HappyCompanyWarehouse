using System.Configuration;

namespace HappyCompany.Domain.Models
{
    public class Item
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string? SKUCode { get; set; }

        [IntegerValidator(MinValue = 1)]
        public int Qty { get; set; }

        
        public decimal CostPrice { get; set; }

        public decimal MSRPPrice { get; set; }

       
        public int warehouseId { get; set; }

        public Warehouse warehouse { get; set; }
    }
}
