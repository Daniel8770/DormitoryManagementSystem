using DormitoryManagementSystem.Domain.Clubs.BookableResourceAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDormitoryManagementStystem.UnitTests.Domain.Common;
public class ValueObjectsTest
{
    [Fact]
    public void ValueObjects_ShouldBeComparableByAttributes()
    {
        MaxBookingsPerMemberRules rules1 = new("Information", 3);
        MaxBookingsPerMemberRules rules2 = new("Information", 3);
        MaxBookingsPerMemberRules rules3 = new("Information", 4);
        Assert.True(rules1 == rules2);
        Assert.False(rules1 == rules3);
        Assert.True(rules1.Equals(rules2));
        Assert.False(rules1.Equals(rules3));
    }

    [Fact]
    public void ValueObjects_ShouldNotBeEqualIfDifferentTypes()
    {
        Rules rules1 = new MaxBookingsPerMemberRules("Information", 3);
        Rules rules2 = new Rules("Information");
        Assert.False(rules1 == rules2);
    }
}
