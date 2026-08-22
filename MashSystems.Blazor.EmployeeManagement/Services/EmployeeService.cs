using MashSystems.Blazor.EmployeeManagement.Data;
using MashSystems.Blazor.EmployeeManagement.Models;
using MashSystems.Blazor.EmployeeManagement.Models.DTOs;
using MashSystems.Blazor.EmployeeManagement.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace MashSystems.Blazor.EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        Task<GetEmployeesResponse> GetEmployees();
        Task<BaseResponse> AddEmployee(AddEmployeeForm form);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbContextFactory<DataContext> _factory;

        public EmployeeService(IDbContextFactory<DataContext> factory)
        {
            _factory = factory;
        }

        public async Task<BaseResponse> AddEmployee(AddEmployeeForm form)
        {
            var response = new BaseResponse();
            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    context.Add(new Employee
                    {
                        Name = form.Name,
                        Position = form.Position,
                        Salary = form.Salary,
                        Type = form.Type,
                        ImgUrl = form.ImgUrl,
                    });
                    var result = await context.SaveChangesAsync();

                    if (result == 1)
                    {
                        response.StatusCode = 200;
                        response.Message = "Employee Added Succesfully ";
                    }
                    else
                    {
                        response.StatusCode = 400;
                        response.Message = "Error occured while adding employee ";
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message ="Error adding employee" + ex.Message;
            }
            return response;

        }

        public async Task<GetEmployeesResponse> GetEmployees()
        {
            var response = new GetEmployeesResponse();

            try
            {
                using (var context = _factory.CreateDbContext())
                {
                    var employees = await context.Employees.ToListAsync();
                    response.Employees = employees;
                    response.StatusCode = 200;
                    response.Message = "success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = "Error retrieving employees: " + ex.Message;
                response.Employees = null;
            }
            return response;
        }
    }
}
