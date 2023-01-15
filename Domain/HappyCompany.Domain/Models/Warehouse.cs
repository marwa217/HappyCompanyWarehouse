
using HappyCompany.Infra.Constants;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyCompany.Domain.Models
{
    public class Warehouse
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        public string Country { get; set; }
    }
}
