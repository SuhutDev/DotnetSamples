using System.ComponentModel;
using DatetimeOffsetApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatetimeOffsetApi.Controllers
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
        public async Task<ActionResult<Guid>> Add(AddCustomerReq request)
        {
            var myContext = new MyContext();

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CustomerName = request.CustomerName,
                JoinDate = request.JoinDate
            };
            myContext.Customers.Add(customer);
            await myContext.SaveChangesAsync();

            return customer.Id;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Guid>> Update(UpdateCustomerReq request)
        {
            var myContext = new MyContext();
            var customer = await myContext.Customers.FindAsync(request.Id);
            customer.CustomerName = request.CustomerName;
            customer.JoinDate = request.JoinDate;

            myContext.Customers.Update(customer);
            await myContext.SaveChangesAsync();

            return customer.Id;
        }


        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Customer>> GetId(Guid id)
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