using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OZKService.Models;
using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Contracts;
using OZK.Domain.CQRS.Models;
using OZK.Domain.CQRS.Projections;
using System;
using System.Threading.Tasks;


namespace OZKBankUserService.Controllers
{
    [Route("api/user")]
    [ApiController]

    public class OZKBankUserController : ControllerBase
    {
        private readonly IService<BankUser, DefaultSearch<OZKBankUserSearchResult>> _bankuserService;
        public OZKBankUserController(IService<BankUser, DefaultSearch<OZKBankUserSearchResult>> orgService)
        {
            _bankuserService = orgService;
        }


        [HttpGet("{userid}")]
    
        public async Task<IActionResult> Get(Guid userId)
        {
            return Ok(await _bankuserService.Get(OZKBankUserProjection.OZKBankAccountInformation, userId));
        }

        [HttpGet()]

        public async Task<IActionResult> Get()
        {
            return Ok(await _bankuserService.Get(OZKBankUserProjection.OZKBankAccountInformation, HttpContext.RequestAborted));
        }

        [HttpPost("query")]
   
        public async Task<IActionResult> Query(DefaultSearch<OZKBankUserSearchResult> search)
        {
            return Ok(await _bankuserService.Query(search, HttpContext.RequestAborted));
        }

        [HttpPost]
 
        public async Task<IActionResult> Post(OZKBankUserInfo NewBankUser)
        {
            if (!ModelState.IsValid) return BadRequest();

            var organization = new BankUser()
            {
                Id = NewBankUser.Id,
                Firstname = NewBankUser.Firstname,
                Lastname = NewBankUser.Lastname,    
                Address_Address1 = NewBankUser.Address_Address1,
                Address_Address2 = NewBankUser.Address_Address2,
                Address_City = NewBankUser.Address_City, 
                Address_State= NewBankUser.Address_State,
                Address_Zip = NewBankUser.Address_Zip,
                CreatedBy = "system",
                CreatedDate = DateTime.UtcNow
            };

            return Ok(await _bankuserService.Add(organization, HttpContext.RequestAborted));
        }

        [HttpPut]
    
        public async Task<IActionResult> Put(OZKBankUserInfo updateBankUser)
        {
            if (!ModelState.IsValid && updateBankUser.Id == Guid.Empty) return BadRequest();

            var ozkBankuser = new BankUser();

            ozkBankuser.Id = updateBankUser.Id;
            ozkBankuser.Firstname = updateBankUser.Firstname;
            ozkBankuser.Lastname = updateBankUser.Lastname;
            ozkBankuser.Address_Address1 = updateBankUser.Address_Address1;
            ozkBankuser.Address_Address2 = updateBankUser.Address_Address2;
            ozkBankuser.Address_City = updateBankUser.Address_City;
            ozkBankuser.Address_State = updateBankUser.Address_State;
            ozkBankuser.Address_Zip = updateBankUser.Address_Zip;


            ozkBankuser.ModifiedBy = "system";
            ozkBankuser.ModifiedDate = DateTime.UtcNow;

            return Ok(await _bankuserService.Update(ozkBankuser, HttpContext.RequestAborted));
        }

        [HttpDelete]
    
        public async Task<IActionResult> Delete(Guid userId)
        {
            if (userId == Guid.Empty) return BadRequest();

            return Ok(await _bankuserService.Delete(new BankUser { Id = userId, DeletedBy = "system", DeletedDate = DateTime.UtcNow }, HttpContext.RequestAborted));
        }
    }
}

