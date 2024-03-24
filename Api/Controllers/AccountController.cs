using Domain.DTOs;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region private variables
        private readonly IAccountService _accountService;
        #endregion
        #region constructor
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion
        #region public methods
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
            var result = await _accountService.GetAllAsync();
            if (result.IsSucces)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{cpf}")]
        public async Task<ActionResult> GetAccountByCpf(string cpf)
        {
            var result = await _accountService.GetByCpfAsync(cpf);
            if (result.IsSucces)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<ActionResult> PutAccount([FromBody] AccountDTO accountDTO)
        {
            var result = await _accountService.UpdateAsync(accountDTO);
            if (result.IsSucces)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{cpf}")]
        public async Task<ActionResult> DeleteAccountByCpf(string cpf)
        {
            var result = await _accountService.DeleteAsync(cpf);
            if (result.IsSucces)
                return Ok(result);

            return BadRequest(result);
        }
        #endregion
    }
}