using System.Collections.Generic;
using System.Linq;

namespace Conways2
{
    public class Conways
    {
        public bool Survives(int neighbourCount, bool alive)
        {
            if (alive && (neighbourCount == 2 || neighbourCount == 3)) return true;
            if (!alive && neighbourCount == 3) return true;

            return false;
        }

        public List<Cell> NeighboursOf(Cell cell)
        {
            return new List<Cell>
            {
                new Cell(cell.X-1, cell.Y+1), new Cell(cell.X, cell.Y+1), new Cell(cell.X+1, cell.Y+1),
                new Cell(cell.X-1, cell.Y),                               new Cell(cell.X+1, cell.Y),
                new Cell(cell.X-1, cell.Y-1), new Cell(cell.X, cell.Y-1), new Cell(cell.X+1, cell.Y-1)
            };
        }

        public HashSet<Cell> NextIteration(HashSet<Cell> world)
        {
            var frequencies = FindHowOftenANeighbourIsReferenced(world);
            var newWorld = ApplyTheSurvivalRules(world, frequencies);

            return new HashSet<Cell>(newWorld);
        }

        private Dictionary<Cell, int> FindHowOftenANeighbourIsReferenced(HashSet<Cell> world)
        {
            var allNeighbours = world.SelectMany(NeighboursOf);
            return Frequencies(allNeighbours);
        }

        public Dictionary<Cell, int> Frequencies(IEnumerable<Cell> allNeighbours)
        {
            return allNeighbours
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        private IEnumerable<Cell> ApplyTheSurvivalRules(HashSet<Cell> world, Dictionary<Cell, int> frequencies)
        {
            return frequencies
                .Where(x => Survives(x.Value, world.Contains(x.Key)))
                .Select(x => x.Key);
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

        public override string ToString()
        {
            return $"[Cell: {X}, {Y}]";
        }
    }
}
