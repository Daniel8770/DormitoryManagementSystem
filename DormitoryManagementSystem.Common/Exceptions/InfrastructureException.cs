using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.Common.Exceptions;
public class InfrastructureException : Exception
{
    public InfrastructureException(string message) : base(message)
    {
    }


}
