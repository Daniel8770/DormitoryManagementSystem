

using DormitoryManagementSystem.Domain.ClubsContext;
using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;

namespace TestDormitoryManagementStystem.UnitTests.Domain.Common;
public class EntitiesTest
{
    [Fact]
    public void Entities_ShouldBeComparableByIdentity()
    {
        MemberId id = MemberId.Next();

        Member unit1 = new (id);
        Member unit2 = new (id);
        Member unit3 = new (MemberId.Next());

        Assert.True(unit1 == unit2);
        Assert.True(unit1.Equals(unit2));
        Assert.False(unit1 == unit3);
        Assert.False(unit1.Equals(unit3));
        Assert.True(unit1 != unit3);
    }


}
