using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string RegNum { get; set; }

        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
    }
}
