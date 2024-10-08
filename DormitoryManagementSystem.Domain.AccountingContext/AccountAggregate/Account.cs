using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
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
        : this(id, bankInformation, administrator, EntryList.NewEmpty()) { }

    private Account(AccountId id, BankInformation bankInformation, Administrator administrator, EntryList entries)
    {
        Id = id;
        BankInformation = bankInformation;
        Administrator = administrator;

        if (entries.HasCurrencyMismatch())
            throw new CurrencyMismatchException(
                "The entries has currency mismatch. All entries must have same currency.");

        this.entries = entries;
    }

    public void RegisterDeposit(Money amount)
    {
        Deposit deposit = new(DepositId.Next(), amount);
        entries.Add(deposit);
    }

    public void RegisterWithdrawal(Money amount)
    {
        Withdrawal withdrawal = new(WithdrawalId.Next(), amount);
        entries.Add(withdrawal);
    }

    public void RegisterDebit(Money amount)
    {
        Debit debit = new (DebitId.Next(), amount);
        entries.Add(debit);   
    }

    public void RegisterCredit(Money amount)
    {
        Credit credit = new Credit(CreditId.Next(), amount);
        entries.Add(credit);
    }

    public Money GetDisposableAmount()
    {
        return GetBalance() - GetTotalCredit();
    }

    public Money GetEquity()
    {
        return GetBalance() + GetTotalDebit() - GetTotalCredit(); 
    }

    public Money GetBalance()
    {
        return entries.Entries
            .Where(e => e is Deposit || e is Withdrawal)
            .Select(e => e.GetRelativeAmount())
            .Aggregate((acc, m) => acc + m);
    }

    public Money GetTotalCredit()
    {
        return entries.Entries
            .Where(e => e is Credit)
            .Select(e => e.Amount)
            .Aggregate((m1, m2) => m1 + m2);
    }

    public Money GetTotalDebit()
    {
        return entries.Entries
            .Where(e => e is Debit)
            .Select(e => e.Amount)
            .Aggregate((m1, m2) => m1 + m2);
    }
}
