using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchipAlgoritmiek
{
    public class BaseContainer
    {
        int _weight = 4;
        public bool TopStackAllowed { get { return true; } }
        public int MaxWeightOnTop { get { return 120; } }
        public int Weight { get { return this._weight; } set { this._weight += value; } }
        public int MaxWeight { get { return 30; } }
        public List<Row> AllowedRows { get; set; }
    }
}
