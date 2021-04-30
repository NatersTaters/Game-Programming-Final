using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace FinalProjectShell
{
	class Bomb : DrawableGameComponent
	{
		List<Texture2D> textureBomb = new List<Texture2D>();
		Vector2 bombPosition;

		int currentFrame = 0;
		int timeSinceLastFrame = 0;
		int millisecondsPerFrame = 50;
		const int BOMB_FRAME_COUNT = 2;

		float speed = 2f;
		const double FRAME_RATE = 0.1;

		public Rectangle BombRectangle
		{
			get { return new Rectangle((int)bombPosition.X, (int)bombPosition.Y, textureBomb[0].Width, textureBomb[0].Height); }
		}


		public Bomb(Game game) : base(game)
		{
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
			sb.Begin();

			sb.Draw(textureBomb[currentFrame], bombPosition, Color.White);

			sb.End();
			base.Draw(gameTime);
		}

		public override void Update(GameTime gameTime)
		{
			timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
			if (timeSinceLastFrame > millisecondsPerFrame)
			{
				timeSinceLastFrame -= millisecondsPerFrame;

				if (++currentFrame >= BOMB_FRAME_COUNT)
				{
					currentFrame = 0;
				}
			}

			bombPosition.X -= speed;
			bombPosition.Y = MathHelper.Clamp(bombPosition.Y, 0, GraphicsDevice.Viewport.Height - textureBomb[currentFrame].Height - 72);

			CheckCollisionWithPlayer();

			base.Update(gameTime);
		}

		protected override void LoadContent()
		{
			for (int i = 1; i <= BOMB_FRAME_COUNT; i++)
			{
				textureBomb.Add(Game.Content.Load<Texture2D>($"Images/bomb{i}"));
			}

			Random random = new Random();
			bombPosition = new Vector2(GraphicsDevice.Viewport.Width + 100, random.Next(0, GraphicsDevice.Viewport.Width - textureBomb[currentFrame].Width));

			base.LoadContent();
		}

		/// <summary>
		/// Will constantly check for collision with the player character
		/// </summary>
		private void CheckCollisionWithPlayer()
		{
			Cat cat = Game.Services.GetService<Cat>();
			if (XNA2DCollisionDetection.CollisionDetection2D.BoundingRectangles(cat.CatRectangle, this.BombRectangle))
			{
				cat.CollidedBomb();
				Game.Components.Remove(this);
			}
		}
	}
}
