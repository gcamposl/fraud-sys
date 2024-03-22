using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class AccountController
    {
        public AccountController()
        {

        }
        [HttpGet]
        public void GetAllAccounts()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public void GetAccountByCpf(string cpf)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public void PostAccount()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public void PutAccount()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public void DeleteAccountByCpf()
        {
            throw new NotImplementedException();
        }
    }
}