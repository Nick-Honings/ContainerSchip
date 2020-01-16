using ContainerSchipAlgoritmiek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    
    public class RowTests
    {
        [Fact]
        public void InitializeStacks_ShouldWorkWithNormal()
        {
            // Arrange
            int expected = 4;

            // Act
            Row normalRow = new Row(4, false, false, 1);
            int result = normalRow.Stacks.Count;

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void InitializeStacks_ShouldWorkWithCool()
        {
            // Arrange
            List<Stack> expected = new List<Stack>()
            {
                new Stack(true, false), 
                new Stack(true, false), 
                new Stack(true, false), 
                new Stack(true, false),
            };

            // Act
            Row coolRow = new Row(4, true, false,  1);
            var result = coolRow.Stacks;

            Assert.Equal(result[0].IsCool, expected[0].IsCool);
        }

        [Fact]
        public void InitializeStacks_ShouldWorkWithValuable()
        {
            // Arrange
            List<Stack> expected = new List<Stack>()
            {
                new Stack(true, true), 
                new Stack(true, true), 
                new Stack(true, true), 
                new Stack(true, true),
            };

            // Act
            Row valRow = new Row(4, true, true, 1);
            var result = valRow.Stacks;

            // Assert
            Assert.Equal(result[0].isValuable, expected[0].isValuable);
        }
    }
}
