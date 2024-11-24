using DormitoryManagementSystem.Domain.Common.MoneyModel;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDormitoryManagementStystem.UnitTests.Domain.Common.MoneyModel;

public class RoundingTest
{
    [Theory]
    [InlineData(3.115, 3.12)]
    [InlineData(3.125, 3.12)]
    [InlineData(3.1415, 3.14)]
    [InlineData(100.32897239847, 100.33)]
    [InlineData(-500.238947, -500.24)]
    [InlineData(-50.392084, -50.39)]
    [InlineData(70.320483242342342343243243242342, 70.32)]
    public void GetRoundedValue_ShouldRoundAccordingToBankersRounding(decimal amount, decimal rounded)
    {
        Money money = Money.CreateNew(amount, Currency.DKK);
        money.GetRoundedValue().Should().Be(rounded);
    }
}
