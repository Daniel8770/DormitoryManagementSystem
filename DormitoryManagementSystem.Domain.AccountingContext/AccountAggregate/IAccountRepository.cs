using DormitoryManagementSystem.Domain.Common.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;

public interface IAccountRepository
{
    void SaveOrUpdate(Account account);

    Account? GetById(AccountId id);

}
