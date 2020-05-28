using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(

                new Department
                {
                    Id = 1,
                    Name = "HR",
                    LocationName = "Bangalore"
                },
                 new Department
                 {
                     Id = 2,
                     Name = "Sales",
                     LocationName = "Bangalore"
                 },
                  new Department
                  {
                      Id = 3,
                      Name = "IT",
                      LocationName = "Bangalore"
                  }
                );
            modelBuilder.Entity<Employee>().HasData(


                new Employee
                {
                    Id = 1,
                    DepartmentId=3,
                    FirstName = "Ra,jesh",
                    LastName = "K",
                    //Email = "Rajesh@gmail.com",
                    //FilePath = ""

                },
                new Employee
                {
                    Id = 2,
                    DepartmentId = 2,
                    FirstName = "Akash",
                    LastName = "S",
                    //Email = "Akash@gmail.com",
                    //FilePath = ""

                },
                new Employee
                {
                    Id = 3,
                    DepartmentId = 1,
                    FirstName = "Kiran",
                    LastName = "B",
                    //Email = "Kiran@gmail.com",
                    //FilePath = ""

                }
                );
        }
    }
}

