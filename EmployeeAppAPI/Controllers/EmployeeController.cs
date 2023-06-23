using Azure.Core;
using EmployeeAppAPI.Data;
using EmployeeAppAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeAppDbContext _context;
        public EmployeeController(EmployeeAppDbContext context)
        {
            _context = context;
        }

        //C R U D
        // C = Create
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody]EmpModel emp)
        {
            var checkemp = await _context.Employee.Where(e => e.email == emp.email).FirstOrDefaultAsync();
            if(checkemp != null)
            {
                return Ok("Employee is Already there in DB");
            }
            else
            {
                Employee employee = new Employee();
                employee.id = Guid.NewGuid();
                employee.name = emp.name;
                employee.salary = emp.salary;
                employee.phone = emp.phone;
                employee.designation = emp.designation;
                employee.email = emp.email;

                await _context.Employee.AddAsync(employee);
                await _context.SaveChangesAsync();
                return Ok(employee);
            }
            
        } 

        // R = Read
        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _context.Employee.ToListAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var emp = await _context.Employee.FindAsync(id);
            if (emp!=null)
            {
                return Ok(emp);
            }
            else
            {
                return NotFound();
            }
        }

        // U = Update
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, EmpModel emp)
        {
            var employee = await _context.Employee.FindAsync(id);
            if(employee != null)
            {
                employee.email = emp.email;
                employee.phone = emp.phone;
                employee.salary = emp.salary;
                employee.designation = emp.designation;
                employee.name = emp.name;

                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }

        // D = Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> RemoveEmployee([FromRoute] Guid id)
        {
            var emp = await _context.Employee.FindAsync(id);
            if(emp == null)
            {
                return NotFound();
            }
            _context.Employee.Remove(emp);
            await _context.SaveChangesAsync();
            return Ok("Employee Data Removed Successfully!!!");
        }
    }
}
