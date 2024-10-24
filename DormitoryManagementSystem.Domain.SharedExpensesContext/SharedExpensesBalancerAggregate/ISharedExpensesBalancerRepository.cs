

namespace DormitoryManagementSystem.Domain.SharedExpensesContext.SharedExpensesBalancerAggregate;
public interface ISharedExpensesBalancerRepository
{

    Task<SharedExpensesBalancer?> GetById(SharedExpensesBalancerId id);
    Task Update(SharedExpensesBalancer sharedExpensesBalancer);
    Task Save(SharedExpensesBalancer sharedExpensesBalancer);

}
