using DormitoryManagementSystem.Domain.Common.Accounting;


namespace DormitoryManagementSystem.Domain.AccountingContext.AccountRules;
public interface IAccountRule
{
    bool isSatisfiedBy(Account account);    
}
