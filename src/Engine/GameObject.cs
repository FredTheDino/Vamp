using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
    public abstract class GameObject
    {
        private Collider collider;
        private Vector2 position;
        private float size, scale;

        public GameObject () : this(new Vector2(), 128, 1, null)
        {
            position = new Vector2();
            scale = 1;
            size = 128;
        }

        public GameObject (Vector2 position, float size, float scale, Collider collider)
        {
            this.position = position;
            this.size = size;
            this.scale = scale;
            this.collider = collider;
        }

        public Collider Collider { get => collider; set => collider = value; }
        public Vector2 Position { get => position; set => position = value; }
        public float Scale { get => scale; set => scale = value; }
        public float Size { get => size; set => size = value; }
    }
}