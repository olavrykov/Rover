// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

namespace PlutoNS
{
	public class Direction
	{
		// 0 north 1 east 2 south 3 west
		public int state { get; private set; }
		private static IReadOnlyDictionary<char, int> turnCoder = new Dictionary<char, int> { { 'L', -1 }, { 'R', 1 } };
		private static readonly char[] StateC = { 'N', 'E', 'S', 'W' };
		public Direction(char v)
		{
			state = Array.IndexOf(StateC, v);
			if (state < 0)
				throw new ArgumentOutOfRangeException(nameof(v));
		}

		public char AsChar()
		{
			return StateC[state];
		}
		public void Turn(char v)
		{
			if (!turnCoder.ContainsKey(v))
				throw new ArgumentOutOfRangeException(nameof(v));

			Turn(turnCoder[v]); // L,R
		}
		public void Turn(int v)
		{
			state = (state + v + 4) % 4;
		}
	}

}
