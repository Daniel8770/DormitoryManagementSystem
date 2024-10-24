using DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.MinimumTransactionDebtSettlementAlgorithm;
public class RandomMinimumTransactionsDebtSettler : IMinimumTransactionDebtSettler
{
    public List<Participant> GetDebtorsOf(Participant creditor, List<Participant> participants)
    {
        int n = new Random().Next(1, participants.Count + 1);
        
        List<Participant> randomParticipants = participants
            .OrderBy(x => Guid.NewGuid()) // shuffles the list
            .Take(n)                      
            .ToList();

        return randomParticipants;
    }
}
