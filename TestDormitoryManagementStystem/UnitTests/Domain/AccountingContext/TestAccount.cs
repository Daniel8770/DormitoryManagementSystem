using DormitoryManagementSystem.Common.Aggregates;
using DormitoryManagementSystem.Common.Entities;
using DormitoryManagementSystem.Domain.AccountingContext.Account;

namespace TestDormitoryManagementStystem.UnitTests.Domain.AccountingContext;

public class TestAccount
{

    [Fact]
    public void AccountCanOnlyBeCreatedWithAccountId()
    {
        Account sut = new Account(AccountId.Next());
        Assert.True(sut.Id.Value != Guid.Empty);
        Assert.IsAssignableFrom<AggregateRoot>(sut);
        Assert.IsAssignableFrom<Entity>(sut);
    }

}
