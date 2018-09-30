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

		private float shakeTimer;
		private float speed = 4;
		private float maxCamDistance = 100;

		private Random rng;

		private Matrix viewMatrix;

		GameObject target;
		GraphicsDevice device;

		public Camera(GameObject target, GraphicsDevice device)
		{
			this.position = new Vector2(0, 0);
			this.offset = offset;
			this.zoom = 0.5f;
			this.rotation = 0.0f;
			this.target = target;
			this.device = device;
			this.rng = new Random(1234443777);
		}

		public void Shake(float time)
		{
			shakeTimer += time;
		}


		public void Update(GameTime time)
		{
			float delta = (float) time.ElapsedGameTime.TotalSeconds;
			shakeTimer = Math.Max(0, shakeTimer - delta);

			Rectangle viewRect = device.Viewport.Bounds;
			Vector2 offset = new Vector2(viewRect.Width, viewRect.Height) * 0.5f;
			Vector2 deltaPosition = target.Position - position;
			position += deltaPosition * Math.Min(delta * speed, 1.0f);

			Vector2 relative_position = position - target.Position;
			if (relative_position.LengthSquared() > maxCamDistance * maxCamDistance)
			{
				position = target.Position + Vector2.Normalize(relative_position) * maxCamDistance;
			}

			Vector2 randomPosition = new Vector2();
			float randomRotation = 0.0f;
			if (0 < shakeTimer)
			{
				float strength = shakeTimer * shakeTimer * shakeTimer * 30.0f;
				strength = Math.Min(strength, 60);
				randomPosition = RNG.RandomUnitVector() * strength;
				randomRotation = 0.0f;
			}

			viewMatrix = 
				Matrix.CreateTranslation(-position.X + randomPosition.X, -position.Y + randomPosition.Y, 0) *
				Matrix.CreateRotationZ(rotation + randomRotation) *
				Matrix.CreateTranslation(offset.X / zoom, offset.Y / zoom, 0) *
				Matrix.CreateScale(zoom);
		}

		public Matrix ViewMatrix { get { return viewMatrix; } }
		public GameObject Target { get { return target; } set { target = value; } }
	}
}
