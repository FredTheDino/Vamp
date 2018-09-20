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

	// Holds all possible overlaps. TODO
	public class CollisionSystem
	{
		private List<GameObject> gameObjects;

		public CollisionSystem()
		{
			gameObjects = new List<GameObject>();
		}

		// Add a GameObject to the list of objects to check.
		public void Add(GameObject obj)
		{
			gameObjects.Add(obj);
		}

		// Checks if a and b overlap and if it does generates a valid Overlap.
		public Overlap Check(GameObject a, GameObject b)
		{
			Overlap overlap = new Overlap(a, b);
			Vector2 distance = a.Position - b.Position;
			// Find all potential normals.
			Vector2[] normals = new Vector2[] {
				new Vector2( 1,  0), new Vector2( 0,  1),
				Vector2.Normalize(distance)
			};

			foreach (Vector2 normal in normals)
			{
				float projected_distance = 
					Math.Abs(Vector2.Dot(distance, normal));
				float projected_limit = 
					Math.Abs(b.Collider.Project(b.Size(),  normal)) + 
					Math.Abs(a.Collider.Project(a.Size(), -normal));
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
		public float Project(Vector2 Size, Vector2 normal)
		{
			float result;
			if (shape == Shape.Box)
			{
				result = Vector2.Dot(normal, 
						new Vector2(
							Size.X * Math.Sign(normal.X), 
							Size.Y * Math.Sign(normal.Y)
						));
			}
			else 
			{
				result = Vector2.Dot(Size.X * normal, normal);
			}
			return result;
		}

		public Shape Shape { get { return shape; } set { shape = value; }}
		public bool Movable { get { return movable; } set { movable = value; }}
	}
}

