using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.OperationTypes.Any())
            {
                for (int i = 1; i <= 35; i++)
                {
                    context.OperationTypes.Add(new OperationType
                    {
                        Name = i % 3 == 0 ? "Korrapärane hooldus" : i % 3 == 1 ? "Veermiku remont" : "Elektritööd",
                        Description = $"Standardne tehnohooldusprotseduur etapi koodiga {100 + i}."
                    });
                }
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                for (int i = 1; i <= 35; i++)
                {
                    context.Employees.Add(new Employee
                    {
                        FirstName = $"Mehaanik{i}",
                        LastName = i % 2 == 0 ? "Sepp" : "Kask",
                        Email = $"mehaanik{i}@kooliprojekt.ee",
                        PasswordHash = "AQAAAAIAAYagAAAAEJj...",
                        Role = "Mechanic"
                    });
                }
                context.SaveChanges();
            }

            if (!context.Cars.Any())
            {
                string[] makes = { "Volkswagen", "Audi", "BMW", "Toyota", "Ford" };
                string[] models = { "Golf", "A6", "320i", "Corolla", "Focus" };

                for (int i = 1; i <= 35; i++)
                {
                    context.Cars.Add(new Car
                    {
                        Make = makes[i % makes.Length],
                        Model = models[i % models.Length],
                        RegNum = $"{100 + i} ABC"
                    });
                }
                context.SaveChanges();
            }

            if (!context.Operations.Any())
            {
                var carId = context.Cars.First().Id;
                var employeeId = context.Employees.First().Id;
                var operationTypeId = context.OperationTypes.First().Id;

                string[] statuses = { "Teostatud", "Töös", "Ootel" };

                for (int i = 1; i <= 35; i++)
                {
                    context.Operations.Add(new Operation
                    {
                        Date = DateTime.Now.AddDays(-i),
                        Status = statuses[i % statuses.Length],
                        Cost = 45.50m + (i * 10m),
                        CarId = carId,
                        EmployeeId = employeeId,
                        OperationTypeId = operationTypeId
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
