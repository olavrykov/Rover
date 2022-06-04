using System;

namespace PlutoNS
{
    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y) : this()
        {
            this.x = x;
            this.y = y;
        }
    }
    public struct PlutoPosition
    {
        public char cdir;
        public int x;
        public int y;

        public PlutoPosition(char cdir, int x, int y) : this()
        {
            this.cdir = cdir;
            this.x = x;
            this.y = y;
        }
    }
    public class PlutoMob
    {
        public Point p = new Point(0,0);
        public bool obstacleAppeared = false;

        private static int MaxX = 100;
        private static int MaxY = 100;

        public Direction direction = new Direction('N');

        private List<Point> obstacle = new List<Point>();
        public void SetObstacle(List<Point> v)
        {
            obstacle = v;
        }
        public PlutoMob() 
        {
            // for testing purposes
        }
        private Point NewPosition(bool forward, int dir)
        {
            var p = this.p;
            if (forward)
                switch (dir)
                {
                    case 0: Move(0, 1); break;
                    case 1: Move(1, 0); break;
                    case 2: Move(0, -1); break;
                    case 3: Move(-1, 0); break;
                    default: Move(0, 0); break;
                }
            else
                switch (dir)
                {
                    case 0: Move(0, -1); break;
                    case 1: Move(-1, 0); break;
                    case 2: Move(0, 1); break;
                    case 3: Move(1, 0); break;
                    default: Move(0, 0); break;
                }
            return p;

            void Move(int dx, int dy)
            {
                p.x = (p.x + dx + MaxX) % MaxX;
                p.y = (p.y + dy + MaxY) % MaxY;
            }
        }
        private void Move(bool forward)
        {
            var pos = NewPosition(forward, direction.state);
            p = pos;
        }

        public bool TryMove(bool forward)
        {
            var pos = NewPosition(forward, direction.state);
            return !obstacle.Contains(pos);
        }

        public PlutoPosition Run(string rout)
        {
            obstacleAppeared = false;
            foreach (char ch in rout)
            {
                var oldp = p;
                switch (ch)
                {
                    case 'F': Move(true); break;
                    case 'B': Move(false); break;
                    case 'R':
                    case 'L': direction.Turn(ch); break;
                    default: throw new Exception("Invalid char "+ch);
                }
                System.Diagnostics.Debug.WriteLine("{0} {1} {2} {3}", ch.ToString(), direction.AsChar(), p.x, p.y);
                if (obstacle.Contains(p))
                {
                    p = oldp;
                    obstacleAppeared = true;
                    break;
                }
            }
            return new PlutoPosition(direction.AsChar(), p.x, p.y);
        }
        public static void Main() {
            PlutoMob pm = new PlutoMob();
            var p = pm.Run("FFLFF");
            Console.WriteLine("finished " +p.ToString());
        }
    }
}