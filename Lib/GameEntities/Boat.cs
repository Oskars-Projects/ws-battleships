namespace Lib.GameEntities
{
    public class Boat
    {
        public Location Start { get; set; }
        public Location End { get; set; }
    
        public int Length 
        { 
            get 
            {
                return Direction switch
                {
                    Direction.HORIZONTAL => Math.Abs(Start.XIdx - End.XIdx) + 1,
                    Direction.VERTICAL => Math.Abs(Start.Y - End.Y) + 1,
                    _ => throw new ArgumentException("A boat can only have a length, if it is placed horizontal or vertical!"),
                };
            } 
        }

        public Direction Direction
        {
            get
            {
                if(Start.X == End.X) return Direction.VERTICAL;
                if(Start.Y == End.Y) return Direction.HORIZONTAL;
                return Direction.DIAGONAL;
            }
        }

        public Boat(Location start, Location end)
        {
            Start = start;
            End = end;
        }
    }
}
