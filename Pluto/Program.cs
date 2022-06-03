// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;

namespace PlutoNS {

    public class Direction
    {
        // 0 north 1 east 2 south 3 west
        public int state = 0;
        private static Dictionary<char, int> coder = new Dictionary<char, int> { { 'L', -1 }, { 'R', 1 } };
        private static char[] StateC = { 'N', 'E', 'S', 'W' };
        public Direction(char v)
        {
            state = Array.IndexOf(StateC, v);
        }

        public char AsChar() { return StateC[state]; }
        public void Turn(char v)
        {
            Turn(coder[v]); // L,R
        }
        public void Turn(int v)
        {
            state = (state + v + 4) % 4;
        }
    }
    public class PlutoMob
    {
        public int x = 0;
        public int y = 0;
        public bool obstacleAppeared = false;

        private static int MaxX = 100;
        private static int MaxY = 100;

        public Direction direction = new Direction('N');

        private List<Tuple<int,int>> obstacle = new List<Tuple<int, int>>();
        public void SetObstacle(List<Tuple<int, int>> v)
        {
            obstacle = v;
        }
        public PlutoMob() { }
        private Tuple<int, int> NewPosition(bool forward, int dir)
        {
            // todo: check direction
            // default: throw new Exception("invalid direction");
            var x = this.x;
            var y = this.y;
            if(forward)
                switch (dir)
                {
                    case 0: Move(0, 1); break;
                    case 1: Move(1, 0); break;
                    case 2: Move(0, -1); break;
                    case 3: Move(-1, 0); break;
                }
            else
                switch (dir)
                {
                    case 0: Move(0, -1); break;
                    case 1: Move(-1, 0); break;
                    case 2: Move(0, 1); break;
                    case 3: Move(1, 0); break;
                }
            return Tuple.Create(x, y);

            void Move(int dx, int dy)
            {
                x = (x + dx + MaxX) % MaxX;
                y = (y + dy + MaxY) % MaxY;
            }
        }
        private void Move(bool forward)
        {
            var pos = NewPosition(forward, direction.state);
            x = pos.Item1;
            y = pos.Item2;
        }

        public bool TryMove(bool forward)
        {
            var pos = NewPosition(forward, direction.state);
            return !obstacle.Contains(pos);
        }

        public Tuple<char, int, int> Run(string rout)
        {
            obstacleAppeared = false;
            foreach (char ch in rout)
            {
                var oldx = x;
                var oldy = y;
                switch (ch)
                {
                    case 'F': Move(true); break;
                    case 'B': Move(false); break;
                    case 'R':
                    case 'L': direction.Turn(ch); break;
                    default: throw new Exception("Invalid char "+ch);
                }
                System.Diagnostics.Debug.WriteLine("{0} {1} {2} {3} ", ch.ToString(), direction.AsChar(), x, y);
                var pos = Tuple.Create(x, y);

                if (obstacle.Contains(pos))
                {
                    x = oldx;
                    y = oldy;
                    var res1 = new Tuple<char, int, int>(direction.AsChar(), x, y);
                    obstacleAppeared = true;
                    return res1;
                }
            }
            var res = new Tuple<char, int, int>(direction.AsChar(), x, y);
            return res;
        }
        public static void Main() {
            PlutoMob pm = new PlutoMob();
            var p = pm.Run("FFLFF");
            Console.WriteLine("finished " +p.ToString());
        }
    }

}