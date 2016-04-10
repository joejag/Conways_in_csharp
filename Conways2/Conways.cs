using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var neighbours = new Conways().CordsFor(new Cell(1, 1), 1, 1);

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

    }


    public class Conways
    {
        public Dictionary<Cell, int> Frequencies(List<Cell> allNeighbours)
        {
            var result = new Dictionary<Cell, int>();

            foreach (var neighbour in allNeighbours)
            {
                if (result.ContainsKey(neighbour))
                    result[neighbour] = result[neighbour] + 1;
                else
                    result[neighbour] = 1;
            }

            return result;
        }

        public bool Survives(int neighbourCount, bool alive)
        {
            if (alive && (neighbourCount == 2 || neighbourCount == 3)) return true;
            if (!alive && neighbourCount == 3) return true;

            return false;
        }

        public List<Cell> CordsFor(Cell cell, int x, int y)
        {
            return new List<Cell>
            {
                new Cell(x-1, y+1), new Cell(x, y+1), new Cell(x+1, y+1),
                new Cell(x-1, y),                     new Cell(x+1, y),
                new Cell(x-1, y-1), new Cell(x, y-1), new Cell(x+1, y-1)
            };
        }
    }

    public class Cell
    {
        public int X { get; }
        public int Y { get; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected bool Equals(Cell other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }
}
