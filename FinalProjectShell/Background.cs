using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FinalProjectShell
{
	enum BackType
	{
		Sky,
		Tile
	}

	class Background : DrawableGameComponent
	{
		private static int backgroundCount = 0;
		Texture2D texture;
		Vector2 velocity;
		Vector2 position = Vector2.Zero;
		BackType backgroundType;

		List<Rectangle> textureTiles;

		public Background(Game game, Texture2D texture, Vector2 velocity, BackType backgroundType) : base(game)
		{
			DrawOrder = backgroundCount;
			backgroundCount++;

			this.texture = texture;
			this.velocity = velocity;
			this.backgroundType = backgroundType;

			textureTiles = CalculateBackgroundRectangleList();
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
			sb.Begin();
			foreach (Rectangle rect in textureTiles)
			{
				sb.Draw(texture, rect, Color.White);

			}
			sb.End();
			base.Draw(gameTime);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			for (int i = 0; i < textureTiles.Count; i++)
			{
				Rectangle rect = textureTiles[i];
				rect.Location -= velocity.ToPoint();
				textureTiles[i] = rect;
			}

			Rectangle firstRect = textureTiles[0];
			if (firstRect.Right < 0)
			{
				textureTiles.RemoveAt(0);
				Rectangle lastRect = textureTiles[textureTiles.Count - 1];
				firstRect.X = lastRect.Right;

				textureTiles.Add(firstRect);
			}

			base.Update(gameTime);
		}

		protected override void LoadContent()
		{
			base.LoadContent();
		}

		private List<Rectangle> CalculateBackgroundRectangleList()
		{
			List<Rectangle> neededRectangles = new List<Rectangle>();

			int rectangleCount = Game.GraphicsDevice.Viewport.Width / texture.Width + 2;
			int yPosition = CalculateYPosition();

			for (int i = 0; i < rectangleCount; i++)
			{
				neededRectangles.Add(new Rectangle(texture.Width * i, yPosition, texture.Width, texture.Height));
			}

			return neededRectangles;
		}

		private int CalculateYPosition()
		{
			switch (backgroundType)
			{
				default:
				case BackType.Sky:
					return 0;
				case BackType.Tile:
					return Game.GraphicsDevice.Viewport.Height - texture.Height;
			}
		}
	}
}
