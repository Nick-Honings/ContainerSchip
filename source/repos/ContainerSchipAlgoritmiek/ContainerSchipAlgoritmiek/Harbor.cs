using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchipAlgoritmiek
{
    public class Harbor
    {
        public static List<IContainer> CreateShipment (int valueCount, int coolCount, int normalCount)
        {
            List<IContainer> containers = new List<IContainer>();
            for (int v = 0; v < valueCount; v++)
            {
                containers.Add(new ValuableContainer() { Weight = 26 });
            }
            for (int c = 0; c < coolCount; c++)
            {
                containers.Add(new CoolContainer() { Weight = 26 });
            }
            for (int n = 0; n < normalCount; n++)
            {
                containers.Add(new NormalContainer() { Weight = 26 });
            }
            return containers;
        }

        public static List<IContainer> MakeContainersEmpty(int valueCount, int coolCount, int normalCount)
        {
            Random random = new Random();

            List<IContainer> containers = new List<IContainer>();
            for (int v = 0; v < valueCount; v++)
            {
                containers.Add(new ValuableContainer());
            }
            for (int c = 0; c < coolCount; c++)
            {
                containers.Add(new CoolContainer());
            }
            for (int n = 0; n < normalCount; n++)
            {
                containers.Add(new NormalContainer());
            }
            return containers;
        }
    }
}
