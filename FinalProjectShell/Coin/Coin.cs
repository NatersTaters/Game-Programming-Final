using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace FinalProjectShell
{
	class Coin : DrawableGameComponent
	{
		List<Texture2D> textureCoin = new List<Texture2D>();
		const int COIN_FRAME_COUNT = 6;

		Vector2 coinPosition;

		int currentFrame = 0;
		int timeSinceLastFrame = 0;
		int millisecondsPerFrame = 50;

		float speed = 2f;
		const double FRAME_RATE = 0.1;

		public Rectangle CoinRectangle
		{
			get { return new Rectangle((int)coinPosition.X, (int)coinPosition.Y, textureCoin[0].Width, textureCoin[0].Height); }
		}

		public Coin(Game game) : base(game)
		{

		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
			sb.Begin();

			sb.Draw(textureCoin[currentFrame], coinPosition, Color.White);

			sb.End();
			base.Draw(gameTime);
		}

		public override void Update(GameTime gameTime)
		{
			timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
			if (timeSinceLastFrame > millisecondsPerFrame)
			{
				timeSinceLastFrame -= millisecondsPerFrame;

				if (++currentFrame >= COIN_FRAME_COUNT)
				{
					currentFrame = 0;
				}
			}

			coinPosition.X -= speed;
			coinPosition.Y = MathHelper.Clamp(coinPosition.Y, 0, GraphicsDevice.Viewport.Height - textureCoin[currentFrame].Height - 72);

			CheckCollisionWithPlayer();

			base.Update(gameTime);
		}

		protected override void LoadContent()
		{
			for (int i = 1; i <= COIN_FRAME_COUNT; i++)
			{
				textureCoin.Add(Game.Content.Load<Texture2D>($"Images/coin{i}"));
			}

			Random random = new Random();
			coinPosition = new Vector2(GraphicsDevice.Viewport.Width, random.Next(0, GraphicsDevice.Viewport.Height - textureCoin[currentFrame].Height));
			base.LoadContent();
		}

		/// <summary>
		/// Will constantly check for collision with the player character
		/// </summary>
		private void CheckCollisionWithPlayer()
		{
			Cat cat = Game.Services.GetService<Cat>();
			if (XNA2DCollisionDetection.CollisionDetection2D.BoundingRectangles(cat.CatRectangle, this.CoinRectangle))
			{
				cat.CollidedCoin();
				Game.Components.Remove(this);
			}
		}
	}
}
