using ContainerSchipAlgoritmiek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    
    public class StackTests
    {
        [Fact]
        public void TryPlaceContainer_ShouldNotLoadTwoValuables()
        {
            // Arrange
            int expected = 1;
            Stack stack = new Stack(true, true);
            IContainer val1 = new ValuableContainer();
            IContainer val2 = new ValuableContainer();
            stack.TryPlaceContainer(val1);

            // Act
            stack.TryPlaceContainer(val2);
            int result = stack.Containers.Count;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryPlaceContainer_ShouldNotLoadCool()
        {
            // Assert
            int expected = 0;
            Stack stack = new Stack(false, false);

            // Act
            stack.TryPlaceContainer(new CoolContainer());
            int result = stack.Containers.Count;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryPlaceContainer_ShouldNotLoadValuable()
        {
            // Arrange
            int expected = 0;
            Stack stack = new Stack(false, false);

            // Act
            stack.TryPlaceContainer(new ValuableContainer());
            int result = stack.Containers.Count;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryPlaceContainer_ShouldNotLoadTooMuchWeight()
        {
            // Arrange
            int expected = 5;
            Stack stack = new Stack(false, false);
            List<IContainer> toLoad = new List<IContainer>()
            {
                new NormalContainer() { Weight = 30 },
                new NormalContainer() { Weight = 30 },
                new NormalContainer() { Weight = 30 },
                new NormalContainer() { Weight = 30 },
                new NormalContainer() { Weight = 10 } 
            };

            foreach (var c in toLoad)
            {
                stack.TryPlaceContainer(c);
            }

            // Act
            stack.TryPlaceContainer(new NormalContainer() { Weight = 20 });
            int result = stack.Containers.Count;

            // Arrange
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TryPlaceContainer_ShouldMoveValuableToTop()
        {
            // Arrange
            Stack stack = new Stack(true, true);
            IContainer valContainer = new ValuableContainer();
            IContainer coolContainer = new CoolContainer();
            IContainer normalContainer = new NormalContainer();

            // Act
            stack.TryPlaceContainer(valContainer);
            stack.TryPlaceContainer(coolContainer);
            stack.TryPlaceContainer(normalContainer);

            // Assert
            Assert.Equal(valContainer, stack.Containers[2]);
        }
    }
}
