using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalProjectShell
{
	enum CatState
	{
		Walking,
		Jump
	}
	public class Cat : DrawableGameComponent
	{
		Texture2D textureJump;
		Texture2D textureExplode;
		SoundEffect soundFxCoin;
		SoundEffect soundFxBomb;
		List<Texture2D> textureWalk = new List<Texture2D>();
		Vector2 position;
		public Vector2 Velocity;

		int currentFrame = 0;
		int timeSinceLastFrame = 0;
		int millisecondsPerFrame = 50;
		const int WALK_FRAME_COUNT = 6;
		float jumpSpeed = 3f;

		bool alive = true;

		private int score;
		private ScoreManager scoreManager;
		private SpriteFont font;

		CatState catState = CatState.Walking;

		public Rectangle CatRectangle
		{
			get { return new Rectangle((int)position.X, (int)position.Y, textureWalk[0].Width, textureWalk[0].Height); }
		}

		public Cat(Game game) : base(game)
		{

		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
			sb.Begin();

			if(alive)
			{
				if (catState == CatState.Jump)
				{
					DrawSingleFrame(textureJump, sb);
				}
				else
				{
					sb.Draw(textureWalk[currentFrame], position, null, Color.White, 0f, Vector2.Zero, 1.0f, catState == CatState.Walking ? SpriteEffects.None : SpriteEffects.None, 0f);
				}
			}
			else
			{
				DrawSingleFrame(textureExplode, sb);
			}

			sb.DrawString(font, "Score: " + score, new Vector2(10, 10), Color.Red);
			sb.DrawString(font, "Press esc to exit game", new Vector2(10, 30), Color.Red);

			sb.End();
			base.Draw(gameTime);
		}

		public override void Update(GameTime gameTime)
		{
			catState = CatState.Walking;

			if (Keyboard.GetState().IsKeyDown(Keys.Up))
			{
				catState = CatState.Jump;
				position.Y -= jumpSpeed * 1.5f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
			{
				position.Y += jumpSpeed * 2;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.Up))
			{
				position.Y += jumpSpeed;
			}

			position.X = MathHelper.Clamp(position.X, 0, GraphicsDevice.Viewport.Width - textureWalk[0].Width);
			position.Y = MathHelper.Clamp(position.Y, 0, GraphicsDevice.Viewport.Height - textureWalk[0].Height - 72);

			timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
			if (timeSinceLastFrame > millisecondsPerFrame)
			{
				timeSinceLastFrame -= millisecondsPerFrame;

				if (++currentFrame >= WALK_FRAME_COUNT)
				{
					currentFrame = 0;
				}
			}

			base.Update(gameTime);
		}

		protected override void LoadContent()
		{
			scoreManager = ScoreManager.Load();
			font = Game.Content.Load<SpriteFont>("Fonts/regularFont");

			for (int i = 1; i <= WALK_FRAME_COUNT; i++)
			{
				textureWalk.Add(Game.Content.Load<Texture2D>($"Images/cat_walk{i:D2}"));
			}

			textureJump = Game.Content.Load<Texture2D>("Images/cat_jump");
			textureExplode = Game.Content.Load<Texture2D>("Images/explosion");
			soundFxCoin = Game.Content.Load<SoundEffect>("Sounds/Picked Coin Echo 2");
			soundFxBomb = Game.Content.Load<SoundEffect>("Sounds/BangShort");

			position.X = textureWalk[0].Bounds.Center.X - GraphicsDevice.Viewport.Width;
			position.Y = GraphicsDevice.Viewport.Height / 1.3f - textureWalk[0].Bounds.Center.Y;

			base.LoadContent();
		}

		private void DrawSingleFrame(Texture2D activeTexture, SpriteBatch sb)
		{
			sb.Draw(activeTexture, position, Color.White);
		}

		/// <summary>
		/// Will handle the procedure for when the player chacter collides with a coin
		/// </summary>
		internal void CollidedCoin()
		{
			soundFxCoin.Play();
			score++;
		}

		/// <summary>
		/// Will handle the procedure for when the player chacter collides with a bomb
		/// </summary>
		internal void CollidedBomb()
		{
			if (alive)
			{
				soundFxBomb.Play();
				alive = false;
				scoreManager.Add(new Score() { Value = score });
				ScoreManager.Save(scoreManager);
			}
		}
	}
}
