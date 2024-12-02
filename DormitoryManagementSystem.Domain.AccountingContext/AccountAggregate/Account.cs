﻿using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;
using DormitoryManagementSystem.Domain.AccountingContext.DomainEvents;
using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.DomainEvents;
using DormitoryManagementSystem.Domain.Common.Entities;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System.Collections.Immutable;

namespace DormitoryManagementSystem.Domain.Common.Accounting;

public record AccountId(Guid Value) : EntityId<Guid>(Value)
{
    public static AccountId Next() => new AccountId(Guid.NewGuid());
}

public class Account : AggregateRoot<AccountId>
{
    public BankInformation BankInformation { get; private set; }
    public Administrator Administrator { get; private set; }
    public ImmutableList<Entry> Entries { get => entries.Entries; }

    private EntryList entries;
    public decimal? disposableAmountLowerLimit;

    public Account(AccountId id, BankInformation bankInformation, Administrator administrator) 
        : this(id, bankInformation, administrator, EntryList.NewEmpty()) { }

    private Account(AccountId id, BankInformation bankInformation, Administrator administrator, EntryList entries)
        : base(id)
    {
        BankInformation = bankInformation;
        Administrator = administrator;

        if (entries.HasCurrencyMismatch())
            throw new CurrencyMismatchException(
                "The entries has currency mismatch. All entries must have same currency.");

        this.entries = entries;
    }

    public void SetDispoableAmountLowerLimit(decimal limit)
    {
        disposableAmountLowerLimit = limit;
    }

    public void RemoveDispoableAmountLowerLimit()
    {
        disposableAmountLowerLimit = null;
    }

    public void RegisterDeposit(Money amount)
    {
        Deposit deposit = new(DepositId.Next(), amount);
        entries.Add(deposit);
    }

    public void RegisterWithdrawal(Money amount)
    {
        Money disposableBefore = GetDisposableAmount();
        Withdrawal withdrawal = new(WithdrawalId.Next(), amount);
        entries.Add(withdrawal);
        Money disposableAfter = GetDisposableAmount();
        RaiseIfDispoableAmountLowerLimitBreached(disposableBefore, disposableAfter);
    }

    public void RegisterDebit(Money amount)
    {
        Debit debit = new (DebitId.Next(), amount);
        entries.Add(debit);   
    }

    public void RegisterCredit(Money amount)
    {
        Money disposableBefore = GetDisposableAmount();
        Credit credit = new Credit(CreditId.Next(), amount);
        entries.Add(credit);
        Money disposableAfter = GetDisposableAmount();
        RaiseIfDispoableAmountLowerLimitBreached(disposableBefore, disposableAfter);
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
            .Sum();
    }

    public Money GetTotalCredit()
    {
        return entries.Entries
            .Where(e => e is Credit)
            .Select(e => e.Amount)
            .Sum();
    }

    public Money GetTotalDebit()
    {
        return entries.Entries
            .Where(e => e is Debit)
            .Select(e => e.Amount)
            .Sum();
    }

    private void RaiseIfDispoableAmountLowerLimitBreached(Money disposableBefore, Money disposableAfter)
    {
        if (disposableAmountLowerLimit is null)
            return;

        if (disposableBefore.Value >= disposableAmountLowerLimit && disposableAfter.Value < disposableAmountLowerLimit)
            Raise(new DisposableAmountLowerLimitBreachedEvent(
                Id,
                disposableAmountLowerLimit ?? 0,
                disposableAfter)
            );
    }
    
}
