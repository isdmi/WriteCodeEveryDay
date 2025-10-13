using GitHubAction;

namespace GitHubActionsTest
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsCorrectSum()
        {
            var calc = new Calculator();
            Assert.Equal(5, calc.Add(2, 3));
        }

        [Fact]
        public void Subtract_ReturnsCorrectDifference()
        {
            var calc = new Calculator();
            Assert.Equal(1, calc.Subtract(3, 2));
        }
    }
}
