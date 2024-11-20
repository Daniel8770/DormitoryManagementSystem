using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext;

public class EFBookableResourceRepository : IBookableResourceRepository
{
    private readonly ClubsDBContext dbContext;

    public EFBookableResourceRepository(ClubsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<BookableResource> GetById(BookableResourceId id)
    {
        throw new NotImplementedException();
    }

    public Task Save(BookableResource bookableResource)
    {
        throw new NotImplementedException();
    }

    public Task Update(BookableResource bookableResource)
    {
        throw new NotImplementedException();
    }
}
