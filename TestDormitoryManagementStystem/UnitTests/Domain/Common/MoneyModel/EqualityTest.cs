using DormitoryManagementSystem.Domain.Common.MoneyModel;
using FluentAssertions;

namespace TestDormitoryManagementStystem.UnitTests.Domain.Common.MoneyModel;

public class EqualityTest
{
    [Fact]
    public void Equality()
    {
        Money money1 = new Money(100, Currency.DKK);
        Money money2 = new Money(100, Currency.DKK);

        Assert.True(money1 == money2);
        Assert.True(money1.Equals(money2));
    }

    [Fact]
    public void BiggerThan()
    {
        Money money1 = new Money(100, Currency.DKK);
        Money money2 = new Money(200, Currency.DKK);

        Assert.True(money2 > money1);
    }

    [Fact]
    public void LessThan()
    {
        Money money1 = new Money(100, Currency.DKK);
        Money money2 = new Money(200, Currency.DKK);

        Assert.True(money1 < money2);
    }


}
