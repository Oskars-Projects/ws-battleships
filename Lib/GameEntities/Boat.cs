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
                    Direction.HORIZONTAL => Math.Abs(Start.XAsInt - End.XAsInt),
                    Direction.VERTICAL => Math.Abs(Start.Y - End.Y),
                    _ => throw new ArgumentException("A boat can only have a length, if it is placed horizontal or vertical!"),
                };
            } 
        }

        public Direction Direction
        {
            get
            {
                if(Start.X == End.X) return Direction.HORIZONTAL;
                if(Start.Y == End.Y) return Direction.VERTICAL;
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
