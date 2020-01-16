using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchipAlgoritmiek
{
    public class NormalContainer: IContainer
    {
        int _weight = 4;
        public bool TopStackAllowed { get; } = true;
        public int MaxWeightOnTop { get; } = 120;
        public int Weight { get { return this._weight; } set { this._weight += value; } }
        public int MaxWeight { get; } = 30;
        public List<Row> AllowedRows { get; set; }
  
    }
}
