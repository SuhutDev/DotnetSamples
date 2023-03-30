using System.Data;
using UlidAsGuid.Dto;
using UlidAsGuid.Ef;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace UlidAsGuid.Controllers
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
        public async Task<ActionResult<List<CustomerVm>>> List()
        {
            string connstring = "Server=localhost;Database=ULID_AS_GUID;User Id=postgres;Password=1234;Application Name=CobaUlid;";
            using (var conn = OpenConnection(connstring))
            {
                var ssql = @"SELECT * FROM ""Customers"" ";
                return conn.Query<CustomerVm>(ssql).ToList();
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