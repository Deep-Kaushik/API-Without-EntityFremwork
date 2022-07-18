using FirstApplication.Models;
using FirstApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc;
//using System.Web.Mvc;

namespace FirstApplication.Controllers
{
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //private IEmployee _UnitOfWork;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeController> _logger;
        private string connString;
        private object _permissionService;


        public EmployeeController(ILogger<EmployeeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            //_UnitOfWork = UsersUnitOfWork;
        }


        [HttpGet]
        [Route("api/[controller]")]
        public IEnumerable<Employee> Get()
        {
            List<Employee> emp = new List<Employee>();

            try
            {
                string connString = ConfigurationExtensions.GetConnectionString(_configuration, "DeepanshuContext");
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Employee", conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];
                sda.Dispose();

                foreach (DataRow dr in dt.Rows)
                {
                    Employee m = new Employee
                    {
                        EmpId = (int)dr["EmpId"],
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        Password= dr["Password"].ToString(),
                        ContectNumber = dr["ContectNumber"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Contrey = dr["Contrey"].ToString()
                    };
                    emp.Add(m);
                }
                conn.Close();
                return emp.ToArray();
            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("api/[controller]/GetEmployeeById")]
        public IEnumerable<Employee> GetEmployeeById([FromQuery]int id)
        {
            List<Employee> emp = new List<Employee>();

            try
            {
                string connString = ConfigurationExtensions.GetConnectionString(_configuration, "DeepanshuContext");
                SqlConnection conn = new SqlConnection(connString);
                conn.Open();

                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Employee WHERE EmpId="+ id, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];
                sda.Dispose();

                foreach (DataRow dr in dt.Rows)
                {
                    Employee m = new Employee
                    {
                        EmpId = (int)dr["EmpId"],
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Email = dr["Email"].ToString(),
                        ContectNumber = dr["ContectNumber"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Contrey = dr["Contrey"].ToString()
                    };
                    emp.Add(m);
                }
                conn.Close();
                return emp.ToArray();
            }
            catch (Exception e)
            {

                throw;
            }
        }






        [HttpPost]
        [Route("api/[controller]/PostEmployee")]
        public Employee PostEmployee(Employee employee)
        {
            Console.WriteLine(employee.EmpId);
            string constr = ConfigurationExtensions.GetConnectionString(_configuration, "DeepanshuContext");
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertEmployee", con))

                    try
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                        cmd.Parameters.AddWithValue("@Email", employee.Email);
                        cmd.Parameters.AddWithValue("@Password", employee.Password);
                        cmd.Parameters.AddWithValue("@ContectNumber", employee.ContectNumber);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        cmd.Parameters.AddWithValue("@Contrey", employee.Contrey);
                        cmd.Connection = con;
                        con.Open();
                        employee.EmpId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    catch (Exception e)
                    {

                        throw;
                    }
                return employee;
            }

        }

    }

}


 