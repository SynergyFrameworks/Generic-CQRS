using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OZKService.Models;
using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Contracts;
using OZK.Domain.CQRS.Models;
using OZK.Domain.CQRS.Projections;
using System;
using System.Threading.Tasks;


namespace OZKBankAccountService.Controllers
{
    [Route("api/account")]
    [ApiController]
  
    public class OZKBankAccountController : ControllerBase
    {
        private readonly IService<BankAccount, DefaultSearch<OZKBankAccountSearchResult>> _accountService;
        public OZKBankAccountController(IService<BankAccount, DefaultSearch<OZKBankAccountSearchResult>> accountService)
        {
            _accountService = accountService;
        }


        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(Guid accountId)
        {
            return Ok(await _accountService.Get(OZKBankAccountProjection.OZKBankAccountInformation, accountId));
        }

        [HttpGet()]
     
        public async Task<IActionResult> Get()
        {
            return Ok(await _accountService.Get(OZKBankAccountProjection.OZKBankAccountInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
    
        public async Task<IActionResult> Query(DefaultSearch<OZKBankAccountSearchResult> search)
        {
            return Ok(await _accountService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]

        public async Task<IActionResult> Post(OZKBankAccountInfo newAccount)
        {
            if (!ModelState.IsValid) return BadRequest();

            var Account = new BankAccount()
            {
               Id = newAccount.Id,
               BankUserID = newAccount.BankUserID,    
               AccountType = newAccount.AccountType,
               Amount = newAccount.Amount,
               Enbled = newAccount.Enbled,  
               CreatedBy = "system",
               CreatedDate = DateTime.UtcNow
            };

            return Ok(await _accountService.Add(Account, HttpContext.RequestAborted));
        }

        [HttpPut]
     
        public async Task<IActionResult> Put(OZKBankAccountInfo updateAccount)
        {
            if (!ModelState.IsValid && updateAccount.Id == Guid.Empty) return BadRequest();

            BankAccount modifiedClient = new BankAccount
            {
                Id = updateAccount.Id,
                BankUserID = updateAccount.BankUserID,
                AccountType = updateAccount.AccountType,
                Amount = updateAccount.Amount,
                Enbled = updateAccount.Enbled,
                ModifiedBy = "system",
                ModifiedDate = DateTime.UtcNow,
            };

            return Ok(await _accountService.Update(modifiedClient, HttpContext.RequestAborted));
        }

        [HttpDelete]
        [Route("{clientId}")]
   
        public async Task<IActionResult> Delete(Guid clientId)
        {
            if (clientId == Guid.Empty) return BadRequest();
            return Ok(await _accountService.Delete(new BankAccount { Id = clientId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}
