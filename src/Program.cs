using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vamp
{
	static class Program
	{
		static void Main(string[] args)
		{
			using (VampGame game = new VampGame())
			{
				game.Run();
			}
		}
	}
}
