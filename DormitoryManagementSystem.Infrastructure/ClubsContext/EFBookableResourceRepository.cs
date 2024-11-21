using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

namespace DormitoryManagementSystem.Infrastructure.ClubsContext;

public class EFBookableResourceRepository : IBookableResourceRepository
{
    private readonly ClubsDBContext dbContext;
    //private readonly BookableResourceFactory factory;

    public EFBookableResourceRepository(ClubsDBContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<BookableResource?> GetById(BookableResourceId id)
    {
        return await dbContext.BookableResources.FindAsync(id);
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


