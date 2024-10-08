using DormitoryManagementSystem.Domain.Common.MoneyModel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries;

public class EntryList
{
    private List<Entry> entries;
    public ImmutableList<Entry> Entries { get => entries.ToImmutableList(); }

    public static EntryList NewEmpty()
    {
        return new EntryList(new());
    }

    public EntryList(List<Entry> entries)
    {
        if (HasCurrencyMismatch(entries))
            throw new CurrencyMismatchException("Not all entries has the same currency.");

        this.entries = entries;
    }

    public void Add(Entry entry)
    {
        Entry? first = entries.FirstOrDefault();

        if (first is null || entry.Amount.Currency == first.Amount.Currency)
            entries.Add(entry);
        else
            throw new CurrencyMismatchException($"The amount of the entry you are " +
                $"adding doesn't match the same currency as the rest of the entries.");
    }

    public bool HasCurrencyMismatch()
    {
        return HasCurrencyMismatch(entries);
    }

    private bool HasCurrencyMismatch(List<Entry> entries)
    {
        return !entries.All(e => entries.First().Amount.Currency == e.Amount.Currency);
    }
}
