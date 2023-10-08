using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Contracts;
using OZK.Domain.CQRS.Models;
using OZK.Models;
using OZKService.Contracts;
using OZKService.Models;
using System;
using System.Threading.Tasks;

namespace OZK.Controllers
{
    [Route("api/transfer")]
    [ApiController]
    public class OZKTransferController : ControllerBase
    {

        private readonly ITransfer<OZKBankAccountInfo> _transferService;
        public OZKTransferController(ITransfer<OZKBankAccountInfo> transferService)
        {
            _transferService = transferService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(OZKTransfer updateAccount)
        {
            if (!ModelState.IsValid && updateAccount.TransferAmount > 0) return BadRequest();

            return Ok(await _transferService.Transfer(updateAccount));


        }




    }
}
