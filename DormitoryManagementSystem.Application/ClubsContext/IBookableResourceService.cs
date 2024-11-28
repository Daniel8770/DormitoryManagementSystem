using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

namespace DormitoryManagementSystem.Application.Clubs;
public interface IBookableResourceService
{
    Task<BookableResource?> AddUnit(BookableResourceId id, string unitName);
    Task<BookableResource?> BookDays(BookableResourceId id, MemberId memberId, UnitId unitId, DateTime date, int days);
    Task<BookableResource> CreateNewBookableResource(string name, string rules, DateTime openDate, DateTime endDate);
    Task<BookableResource?> GetBookableResourceById(BookableResourceId id);
}