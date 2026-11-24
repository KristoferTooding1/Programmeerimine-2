using System;

namespace KooliProjekt.Application.Dto
{
    public class OperationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public decimal? Cost { get; set; }
        public int CarId { get; set; }
        public string CarRegNum { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int OperationTypeId { get; set; }
        public string OperationTypeName { get; set; }
    }
}
