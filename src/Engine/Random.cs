using System;
using Microsoft.Xna.Framework;

namespace Vamp
{
	class RNG
	{
		static private Random random;

		static public void Seed(int seed)
		{
			random = new Random(seed);
		}

		static public Vector2 RandomUnitVector()
		{
			float angle = (float) (random.NextDouble() * Math.PI * 2);
			Vector2 result = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
			return result;
		}

		static public float RandomFloatInRange(float min, float max)
		{
			float result = (float) random.NextDouble();
			return (result * (max - min) + min);
		}

		static public int RandomIntInRange(int min, int max)
		{
			int result = (int) (random.Next()); 
			result = result % (max - min + 1);
			result = result + min;
			return result;
		}
		

	}
}

