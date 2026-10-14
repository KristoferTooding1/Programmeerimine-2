using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
    }
}
