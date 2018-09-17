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

	public class Overlap
	{
		public GameObject A, B;
		public float Depth;
		public Vector2 Normal;

		public Overlap(GameObject A, GameObject B)
		{
			this.A = A;
			this.B = B;
			this.Normal = new Vector2();
			this.Depth = float.MaxValue;
		}
	};

	public class PhysicsSystem
	{
		private List<GameObject> gameObjects;

		public PhysicsSystem()
		{
			gameObjects = new List<GameObject>();
		}

		public Overlap Check(GameObject A, GameObject B)
		{
			Vector2 distance = A.Position - B.Position;

			// Find all potential normals.
			Vector2[] normals = new Vector2[] {
				new Vector2( 1,  0), new Vector2( 0,  1),
				Vector2.Normalize(distance)
			};

			Overlap overlap = new Overlap(A, B);
			Console.WriteLine("----------");
			foreach (Vector2 Normal in normals)
			{
				float projected_distance = 
					Math.Abs(Vector2.Dot(distance, Normal));
				float projected_limit = 
					Math.Abs(B.Collider.Project(B.Scale,  Normal)) + 
					Math.Abs(A.Collider.Project(A.Scale, -Normal));
				float Depth = projected_limit - projected_distance;

				// They overlap.
				if (Depth < overlap.Depth)
				{
					// Can this be refactored.
					overlap.Normal = Vector2.Dot(Normal, distance) < 0 ? -Normal : Normal;
					overlap.Depth = Depth;
					Console.WriteLine($"{overlap.Normal}, {Depth}");
				}

				if (Depth < 0.0)
				{
					break;
				}
			}

			return overlap;
		}
	}

	public class Collider
	{
		private Shape shape;

		public Collider(Shape shape=Shape.Box) 
		{
			this.shape = shape;
		}

		public float Project(Vector2 Scale, Vector2 Normal)
		{
			float result;
			if (shape == Shape.Box)
			{
				result = Vector2.Dot(Scale, 
						new Vector2(Math.Sign(Normal.X), Math.Sign(Normal.Y)));
			}
			else 
			{
				result = Vector2.Dot(Scale.X * Normal, Normal);
			}
			return result;
		}

		public Shape Shape 
		{
			get
			{
				return shape; 
			}
			set 
			{
				shape = value; 
			}
		}
	}
}

