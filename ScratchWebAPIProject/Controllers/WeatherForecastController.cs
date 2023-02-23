using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ScratchWebAPIProject.Context;
using ScratchWebAPIProject.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScratchWebAPIProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _config;
        private readonly EmployeeDBContext _empDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, EmployeeDBContext empDbcontext)
        {
            _logger = logger;
            _config = config;
            _empDbContext = empDbcontext;
        }


        [HttpGet]
        [Route("get-Emps")]
        public async Task<ActionResult> GetEmployees()
        {
            var tblemp = new Emp()
            {
                Empno = 102,
                Ename = "Sample 102",
                Job = "Software"
            };

            _empDbContext.Add(tblemp);
            await _empDbContext.SaveChangesAsync();

            var emplist1 = await _empDbContext.Emps.Where(x => x.Empno == 10).FirstOrDefaultAsync();

            var emplist = await _empDbContext.Emps.ToListAsync();
            var emp = emplist.AsQueryable().ToList();

            return Ok("list of employees");
        }
        [HttpGet]
        [Route("Get-DeptDetails")]
        public async Task<IActionResult> GetDeptDetails()
        {
            //var result = (from d in _empDbContext.Depts
            //              join e in _empDbContext.Emps on d.Deptno equals e.Deptno
            //              group e by d into deptGroup
            //              select new
            //              {
            //                  Department = deptGroup.Key,
            //                  TotalSalary = deptGroup.Sum(e => e.Sal)
            //              })
            //  .OrderByDescending(d => d.TotalSalary)
            //  .FirstOrDefault();
            //return Ok(result);


var result = from e in _empDbContext.Emps
             join d in _empDbContext.Depts on e.Deptno equals d.Deptno into ed
             from d in ed.DefaultIfEmpty()
             orderby e.Empno
             select new { e.Empno, e.Ename, d.Dname };
            return Ok(result);

        }


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
