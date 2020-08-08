using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Enum
{
    public enum TypePermissionEnum
    {
        [Description("Gerenciamento de Usuario")]
        User = 0,
        [Description("Gerenciamento de Pedidos")]
        Order = 1,
        [Description("Gerenciar Permissões")]
        Permission = 2, 
        
    }
}
