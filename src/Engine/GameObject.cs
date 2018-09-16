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
    public abstract class GameObject
    {
        // The attached collider
        private Collider collider;

        // The position of the object
        private Vector2 position;

        // The scale of the object
        private Vector2 scale;


        // Base constructor
        public GameObject () : this(new Vector2(), new Vector2(1,1), null) {}

        // Main constructor
        public GameObject (Vector2 position, Vector2 scale, Collider collider)
        {
            this.position = position;
            this.scale = scale;
            this.collider = collider;
        }


        // Getter and Setter methods

        public Collider Collider 
		{ 
			get 
			{
				return collider;
			}
			set 
			{
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
