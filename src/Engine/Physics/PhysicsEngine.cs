using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace Vamp
{
	public struct ColliderLimits
	{
		float min, max;
		Collider collider;
	}

	public class CollisionSystem
	{
		List<Collider> colliders;
		List<ColliderLimits> limits;
			
		// Checks if a and b overlap and if it does generates a valid Overlap.
		public Overlap Check(Collider a_col, Collider b_col)
		{
			GameObject a = a_col.owner;
			GameObject b = b_col.owner;

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

		public void AddCollider(Collider c)
		{
			colliders.Add(c);
		}

		public void RemoveCollider(Collider c)
		{
			colliders.Remove(c);
		}

		// Check all colliders in the engine
		// for collisions.
		public void Update(float delta)
		{
			// TODO
			// Update Limits
			// Sort Limits
			// Check for collisions
		}
	}
}
