using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Domain.NotificationContext;
public class NotificationId
{
    public Guid Value { get; init; }

    public static NotificationId Next() => new NotificationId(Guid.NewGuid());

    private NotificationId(Guid value)
    {
        Value = value;
    }


}
