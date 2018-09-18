using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
    /*
     * Main class for all objects in the game
     */
    public /*abstract*/ class GameObject
    {
        // The position of the object
        private Vector2 position = new Vector2();
		
		// The dimensions of the object.
        private Vector2 dimension = new Vector2(1, 1);

        // The scale of the object
        private Vector2 scale = new Vector2(1, 1);
		
        // The attached collider
        private Collider collider = null;


        // Base constructor
        public GameObject () {}

        // Main constructor
        public GameObject (Vector2 position, Vector2 dimension, Vector2 scale, Collider collider = null)
        {
            this.position = position;
			this.dimension = dimension;
            this.scale = scale;
			this.collider = collider;
        }

        // Empty Update function to allow all object to update
        public void Update () {}

		// Calcuates the size of the object.
		public Vector2 Size()
		{
			return new Vector2(dimension.X * scale.X, dimension.Y * scale.Y);
		}

		// Draw the collider on the body.
		public void DrawCollider(SpriteBatch batch)
		{
			if (collider == null)
				return;

			if (collider.Shape == Shape.Box)
			{
				Vector2 min = position;
				Vector2 max = position + Size() * 2;
				Debug.DrawLine(batch, min, new Vector2(max.X, min.Y));
				Debug.DrawLine(batch, min, new Vector2(min.X, max.Y));
				Debug.DrawLine(batch, max, new Vector2(max.X, min.Y));
				Debug.DrawLine(batch, max, new Vector2(min.X, max.Y));
			}
			else if (collider.Shape == Shape.Circle)
			{
				Vector2 center = position + Size();
				float r = Size().Y;
				Vector2 last = center + new Vector2((float) Math.Cos(0), (float) Math.Sin(0)) * r;
				int numSegments = 32;
				for (int i = 1; i <= numSegments; i++)
				{
					float angle = (float) ((i / (double) numSegments) * Math.PI * 2);
					Vector2 point = center + 
						new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle)) * r;
					Debug.DrawLine(batch, last, point);
					last = point;
				}
			}
		}

        // Getter and Setter methods
        public Collider Collider 
		{ get { return collider; } set { collider = value; } }
        public Vector2 Dimension 
		{ get { return dimension; } set { dimension = value; } }
        public Vector2 Position 
		{ get { return position; } set { position = value; } }
		public Vector2 Scale 
		{ get { return scale; } set { scale = value; } }    
	}
}

