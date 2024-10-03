using DormitoryManagementSystem.Domain.Common.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.Accounting;

public interface IAccountRepository
{
    void Save(Account account);

    Account? GetById(AccountId id);

}
