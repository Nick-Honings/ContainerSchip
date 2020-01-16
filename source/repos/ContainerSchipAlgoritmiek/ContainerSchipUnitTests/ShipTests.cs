using ContainerSchipAlgoritmiek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    
    public class ShipTests
    {
        [Fact]
        public void InitializeRows_ShouldWork()
        {
            // Arrange
            int expected = 10;
            Ship ship = new Ship(10, 3);

            // Act
            int result = ship.Rows.Count;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void InitializeRows_ShouldMakeFirstRowCool()
        {
            // Arrange
            bool expected = true;
            Ship ship = new Ship(10, 3);

            // Act
            bool result = ship.Rows[0].IsCoolRow;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void LoadContainers_ShouldWorkSmall()
        {
            // Arrange
            int expected = 5;
            Ship ship = new Ship(2, 2);
            List<IContainer> containers = Harbor.CreateShipment(0,0,20);
            
            // Act
            ship.LoadContainers(containers);
            int result = ship.Rows[1].Stacks[1].Containers.Count;
            
            // Assert
            Assert.Equal(expected, result);
        }


        [Fact]
        public void CanHandleWeight_ShouldWork()
        {
            // Arrange            
            Ship ship = new Ship(3, 2);
            List<IContainer> containers = Harbor.CreateShipment(4, 8, 18);
            
            // Act
            ship.LoadContainers(containers);
            (int, int) halfWeights = ship.GetFrontAndBackWeight();

            // Assert
            Assert.Equal(450, halfWeights.Item1);
        }

        [Fact]
        public void LoadContainer_ShouldReturnTrue()
        {
            // Arrange
            Ship ship = new Ship(3, 3);
            List<IContainer> containers = Harbor.CreateShipment(5, 0, 10);

            // Act
            bool result = ship.LoadContainers(containers);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LoadContainer_ShouldReturnFalseNotFit()
        {
            // Arrange
            Ship ship = new Ship(3, 2);
            List<IContainer> containers = Harbor.CreateShipment(5, 0, 10);
            
            // Act
            bool result = ship.LoadContainers(containers);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void LoadContainers_ShouldReturnFalseTooHeavy()
        {
            // Arrange
            Ship ship = new Ship(3, 2);
            List<IContainer> containers = Harbor.CreateShipment(10, 10, 10);

            // Act
            bool result = ship.LoadContainers(containers);

            // Assert
            Assert.False(result);
        }
    }
}
