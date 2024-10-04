using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Inflows;
using DormitoryManagementSystem.Domain.AccountingContext.AccountAggregate.Entries.Outflows;
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

    public EntryList()
    {
        entries = new List<Entry>();
    }

    public EntryList(List<Entry> entries)
    {
        this.entries = entries;
    }

    public void Add(Entry entry)
    {
        if (entry.Amount.Currency != entries.First().Amount.Currency)
            throw new CurrencyMismatchException($"The amount of the entry you are " +
                $"adding doesn't match the same currency as the rest of the entries.");

        entries.Add(entry);
    }

    public bool HasCurrencyMismatch()
    {
        return !entries.All(e => entries.First().Amount.Currency == e.Amount.Currency);
    }
}
