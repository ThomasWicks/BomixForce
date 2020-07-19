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
        Gerente = 1,

        [Description("Gestor")]
        GerenteLoja = 2,

        [Description("Visualizador")]
        Visualizador = 3,
    }
}
