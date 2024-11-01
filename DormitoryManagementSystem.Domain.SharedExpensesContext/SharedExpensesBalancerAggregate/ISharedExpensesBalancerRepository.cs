

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
public interface ISharedExpensesBalancerRepository
{

    Task<SharedExpensesGroup?> GetById(SharedExpensesGroupId id);
    Task Update(SharedExpensesGroup sharedExpensesBalancer);
    Task Save(SharedExpensesGroup sharedExpensesBalancer);

}
