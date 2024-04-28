using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.Interfaces;
using Sample.Application.ViewModels;
using Sample.Domain.Core.Models;

namespace Sample.Services.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IEnumerable<AccountViewModel>> Get()
        {
            return await _accountService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<AccountViewModel> Get(string id)
        {
            return await _accountService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Create(model);

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] AccountViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.Update(model);

            return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _accountService.Delete(id);

            return Ok(result);
        }

        [HttpGet("history/{id}")]
        [Authorize]
        public async Task<IList<HistoryData<AccountViewModel>>> History(string id)
        {
            return await _accountService.GetAllHistory(id);
        }
    }
}
