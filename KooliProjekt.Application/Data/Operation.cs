using System;

namespace KooliProjekt.Application.Data
{
    public class Operation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal? Cost { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int OperationTypeId { get; set; }
        public OperationType OperationType { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
