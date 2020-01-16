using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchipAlgoritmiek
{
    public class Stack
    {
        private int _maxWeight = 150;
        public List<IContainer> Containers { get; set; }

        public bool HaValuable { get; set; }
        public bool IsCool { get; set; }
        public bool isValuable { get; set; }

        public Stack(bool coolable, bool valuable)
        {
            this.isValuable = valuable;
            this.IsCool = coolable;
            this.Containers = new List<IContainer>();
        }

        public void TryPlaceContainer(IContainer container)
        {
            if (ContainerCanFit(container))
            {
                if(container is ValuableContainer)
                {
                    this.HaValuable = true;
                }
                this.Containers.Add(container);
                MoveValuableToTop();
            }
        }

        public int GetWeight()
        {
            int weight = 0;
            foreach(IContainer container in Containers)
            {
                weight += container.Weight;
            }
            return weight;
        }

        // Parent method of private checks
        public bool ContainerCanFit(IContainer container)
        {
            if(CanHandleWeight(container) && ContainerMatchesStackType(container))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ContainerMatchesStackType(IContainer container)
        {
            if(container is CoolContainer)
            {
                if(IsCool == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if(container is ValuableContainer)
            {
                if (HaValuable)
                {
                    return false;
                }
                else
                {
                    if (IsCool == true)
                    {
                        return true;
                    }
                    else if (isValuable == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Check if the stack is not too heavy
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private bool CanHandleWeight(IContainer container)
        {
            int totalWeight = GetWeight() + container.Weight;
            if(totalWeight <= this._maxWeight && DoesNotGetCrushed(container))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Check whether the bottom container can support the weight.
        private bool DoesNotGetCrushed(IContainer container)
        {
            for(int i = 0; i < Containers.Count; i++)
            {
                int topWeight = 0;
                var topContainers = Containers.GetRange(i + 1, Containers.Count - i - 1);
                topContainers.Add(container);

                foreach(IContainer stackedContainer in topContainers)
                {
                    topWeight += stackedContainer.Weight;
                }
                if(topWeight > 120)
                {
                    return false;
                }
            }
            return true;
        }

        // Puts valuable containers, if any on top.
        private void MoveValuableToTop()
        {
            if (HaValuable)
            {
                var valContainer = (ValuableContainer)Containers.Find(v => v.TopStackAllowed == false);
                Containers.Remove(valContainer);
                Containers.Add(valContainer);
            }
        }
    }
}
