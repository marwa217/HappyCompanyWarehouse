using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace HappyCompany.Domain.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }

        [DefaultValue(false)]
        public bool Active { get; set; }

    }
}
