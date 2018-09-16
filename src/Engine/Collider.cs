using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Vamp
{
	/*
	 * Colliders that lets things hit other colliders.
	 */
	public class Collider
	{
		private GameObject owner;
		private bool is_box = false;

		public Collider(GameObject owner, bool is_box=false) 
		{
			this.owner = owner;
			this.is_box = is_box;
				
			// TODO: Add to the collision engine.
		}

		public bool overlaps(Collider other)
		{
			
			List<Vector2> normals;
			if (!this.is_box || !other.is_box)
			{
				normals = new List<Vector2>(new Vector2[] {
					new Vector2( 1,  0), new Vector2( 0,  1),
					new Vector2(-1,  0), new Vector2( 0, -1),
					new Vector2( 1,  1), new Vector2( 1, -1),
					new Vector2(-1,  1), new Vector2(-1, -1)
				});
			}
			else
			{
				normals = new List<Vector2>(new Vector2[] {
					new Vector2( 1,  0), new Vector2( 0,  1),
					new Vector2(-1,  0), new Vector2( 0, -1)
				});
			}

			foreach (Vector2 normal in normals)
			{
			}
			return false;
		}

		// Dumb Getter and Setter methods
		public GameObject Owner { get => owner; set => owner = value; }
		public bool Box { get => is_box; set => is_box = value; }
	}
}

