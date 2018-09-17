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
			List<Vector2> normals = new List<Vector2>();
			if (A.Collider.Shape == Shape.Box || B.Collider.Shape == Shape.Box)
			{
				normals.AddRange(new Vector2[] {
					new Vector2( 1,  0), new Vector2( 0,  1)
				});
			}
			if (A.Collider.Shape == Shape.Circle || B.Collider.Shape == Shape.Circle)
			{
				normals.Add( Vector2.Normalize(distance) );
			}

			Overlap overlap = new Overlap(A, B);
			foreach (Vector2 Normal in normals)
			{
				float projected_distance = 
					Math.Abs(Vector2.Dot(distance, Normal));
				float projected_limit = 
					Math.Abs(B.Collider.Project(B.Scale, Normal)) + 
					Math.Abs(A.Collider.Project(A.Scale, Normal));
				float Depth = projected_limit - projected_distance;

				// They overlap.
				if (Depth < overlap.Depth)
				{
					overlap.Normal = Vector2.Dot(Normal, distance) < 0 ? -Normal : Normal;
					overlap.Depth = Depth;
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
			if (shape == Shape.Box)
			{
				if (Math.Abs(Normal.X) > Math.Abs(Normal.Y))
				{
					Normal = new Vector2(Math.Sign(Normal.X), 0);
				}
				else
				{
					Normal = new Vector2(0, Math.Sign(Normal.Y));
				}

				return Vector2.Dot(Scale, Normal);
			}
			else 
			{
				return Vector2.Dot(Scale.X * Normal, Normal);
			}
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

