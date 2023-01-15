

namespace HappyCompany.Domain.Data
{
    public class WarehouseDto
    {

        [MaxLength(100)]
        public string Name { get; set; }


        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        public string Country { get; set; }
    }
}
