using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Vamp
{
	/*
	 * Colliders that lets things hit other colliders.
	 */

	public enum Shape
	{
		Box,
		Circle,
	}

	public class Collider
	{
		private GameObject owner;
		private Shape shape;

		public Collider(GameObject owner, Shape shape=Shape.Box) 
		{
			this.owner = owner;
				
			// TODO: Add to the collision engine.
		}

		public bool overlaps(Collider other)
		{
			Vector2 distance = owner.Position - other.owner.Position;
			Vector2 normalized_distance = Vector2.Normalize(distance);
			List<Vector2> normals;

			if (shape == other.Shape)
			{
				normals = new List<Vector2>(new Vector2[] {
						new Vector2( 1,  0), new Vector2( 0,  1),
						new Vector2(-1,  0), new Vector2( 0, -1)
				});
			}

			if (shape == Shape.Box && other.Shape == Shape.Box)
			{
				
			}
			else
			{
				Console.WriteLine("SHOULD NOT REACH HERE");
				return false;
			}

			return false;
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

