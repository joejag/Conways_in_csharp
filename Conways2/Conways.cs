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

        public List<Cell> CordsFor(Cell cell)
        {
            return new List<Cell>
            {
                new Cell(cell.X-1, cell.Y+1), new Cell(cell.X, cell.Y+1), new Cell(cell.X+1, cell.Y+1),
                new Cell(cell.X-1, cell.Y),                               new Cell(cell.X+1, cell.Y),
                new Cell(cell.X-1, cell.Y-1), new Cell(cell.X, cell.Y-1), new Cell(cell.X+1, cell.Y-1)
            };
        }

        public Dictionary<Cell, int> Frequencies(IEnumerable<Cell> allNeighbours)
        {
            return allNeighbours.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
        }

        public HashSet<Cell> NextIteration(HashSet<Cell> world)
        {
            var neighbouringCells = world.SelectMany(CordsFor);
            var frequencies = Frequencies(neighbouringCells);
            var newWorld = frequencies.Where(x => Survives(x.Value, world.Contains(x.Key))).Select(x => x.Key);

            return new HashSet<Cell>(newWorld);
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
