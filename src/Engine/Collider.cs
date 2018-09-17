using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace Vamp
{
	public enum Shape
	{
		Box,
		Circle,
	}

	// This class handles and stores the information about Overlaps.
	public class Overlap
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

		// Moves the two GameObjects (a, b) out of eachother 
		public void Solve()
		{
			if (depth < 0)
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

		public float Depth { get { return depth; } set { depth = value; } }
		public Vector2 Normal { get { return normal; } set { normal = value; } }
	};

	// Holds all possible overlaps. TODO
	public class PhysicsSystem
	{
		private List<GameObject> gameObjects;

		public PhysicsSystem()
		{
			gameObjects = new List<GameObject>();
		}

		// Checks if a and b overlap and if it does generates a valid Overlap.
		public Overlap Check(GameObject a, GameObject b)
		{
			Vector2 distance = a.Position - b.Position;

			// Find all potential normals.
			Vector2[] normals = new Vector2[] {
				new Vector2( 1,  0), new Vector2( 0,  1),
				Vector2.Normalize(distance)
			};

			Overlap overlap = new Overlap(a, b);
			foreach (Vector2 normal in normals)
			{
				float projected_distance = 
					Math.Abs(Vector2.Dot(distance, normal));
				float projected_limit = 
					Math.Abs(b.Collider.Project(b.Scale,  normal)) + 
					Math.Abs(a.Collider.Project(a.Scale, -normal));
				float depth = projected_limit - projected_distance;

				// They overlap.
				if (depth < overlap.depth)
				{
					// Can this be refactored.
					overlap.normal = Vector2.Dot(normal, distance) < 0 ? -normal : normal;
					overlap.depth = depth;

					if (depth < 0.0)
					{
						break;
					}
				}
			}

			return overlap;
		}
	}

	// Any GameObject that can collide has this.
	public class Collider
	{
		private Shape shape;
		private bool movable;

		public Collider(bool movable=false, Shape shape=Shape.Box) 
		{
			this.shape = shape;
			this.movable = movable;
		}

		// Projects the Collider like if it was in 1 dimensions.
		public float Project(Vector2 Scale, Vector2 normal)
		{
			float result;
			if (shape == Shape.Box)
			{
				result = Vector2.Dot(normal, 
						new Vector2(Scale.X * Math.Sign(normal.X), Scale.Y * Math.Sign(normal.Y)));
			}
			else 
			{
				result = Vector2.Dot(Scale.X * normal, normal);
			}
			return result;
		}

		public Shape Shape { get { return shape; } set { shape = value; }}
		public bool Movable { get { return movable; } set { movable = value; }}
	}
}

