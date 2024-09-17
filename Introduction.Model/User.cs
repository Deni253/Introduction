using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Email { get; set; }
        public Role? role { get; set; }
    }
}
