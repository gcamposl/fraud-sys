using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
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
            if (result.IsSuccess)
                return Created("Conta criada! ", result);

            return BadRequest(accountDTO);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAccounts()
        {
            Console.WriteLine("kadklasd");
            var result = await _accountService.GetAllAsync();
            if (result.IsSuccess)
                return Ok(result);

            return Ok(result);
        }

        [HttpGet]
        [Route("{cpf}")]
        public async Task<ActionResult> GetAccountByCpf(string cpf)
        {
            var result = await _accountService.GetByCpfAsync(cpf);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> PutAccount([FromBody] AccountDTO accountDTO)
        {
            var result = await _accountService.UpdateAsync(accountDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAccountByCpf([FromQuery] string cpf, [FromQuery] int accountNumber)
        {

            var result = await _accountService.DeleteAsync(cpf, accountNumber);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}