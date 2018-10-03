using Microsoft.Xna.Framework;

namespace Vamp
{
	// This class handles and stores the information about Overlaps.
	public struct Overlap
	{
		// The GameObjects involved in the Overlap.
		public GameObject a, b;
		// How deep the collision goes. Negative means overlap.
		public float depth;
		// The direction of the collision, pointing from "a" to "b".
		public Vector2 normal;

		// Instanciates an Overlap class.
		public Overlap(GameObject a, GameObject b)
		{
			this.a = a;
			this.b = b;
			this.normal = new Vector2();
			this.depth = float.MaxValue;
		}

		public static implicit operator bool(Overlap c)
		{
			return !object.ReferenceEquals(c, null) && c.depth > 0;
		}

		// Moves the two GameObjects (a, b) out of eachother 
		public void Solve()
		{
			if (!this)
				return;

			// This is kinda a dumb way to do it.
			float a_mass = a.Collider.Movable ? 1.0f : 0.0f; 
			float b_mass = b.Collider.Movable ? 1.0f : 0.0f; 

			if ((a_mass + b_mass) == 0.0f)
				return;

			float inverseTotalMass = 1.0f / (a_mass + b_mass);
			a.Position += normal * depth * a_mass * inverseTotalMass;
			b.Position -= normal * depth * b_mass * inverseTotalMass;
		}
	};
}

