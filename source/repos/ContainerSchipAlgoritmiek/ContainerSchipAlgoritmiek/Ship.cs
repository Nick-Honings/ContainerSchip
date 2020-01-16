using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchipAlgoritmiek
{
    public class Ship
    {
        public List<Row> Rows { get; set; }
        public int MaxWeight { get; set; }
        public int MinWeight { get; set; }

        public Ship(int shiplength, int shipwidth)
        {
            MaxWeight = (shiplength * shipwidth) * 150;
            MinWeight = MaxWeight / 2;

            //Initialize all rows that fit in the ship.
            Rows = InitializeRows(shiplength, shipwidth);
        }

        private List<Row> InitializeRows(int shiplength, int shipwidth)
        {
            List<Row> output = new List<Row>();
            for (int i = 0; i < shiplength; i++)
            {
                // Cooled row
                if (i == 0)
                {
                    output.Add(new Row(shipwidth, true, true, i));
                }
                else if (i == shiplength - 1)
                {
                    output.Add(new Row(shipwidth, false, true, i));
                }
                else
                {
                    output.Add(new Row(shipwidth, false, false, i));
                }
            }
            return output;
        }

        // Might return the number of containers that are not added.
        public bool LoadContainers(List<IContainer> containers)
        {
            int placedContainers = 0;
            if (CanHanldeWeight(containers))
            {                
                foreach (IContainer container in containers)
                {
                    Stack stack = FindStack(container);
                    if (stack != null)
                    {
                        stack.TryPlaceContainer(container);
                        placedContainers++;
                    }                    
                }

                // If all containers are placed, return true, else return false.
                if(placedContainers == containers.Count)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the ship can handle the weight of the containers we're trying to add
        /// </summary>
        /// <param name="containers"></param>
        /// <returns></returns>
        private bool CanHanldeWeight(List<IContainer> containers)
        {
            int weight = 0;
            foreach (IContainer container in containers)
            {
                weight += container.Weight;
            }
            if (weight >= MinWeight && weight <= MaxWeight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Finds a stack that can fit the container, if none are found it returns null
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        private Stack FindStack(IContainer container)
        {
            // Calculate the weight of both halves of the ship.
            (int, int) weights = GetFrontAndBackWeight();

            if (container is CoolContainer || CanHandleWeight(weights.Item1, weights.Item2))
            {
                foreach (Row row in Rows)
                {
                    foreach (Stack stack in row.Stacks)
                    {
                        if (stack.ContainerCanFit(container))
                        {
                            return stack;
                        }
                    }
                }
            }            
            else
            {   
                // Use a reverse for-loop because we're trying to place a container on the back of the ship.
                for (int i = Rows.Count - 1; i >= 0; i--)
                {
                    for (int j = Rows[i].Stacks.Count - 1; j >= 0; j--)
                    {
                        if (Rows[i].Stacks[j].ContainerCanFit(container))
                        {
                            return Rows[i].Stacks[j];
                        }
                    }
                }
            }
            return null;
        }

        public int GetWeight()
        {
            int weight = 0;
            foreach (Row row in Rows)
            {
                weight += row.GetWeight();
            }
            return weight;
        }

        /// <summary>
        /// Checks if the weight distribution is ok, this method uses a 60% to 40% approach.
        /// </summary>
        /// <param name="frontWeight"></param>
        /// <param name="backWeight"></param>
        /// <returns></returns>
        public bool CanHandleWeight(int frontWeight, int backWeight)
        {           
            //Calculate the needed percentage
            double frontPerc = frontWeight / (double)GetWeight() * 100.0;
            double backPerc = backWeight / (double)GetWeight() * 100.0;

            if (frontPerc > 60 || frontPerc < 40 || backPerc > 60 || backPerc < 40)
            {
                return false;
            }
            return true;
  
        }

        /// <summary>
        /// Calculates the weight of the front and back row.
        /// If there is an uneven number of rows on the ship it distributes the weight of the middle row evenly to the front and the left.
        /// </summary>
        /// <returns></returns>
        public (int, int) GetFrontAndBackWeight()
        {
            int frontWeight = 0;
            int backWeight = 0;            
            double halfRowCount = Rows.Count / 2.0;

            // Even amount of rows.
            if (halfRowCount % 1 == 0)
            {
                for (int i = 0; i < halfRowCount; i++)
                {
                    frontWeight += Rows[i].GetWeight();
                    backWeight += Rows[i + (int)halfRowCount].GetWeight();
                }
            }

            // Uneven amount.
            else
            {
                int middlerowPosition = Rows.Count / 2;
                // Split the middle row weight into two parts and distribute to front and back.
                int middleRowWeightHalf = Rows[middlerowPosition].GetWeight() / 2;

                for (int i = 0; i < Rows.Count; i++)
                {                   
                    if (i < middlerowPosition)
                    {
                        frontWeight += Rows[i].GetWeight();
                    }
                    //Distribute to both halves.
                    else if (i == middlerowPosition)
                    {
                        frontWeight += middleRowWeightHalf;
                        backWeight += middleRowWeightHalf;
                    }
                    else
                    {
                        backWeight += Rows[i].GetWeight();
                    }
                }
            }           
            return (frontWeight, backWeight);
        }
    }
}
