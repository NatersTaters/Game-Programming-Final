using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
	class HighScoreScene : GameScene
	{
		private int score;
		private ScoreManager scoreManager;
		private SpriteFont font;

		public HighScoreScene(Game game) : base(game)
		{
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			ScoreManager.Load();

			base.Update(gameTime);
		}
	}
}
