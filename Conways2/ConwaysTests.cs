using System.Collections.Generic;
using NUnit.Framework;

namespace Conways2
{
    public class ConwaysTests
    {
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        public void TheRulesWhenAlive(int neighbourCount, bool expectation)
        {
            Assert.AreEqual(expectation, new Conways().Survives(neighbourCount, true));
        }

        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, true)]
        [TestCase(4, false)]
        public void TheRulesWhenDead(int neighbourCount, bool expectation)
        {
            Assert.AreEqual(expectation, new Conways().Survives(neighbourCount, false));
        }

        [Test]
        public void CoordinateFinder()
        {
            var neighbours = new Conways().CordsFor(new Cell(1, 1));

            Assert.Contains(new Cell(0, 0), neighbours);
            Assert.Contains(new Cell(0, 1), neighbours);
            Assert.Contains(new Cell(0, 2), neighbours);

            Assert.Contains(new Cell(1, 0), neighbours);
            Assert.Contains(new Cell(1, 2), neighbours);

            Assert.Contains(new Cell(2, 0), neighbours);
            Assert.Contains(new Cell(2, 1), neighbours);
            Assert.Contains(new Cell(2, 2), neighbours);
        }

        [Test]
        public void Frequencies()
        {
            var freqs = new Conways().Frequencies(new List<Cell> { new Cell(1, 1), new Cell(1, 1) });
            var expected = new Dictionary<Cell, int> { { new Cell(1, 1), 2 } };
            CollectionAssert.AreEquivalent(expected, freqs);
        }

        [Test]
        public void EndToEnd_Blinker()
        {
            var world = new HashSet<Cell>
            {
                new Cell(1,1), new Cell(1,2), new Cell(1,3)
            };

            HashSet<Cell> anotherWorld = new Conways().NextIteration(world);

            CollectionAssert.AreEqual(new HashSet<Cell>
            {
                new Cell(0,2), new Cell(1,2), new Cell(2,2)
            }, anotherWorld);
        }
    }
}