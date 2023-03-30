using System.ComponentModel;
using UlidAsGuid.Binder;
using UlidAsGuid.Dto;
using UlidAsGuid.Ef;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UlidAsGuid.Binder;

namespace UlidAsGuid.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EfCustomerController : ControllerBase
    {

        private readonly ILogger<EfCustomerController> _logger;

        public EfCustomerController(ILogger<EfCustomerController> logger)
        {
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Ulid>> Add(AddCustomerReq request)
        {
            var myContext = new MyContext();

            var customer = new Customer
            {
                Id = Ulid.NewUlid(),
                CustomerName = request.CustomerName
            };
            myContext.Customers.Add(customer);
            await myContext.SaveChangesAsync();

            return customer.Id;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Ulid>> Update(UpdateCustomerReq request)
        {
            var myContext = new MyContext();
            var customer = await myContext.Customers.FindAsync(request.Id);
            customer.CustomerName = request.CustomerName;

            myContext.Customers.Update(customer);
            await myContext.SaveChangesAsync();

            return customer.Id;
        }


        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Customer>> GetId(Ulid id)
        {
            var myContext = new MyContext();

            return await myContext.Customers.FindAsync(id);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Customer>>> List()
        {
            var myContext = new MyContext();

            return await myContext.Customers.ToListAsync();
        }

    }
}