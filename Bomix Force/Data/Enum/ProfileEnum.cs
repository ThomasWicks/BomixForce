using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Enum
{
    public enum ProfileEnum
    {
        [Description("Administrador")]
        Administrador = 1,

        [Description("Gestor")]
        Gestor = 2,

        [Description("Visualizador")]
        Visualizador = 3,
    }
}
