

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
public interface IBookableResourceRepository
{
    Task<BookableResource?> GetById(BookableResourceId id);

    Task Save(BookableResource bookableResource);

    Task Update(BookableResource bookableResource);
}
