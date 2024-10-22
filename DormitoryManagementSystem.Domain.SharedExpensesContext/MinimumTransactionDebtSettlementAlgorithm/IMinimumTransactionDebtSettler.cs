using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.MinimumTransactionDebtSettlementAlgorithm;
internal interface IMinimumTransactionDebtSettler
{
    List<Participant> GetDebtorsOf(Participant creditor, List<Participant> participants);
}
