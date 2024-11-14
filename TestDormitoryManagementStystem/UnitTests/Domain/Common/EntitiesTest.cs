

using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

namespace TestDormitoryManagementStystem.UnitTests.Domain.Common;
public class EntitiesTest
{
    [Fact]
    public void Entities_ShouldBeComparableByIdentity()
    {
        UnitId id = new(1);

        Unit unit1 = new (id, "Unit 1");
        Unit unit2 = new(id, "Unit 2");
        Unit unit3 = new(new(2), "Unit 3");

        Assert.True(unit1 == unit2);
        Assert.True(unit1.Equals(unit2));
        Assert.False(unit1 == unit3);
        Assert.False(unit1.Equals(unit3));
    }


}
