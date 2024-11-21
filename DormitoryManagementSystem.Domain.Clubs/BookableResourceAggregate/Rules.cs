
using DormitoryManagementSystem.Domain.Common.ValueObjects;

namespace DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

public record Rules(string Information) : ValueObject;
public record MaxBookingsPerMemberRules(string Information, int MaxBookingsPerMember) : Rules(Information);



