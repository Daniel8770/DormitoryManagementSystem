using DormitoryManagementSystem.Domain.AccountingContext.Accounting;
using DormitoryManagementSystem.Domain.AccountingContext.Accounting.Entries;
using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System.Collections.Immutable;

namespace DormitoryManagementSystem.Domain.Common.Accounting;

public class Account : AggregateRoot
{
    public AccountId Id { get; init; }
    public BankInformation BankInformation { get; private set; }
    public Administrator Administrator { get; private set; }
    public ImmutableList<Entry> Entries { get => entries.Entries; }

    private EntryList entries;

    public Account(AccountId id, BankInformation bankInformation, Administrator administrator) 
        : this(id, bankInformation, administrator, new EntryList())
    { }

    public Account(AccountId id, BankInformation bankInformation, Administrator administrator, EntryList entries)
    {
        Id = id;
        BankInformation = bankInformation;
        Administrator = administrator;

        if (entries.HasCurrencyMismatch())
            throw new CurrencyMismatchException("The entries has currency mismatch. All entries must have same currency.");

        this.entries = entries;
    }

    public void RegisterDeposit(Money amount)
    {
        Deposit deposit = new(
            new DepositId(Guid.NewGuid()), 
            amount);

        entries.Add(deposit);
    }

    public void RegisterWithdrawal(Money amount)
    {
        Withdrawal withdrawal = new(
            new WithdrawalId(Guid.NewGuid()),
            amount);

        entries.Add(withdrawal);
    }

    public Money GetBalance()
    {
        return entries.Entries
            .Select(e => e.GetRelativeAmount())
            .Aggregate((m1, m2) => m1 + m2);
    }

    public Money GetDisposableAmount()
    {
        throw new NotImplementedException();
    }

    public Money GetEquity()
    {
        throw new NotImplementedException();
    }

    public Money GetAccountReceivables()
    {
        throw new NotImplementedException();
    }

    public Money GetBeneficiariesReceivables()
    {
        throw new NotImplementedException();
    }

}
