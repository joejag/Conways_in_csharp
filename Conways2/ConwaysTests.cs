using System.Collections.Generic;
using NUnit.Framework;

namespace Conways2
{
    public class EndToEndTests
    {
        [Test]
        public void Blinker()
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
    }

    public class CellMathsTest
    {
        [Test]
        public void Frequencies()
        {
            var freqs = CellMaths.Frequencies(new List<Cell> { new Cell(1, 1), new Cell(1, 1) });

            CollectionAssert.AreEquivalent(new Dictionary<Cell, int>
            {
                { new Cell(1, 1), 2 }
            }, freqs);
        }

        [Test]
        public void CoordinateFinder()
        {
            var neighbours = CellMaths.NeighboursOf(new Cell(1, 1));

            Assert.Contains(new Cell(0, 0), neighbours);
            Assert.Contains(new Cell(0, 1), neighbours);
            Assert.Contains(new Cell(0, 2), neighbours);

            Assert.Contains(new Cell(1, 0), neighbours);
            Assert.Contains(new Cell(1, 2), neighbours);

            Assert.Contains(new Cell(2, 0), neighbours);
            Assert.Contains(new Cell(2, 1), neighbours);
            Assert.Contains(new Cell(2, 2), neighbours);
        }
    }

    public class CellTests
    {
        [Test]
        public void EqualityWorks()
        {
            Assert.AreEqual(new Cell(0, 0), new Cell(0, 0));
        }
    }
}