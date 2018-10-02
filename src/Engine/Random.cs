using System;
using Microsoft.Xna.Framework;

namespace Vamp
{
	/*
	 * A helper class for getting structured random data
	 * that can be used fast and efficent.
	 *
	 * TODO: Replace Random with a better generator.
	 */
	class RNG
	{
		// Variable declarations
		static private Random random;

		// Main constructor
		static public void Seed(int seed)
		{
			random = new Random(seed);
		}
		
		// Returns a random unit Vector2
		static public Vector2 RandomUnitVector()
		{
			float angle = (float) (random.NextDouble() * Math.PI * 2);
			Vector2 result = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
			return result;
		}

		// Returns a random float in the specified range [min, max]
		static public float RandomFloatInRange(float min, float max)
		{
			float result = (float) random.NextDouble();
			return result * (max - min) + min;
		}

		// Returns a random int in the specified range [min, max]
		static public int RandomIntInRange(int min, int max)
		{
			int result = (int) (random.Next()); 
			result = result % (max - min + 1);
			result = result + min;
			return result;
		}
	}
}

