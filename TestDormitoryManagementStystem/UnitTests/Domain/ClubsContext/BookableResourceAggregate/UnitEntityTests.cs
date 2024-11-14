using DormitoryManagementSystem.Domain.ClubsContext.BookableResourceAggregate;
using DormitoryManagementSystem.Domain.Common.Exceptions;


namespace TestDormitoryManagementStystem.UnitTests.Domain.ClubsContext.BookableResourceAggregate;
public class UnitEntityTests
{
    private UnitId unitId = new(1);

    [Fact]
    public void WhenConstructed_UnitNameCannotBeOnlyDigits()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, "123");
        });
    }

    [Fact]
    public void WhenConstructed_UnitNameCannotStartWithDigit()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, "1hejmeddig");
        });
    }

    [Fact]
    public void WhenConstructed_UnitNameCannotBeEmpty()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, string.Empty);
        });
    }

    [Fact]
    public void WhenConstructed_UnitNameCannotBeWhitespace()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, " ");
        });
    }

    [Fact]
    public void WhenUnitNameChanged_NameCannotBeOnlyDigits()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, "hej");
            unit.ChangeName("123");
        });
    }

    [Fact]
    public void WhenUnitNameChanged_UnitNameCannotStartWithDigit()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, "hej");
            unit.ChangeName("1hejmeddig");
        });
    }

    [Fact]
    public void WhenUnitNameChanged_UnitNameCannotBeEmpty()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, "hej");
            unit.ChangeName(string.Empty);
        });
    }

    [Fact]
    public void WhenUnitNameChanged_UnitNameCannotBeWhitespace()
    {
        Assert.Throws<DomainException>(() =>
        {
            Unit unit = new(unitId, "hej");
            unit.ChangeName(" ");
        });
    }
}
