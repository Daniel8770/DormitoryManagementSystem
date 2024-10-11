using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountRules;
public class DisposableAmountNoLessThanAccountRule : IAccountRule
{
    private decimal limit;

    public DisposableAmountNoLessThanAccountRule(decimal limit)
    {
        this.limit = limit;
    }

    public bool isSatisfiedBy(Account account)
    {
        return account.GetDisposableAmount().Value >= limit;
    }
}
