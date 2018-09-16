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
		public Collider a, b;
		public float depth;
		public Vector2 normal;

		public Overlap(Collider a, Collider b)
		{
			this.a = a;
			this.b = b;
			this.normal = new Vector2();
			this.depth = float.MaxValue;
		}
	};

	public class Collider
	{
		private GameObject owner;
		private Shape shape;

		public Collider(GameObject owner, Shape shape=Shape.Box) 
		{
			this.owner = owner;
				
			// TODO: Add to the collision engine.
		}

		public float Project(Vector2 normal)
		{
			if (shape == Shape.Box)
			{
				if (Math.Abs(normal.X) > Math.Abs(normal.Y))
				{
					normal = new Vector2(Math.Sign(normal.X), 0);
				}
				else
				{
					normal = new Vector2(0, Math.Sign(normal.Y));
				}

				return Vector2.Dot(owner.Scale + owner.Position, normal);
			}
			else 
			{
				return Vector2.Dot(owner.Scale.X * normal + owner.Position, normal);
			}
		}

		public Overlap overlaps(Collider other)
		{
			Vector2 distance = owner.Position - other.owner.Position;

			// Find all potential normals.
			List<Vector2> normals = new List<Vector2>();
			if (this.shape == Shape.Box || other.shape == Shape.Box)
			{
				normals.AddRange(new Vector2[] {
					new Vector2( 1,  0), new Vector2( 0,  1)
				});
			}
			if (this.shape == Shape.Circle || other.shape == Shape.Circle)
			{
				normals.Add( Vector2.Normalize(distance) );
			}

			Overlap overlap = new Overlap(this, other);
			foreach (Vector2 normal in normals)
			{
				// Get extreems (Different signs of the normals).
				// Check if they overlap with distance projected.
				// If they don't, return false, else keep looping until
				// we run out of normals.
			}

			return overlap;
		}

		// Dumb Getter and Setter methods
		public GameObject Owner 
		{
			get
			{
				return owner; 
			}
			set 
			{
				owner = value; 
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

