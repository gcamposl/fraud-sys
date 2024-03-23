using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult> PostAccount([FromBody] AccountDTO accountDTO)
        {
            var result = await _accountService.CreateAsync(accountDTO);
            if (result.IsSucces)
                return Ok(accountDTO);
            return BadRequest(accountDTO);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult> GetAccountByCpf(string cpf)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult> PutAccount()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAccountByCpf()
        {
            throw new NotImplementedException();
        }
    }
}