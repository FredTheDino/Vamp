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

        // The scale of the object
        private Vector2 scale = new Vector2(1, 1);
		
        // The attached collider
        private Collider collider = null;


        // Base constructor
        public GameObject () {}

        // Main constructor
        public GameObject (Vector2 position, Vector2 scale, Collider collider = null)
        {
            this.position = position;
            this.scale = scale;
			this.collider = collider;
        }

        // Empty Update function to allow all object to update
        public void Update () {}

        // Getter and Setter methods
        public Collider Collider 
		{ 
			get 
			{
				return collider;
			}
			set 
			{
				// TODO: Should set the owner field.
				collider = value; 
			}
		}
        public Vector2 Position 
		{ 
			get 
			{
				return position; 
			}
			set 
			{
				position = value; 
			}
		}
        public Vector2 Scale 
		{ 
			get 
			{
				return scale; 
			}
			set 
			{
				scale = value; 
			}
		}
    }
}

