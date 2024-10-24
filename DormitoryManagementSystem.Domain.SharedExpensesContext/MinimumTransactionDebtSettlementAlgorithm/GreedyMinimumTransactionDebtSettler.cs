

using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.MinimumTransactionDebtSettlementAlgorithm;
public class GreedyMinimumTransactionDebtSettler : IMinimumTransactionDebtSettler
{
    public List<Participant> GetDebtorsOf(Participant creditor, List<Participant> participants)
    {
        throw new NotImplementedException();
    }
}
