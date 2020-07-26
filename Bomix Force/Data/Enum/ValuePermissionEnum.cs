using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Enum
{
    public enum ValuePermissionEnum
    {
        [Description("Incluir")]
        INSERT = 0,
        [Description("Consultar")]
        CONSULT = 1,
        [Description("Excluir")]
        DELETE = 2,
    }
}
