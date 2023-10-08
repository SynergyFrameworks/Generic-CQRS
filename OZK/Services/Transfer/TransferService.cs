using OZK.Datalayer.Domain;
using OZK.Domain.CQRS.Contracts;
using OZK.Domain.CQRS.Models;
using OZK.Domain.CQRS.Projections;
using OZK.Models;
using OZKService.Contracts;
using OZKService.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OZK.Services
{
    public class TransferService : ITransfer<OZKBankAccountInfo>
    {


        private readonly IService<BankAccount, DefaultSearch<OZKBankAccountSearchResult>> _accountService;
        public TransferService(IService<BankAccount, DefaultSearch<OZKBankAccountSearchResult>> accountService)
        {

            _accountService = accountService;
        }

        public async Task<BankAccount> Transfer(OZKTransfer model, CancellationToken cancellationToken = default)
        {

            BankAccount sourceBankAccount = await _accountService.Get(OZKBankAccountProjection.OZKBankAccountInformation, model.SourceAccountId);
            BankAccount destinationAccount = await _accountService.Get(OZKBankAccountProjection.OZKBankAccountInformation, model.DestinationAccountId);

            if (sourceBankAccount == null) throw new ArgumentException("No Source Acccount Exists");
            if (sourceBankAccount.Amount == 0) throw new ArgumentException("You have no money in you account");
            if (sourceBankAccount.Enbled == false) throw new ArgumentException("Your source Bank Account is disabled");
            if (destinationAccount.Enbled == false) throw new ArgumentException("Your destination Bank Account is disabled");
            if (sourceBankAccount.Amount < model.TransferAmount) throw new ArgumentException($"You don't have enough money in your account, your ballance is {sourceBankAccount.Amount}");

            if (sourceBankAccount.BankUserID == model.BankUserId 
                && destinationAccount.BankUserID == model.BankUserId 
                && sourceBankAccount.Id != model.DestinationAccountId)
            {

                BankAccount sourceBankAccountUpdate = new()
                {
                    Id = sourceBankAccount.Id,
                    BankUserID = sourceBankAccount.BankUserID,
                    AccountType = sourceBankAccount.AccountType,
                    Amount = sourceBankAccount.Amount - model.TransferAmount,
                    Enbled = true,
                    ModifiedBy = "system",
                    ModifiedDate = DateTime.UtcNow,

                };

                await _accountService.Update(sourceBankAccountUpdate, cancellationToken);

                BankAccount destinationBankAccountUpdate = new()
                {
                    Id = destinationAccount.Id,
                    BankUserID = destinationAccount.BankUserID,
                    AccountType = destinationAccount.AccountType,
                    Amount = destinationAccount.Amount + model.TransferAmount,
                    Enbled = true,
                    ModifiedBy = "system",
                    ModifiedDate = DateTime.UtcNow,

                };

                await _accountService.Update(destinationBankAccountUpdate, cancellationToken);


            }
            else
            {


                throw new ArgumentException("The Money cannot be transferred, please check you parameters", nameof(model));


            }


            BankAccount DestinationBankAccount = await _accountService.Get(OZKBankAccountProjection.OZKBankAccountInformation, model.DestinationAccountId);

            return DestinationBankAccount;

        }





    }
}
