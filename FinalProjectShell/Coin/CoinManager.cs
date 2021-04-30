using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace FinalProjectShell
{
	class CoinManager : GameComponent
	{
		const double CREATION_INTERVAL = 1.5;
		double timer = 0.0;
		Random random = new Random();

		GameScene parent;

		public CoinManager(Game game, GameScene parent) : base(game)
		{
			this.parent = parent;
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			timer += gameTime.ElapsedGameTime.TotalSeconds;
			if (timer >= CREATION_INTERVAL)
			{
				timer = 0;
				parent.AddComponent(new Coin(Game));

			}
			base.Update(gameTime);
		}
	}
}
