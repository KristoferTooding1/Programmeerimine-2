using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class OperationType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Operation> Operations { get; set; } = new List<Operation>();
    }
}
