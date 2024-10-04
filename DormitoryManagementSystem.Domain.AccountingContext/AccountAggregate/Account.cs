using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows.Obligations;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows.Transactions;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Obligations;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows.Transactions;
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
    private List<Debit> debits;
    private List<Credit> credits;

    public Account(AccountId id, BankInformation bankInformation, Administrator administrator) 
        : this(id, bankInformation, administrator, new EntryList(), new List<Debit>(), new List<Credit>())
    { }

    public Account(AccountId id, BankInformation bankInformation, Administrator administrator, EntryList entries)
        : this(id, bankInformation, administrator, entries, new List<Debit>(), new List<Credit>())
    { }

    public Account(AccountId id, BankInformation bankInformation, Administrator administrator, List<Debit> debits)
        : this(id, bankInformation, administrator, new EntryList(), debits, new List<Credit>())
    { }

    public Account(AccountId id, BankInformation bankInformation, Administrator administrator, List<Credit> credits)
        : this(id, bankInformation, administrator, new EntryList(), new List<Debit>(), credits)
    { }

    public Account(AccountId id,
        BankInformation bankInformation,
        Administrator administrator,
        EntryList entries,
        List<Debit> debits,
        List<Credit> credits)
    {
        Id = id;
        BankInformation = bankInformation;
        Administrator = administrator;

        if (entries.HasCurrencyMismatch())
            throw new CurrencyMismatchException("The entries has currency mismatch. All entries must have same currency.");

        this.entries = entries;
        // TODO: what about currency check for these guys
        this.debits = debits;
        this.credits = credits;
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

    public void RegisterDebit(Money amount)
    {
        Debit debit = new (DebitId.Next(), amount);
        debits.Add(debit);   
    }

    public void RegisterCredit(Money amount)
    {
        Credit credit = new Credit(CreditId.Next(), amount);
        credits.Add(credit);
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
            .Select(e => e.GetRelativeAmount())
            .Aggregate((m1, m2) => m1 + m2);
    }

    public Money GetTotalCredit()
    {
        return credits
            .Select(e => e.Amount)
            .Aggregate((m1, m2) => m1 + m2);
    }

    public Money GetTotalDebit()
    {
        return debits
            .Select(e => e.Amount)
            .Aggregate((m1, m2) => m1 + m2);
    }

}
