using DormitoryManagementSystem.Domain.Common.Accounting;
using DormitoryManagementSystem.Domain.Common.MoneyModel;

namespace DormitoryManagementSystem.Application.AccountingContext;
public interface IAccountService
{
    Money GetAccountBalance(AccountId id);
    void RegisterCreditOnAccount(AccountId id, Money amount);
    void RegisterDebitOnAccount(AccountId id, Money amount);
    void RegisterDepositOnAccount(AccountId id, Money amount);
    void RegisterWithdrawalOnAccount(AccountId id, Money amount);
}