
namespace DormitoryManagementSystem.Domain.Common.Exceptions;

public class DomainException : Exception
{
    public DomainException() { }

    public DomainException(string msg) : base(msg) { }

    public DomainException(string msg, Exception inner) : base(msg, inner) { }  
}
