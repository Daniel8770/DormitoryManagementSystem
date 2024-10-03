using DormitoryManagementSystem.Domain.Common.Exceptions;

namespace DormitoryManagementSystem.Domain.Common.MoneyModel;

public class CurrencyMismatchException : DomainException
{
    public static CurrencyMismatchException CreateExceptionWith(Money x, Money y) => 
        new CurrencyMismatchException($"The two money instances has currency mismatch. " +
            $"The left hand side is in {x.Currency} and the right hand side is in {y.Currency}.");

    public CurrencyMismatchException(string msg) : base(msg) { }
    public CurrencyMismatchException(string msg, Exception inner) : base(msg, inner) { }
}
