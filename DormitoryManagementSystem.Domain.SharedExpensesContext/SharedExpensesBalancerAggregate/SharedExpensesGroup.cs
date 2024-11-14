using DormitoryManagementSystem.Domain.Common.Aggregates;
using DormitoryManagementSystem.Domain.Common.Exceptions;
using DormitoryManagementSystem.Domain.Common.MoneyModel;
using DormitoryManagementSystem.Domain.SharedExpensesContext.MinimumTransactionDebtSettlementAlgorithm;

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

public class SharedExpensesGroup
{
    public SharedExpensesGroupId Id { get; init; }
    public Currency Currency { get; private set; }

    private List<Participant> participants;
    private List<Expense> expenses = new();
    private List<Debt> debts = new();
    private IMinimumTransactionDebtSettler debtSettler;

    public static SharedExpensesGroup CreateNew(
        Currency currency,
        List<Participant> participants,
        IMinimumTransactionDebtSettler debtSettler)
    {
        return new SharedExpensesGroup(
            SharedExpensesGroupId.Next(),
            currency,
            participants,
            debtSettler);
    }

    private SharedExpensesGroup(
        SharedExpensesGroupId id,
        Currency currency,
        List<Participant> participants,
        IMinimumTransactionDebtSettler debtSettler)
    {
        Id = id;
        Currency = currency;
        this.participants = participants;
        this.debtSettler = debtSettler;
    }

    public void RecordExpense(Money amount, Participant creditor, List<Participant> debtors)
    {
        AssertCorrectness(amount, creditor, debtors);

        Expense newExpense = new(
            Guid.NewGuid(),
            amount,
            creditor,
            debtors);

        expenses.Add(newExpense);

        RecordDebtsOf(newExpense);
    }

    private void AssertCorrectness(Money amount, Participant creditor, List<Participant> debtors)
    {
        if (amount.Currency != Currency)
            throw new DomainException($"The expense currency does not match the currency of the kitchen balance {Id}.");

        AssertParticipation(creditor);

        IEnumerable<Participant> debtorsNotPartOfSharedExpense = debtors.Where(d => !participants.Contains(d));
        if (debtorsNotPartOfSharedExpense.Any())
            throw new DomainException($"The following of the debtors are not part of this shared expense ({Id}): "
                + string.Join(", ", debtorsNotPartOfSharedExpense.Select(d => d.Id)));
    }



    private void RecordDebtsOf(Expense expense)
    {
        Money pricePerDebtor = expense.Amount.DivideBy(expense.Debtors.Count);

        foreach (var debtor in expense.Debtors)
        {
            debts.Add(new(Guid.NewGuid(), expense.Id, debtor, pricePerDebtor));
        }
    }

    public Money GetSharedExpenses()
    {
        return expenses.Select(p => p.Amount).Aggregate((a, b) => a + b);
    }

    public Money GetTotalExpenseOf(Participant participant)
    {
        AssertParticipation(participant);
        return expenses.Where(e => e.Creditor == participant)
            .Select(e => e.Amount)
            .Aggregate((a, b) => a + b);
    }

    public Money GetTotalDebtOf(Participant participant)
    {
        AssertParticipation(participant);
        return debts.Where(d => d.Debtor == participant)
            .Select(e => e.Amount)
            .Aggregate((a, b) => a + b);
    }

    public Money GetBalanceOf(Participant participant)
    {
        AssertParticipation(participant);
        Money totalExpense = GetTotalExpenseOf(participant);
        Money totalDebt = GetTotalDebtOf(participant);
        return totalExpense - totalDebt;
    }

    public List<Participant> GetDebtorsOf(Participant creditor)
    {
        //AssertParticipation(creditor);
        //return debtSettler.GetDebtorsOf(creditor);
        throw new NotImplementedException();    // TODO
    }

    private void AssertParticipation(Participant creditor)
    {
        if (!participants.Contains(creditor))
            throw new DomainException($"The creditor {creditor.Id} is not part of this shared expense {Id}.");
    }
}
