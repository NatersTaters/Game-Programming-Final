using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public class ActionScene : GameScene
    {
        public ActionScene (Game game): base(game)
        {
          
        }

        public override void Initialize()
        {
			this.AddComponent(new Background(Game, Game.Content.Load<Texture2D>($"Images/background"), new Vector2(3, 0), BackType.Sky));
			this.AddComponent(new Background(Game, Game.Content.Load<Texture2D>($"Images/tile"), new Vector2(2, 0), BackType.Tile));
			this.AddComponent(new CoinManager(Game, this));
			this.AddComponent(new BombManager(Game, this));

			Cat cat = new Cat(Game);
			this.AddComponent(cat);
			Game.Services.AddService<Cat>(cat);

			base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled )
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
            }

            base.Update(gameTime);
        }

       
    }
}
