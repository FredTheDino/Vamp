using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
	class Camera
	{
		private Vector2 position;
		private Vector2 offset;
		private float zoom;
		private float rotation;

		private Matrix viewMatrix;

		GameObject target;
		GraphicsDevice device;

		public Camera(GameObject target, GraphicsDevice device)
		{
			this.position = new Vector2(0, 0);
			this.offset = offset;
			this.zoom = 2.0f;
			this.rotation = 0.0f;
			this.target = target;
			this.device = device;
		}

		public void Update(GameTime time)
		{
			float delta = (float) time.ElapsedGameTime.TotalSeconds;
			Vector2 deltaPosition = target.Position - position - offset;
			position += deltaPosition * delta;

			viewMatrix = Matrix.CreateTranslation(-position.X, -position.Y, 0);
		}

		public Matrix ViewMatrix { get { return viewMatrix; } }
		public GameObject Target { get { return target; } set { target = value; } }
	}
}
