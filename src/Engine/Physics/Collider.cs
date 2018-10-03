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

	// Any GameObject that can collide has this.
	public class Collider
	{
		private GameObject owner;
		private Shape shape;
		private bool movable;
		private bool trigger;
		private List<Overlap> overlaps;

		public Shape Shape { get { return shape; } set { shape = value; }}
		public GameObject Owner { get { return owner; } set { owner = value; } }
		public bool Movable { get { return movable; } set { movable = value; }}
		public bool Trigger { get { return trigger; } set { trigger = value; }}
		public List<Overlap> Overlaps { get { return overlaps; } }

		public Collider(bool movable, Shape shape) 
		{
			Collider(movable, false, shape);
		}

		public Collider(bool movable=false, bool trigger=false, Shape shape=Shape.Box) 
		{
			this.shape = shape;
			this.movable = movable;
			this.trigger = trigger;
			this.overlaps = new List<Overlap>(10);
		}

		// Projects the Collider onto a normal.
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

		public void AddOverlap(Overlap overlap)
		{
			overlaps.Add(overlap);
		}
	
	}
}

