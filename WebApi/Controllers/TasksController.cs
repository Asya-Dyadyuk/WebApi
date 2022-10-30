using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApi.Models;
using EmployeeManagementSystem.Entities.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TasksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"
                    select * from dbo.Tasks";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();

                }
            }

            return new JsonResult(table);
        }
      /*  [HttpGet]
        public List<TasksController> GetTasks()
        {
            var tasksList = (from t in Tasks
                             select new TasksController
                             {
                                 ID = t.ID,
                                 IDEmployee = t.IDEmployee,
                                 AssignDate = t.AssignDate,
                                 Task = t.Task
                             }).ToList();
            return tasksList;
        }*/



        [HttpPost]

        public JsonResult Post(Tasks t)
        {
            string query = @"
                    insert into dbo.Tasks (Name, Task,DueDate)
                    values ('" + t.Name + "'" + "," + "'" + t.Task + "'" + "," + "'" + t.DueDate + @"')
                     ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();

                }
            }
            return new JsonResult("Added Successfully");
        }
    }
}
