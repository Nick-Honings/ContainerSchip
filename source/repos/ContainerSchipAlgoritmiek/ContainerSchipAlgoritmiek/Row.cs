using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchipAlgoritmiek
{
    public class Row
    {
        public int Length { get; set; }
        public int ShipPosition { get; set; }
        public List<Stack> Stacks { get; private set; }
        public bool IsCoolRow { get; set; }
        public bool isValuableRow { get; set; }

        public Row(int length, bool iscool, bool isvaluable, int rowposition)
        {
            ShipPosition = rowposition;
            isValuableRow = isvaluable;
            IsCoolRow = iscool;

            // When the row is being made, initialize all stacks that fit within the specified length.
            Stacks = InitializeStacks(length);            

        }

        private List<Stack> InitializeStacks(int length)
        {
            List<Stack> output = new List<Stack>();
            for (int i = 0; i < length; i++)
            {
                if (this.IsCoolRow == true)
                {
                    output.Add(new Stack(true, true));
                }
                else if (this.isValuableRow == true && this.IsCoolRow == false)
                {
                    output.Add(new Stack(false, true));
                }
                else
                {
                    output.Add(new Stack(false, false));
                }
            }
            return output;
        }

        public int GetWeight()
        {
            int weight = 0;
            foreach(Stack stack in Stacks)
            {
                weight += stack.GetWeight();
            }
            return weight;
        }
    }
}
