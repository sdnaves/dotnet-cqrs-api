using Microsoft.AspNetCore.Mvc;
using Sample.Application.Interfaces;
using Sample.Application.ViewModels;

namespace Sample.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> Get()
        {
            return await _customerService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<CustomerViewModel> Get(string id)
        {
            return await _customerService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerService.Create(model);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CustomerViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _customerService.Update(model);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _customerService.Delete(id);

            return Ok(result);
        }
    }
}
