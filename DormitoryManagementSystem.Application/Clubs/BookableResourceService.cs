using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

namespace DormitoryManagementSystem.Application.Clubs;
public class BookableResourceService : IBookableResourceService
{
    private IBookableResourceRepository repository;

    public BookableResourceService(IBookableResourceRepository repository)
    {
        this.repository = repository;
    }

    public async Task<BookableResource?> GetBookableResourceById(BookableResourceId id)
    {
        return await repository.GetById(id);
    }

    public async Task<BookableResource> CreateNewBookableResource(string name, string rules, DateTime openDate, DateTime endDate)
    {
        BookableResource newResource = BookableResource.CreateNew(name, rules, openDate, endDate);
        await repository.Save(newResource);
        return newResource;
    }

    public async Task<BookableResource?> AddUnit(BookableResourceId id, string unitName)
    {
        BookableResource? resource = await repository.GetById(id);

        if (resource is null)
            return null;

        resource.AddUnit(unitName);
        await repository.Update(resource);

        return resource;
    }

    public async Task<BookableResource?> BookDays(BookableResourceId id, MemberId memberId, UnitId unitId, DateTime date, int days)
    {
        // TODO: should check if the member is allowed to book the resource

        BookableResource? resource = await repository.GetById(id);

        if (resource is null)
            return null;

        resource.BookDays(memberId, unitId, date, days);
        await repository.Update(resource);

        return resource;
    }
}
