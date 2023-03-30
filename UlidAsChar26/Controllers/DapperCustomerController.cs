using System.Data;
using UlidAsChar26.Ef;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace UlidAsChar26.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DapperCustomerController : ControllerBase
    {

        private readonly ILogger<DapperCustomerController> _logger;

        public DapperCustomerController(ILogger<DapperCustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Customer>>> List()
        {
            string connstring = "Server=localhost;Database=ULID_AS_CHAR26;User Id=postgres;Password=adminpass;Application Name=CobaUlid;";
            using (var conn = OpenConnection(connstring))
            {
                var ssql = @"SELECT * FROM ""Customers"" ";
                return conn.Query<Customer>(ssql).ToList();
            }
        }

        private static IDbConnection OpenConnection(string connStr)
        {
            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;
        }

    }
}