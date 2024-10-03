using DormitoryManagementSystem.Domain.AccountingContext.Accounting;
using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Application.AccountingContext;

public class AccountService
{
    private IAccountRepository accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        this.accountRepository = accountRepository;
    }

    public Money GetAccountBalance(AccountId id)
    {
        Account account = accountRepository.GetById(id) ??
            throw new Exception("Not found");

        return account.GetBalance();
    }
}
