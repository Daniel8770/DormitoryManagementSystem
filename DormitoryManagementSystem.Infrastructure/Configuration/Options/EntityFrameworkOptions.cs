using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Infrastructure.Configuration.Options;
public class EntityFrameworkOptions
{
    public const string SectionName = "EntityFramework";
    public string ConnectionString { get; set; } = string.Empty;
}
