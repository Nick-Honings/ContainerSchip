using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchipAlgoritmiek
{
    public interface IContainer
    {
        bool TopStackAllowed { get; }
        int MaxWeightOnTop { get; }
        int Weight { get; set; }
        int MaxWeight { get; }
        List<Row> AllowedRows { get; set; }
    }
}
