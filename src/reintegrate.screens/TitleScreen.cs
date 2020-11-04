using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Menus;
using reintegrate.Core.Screens;

namespace reintegrate.GameScreens
{
    public class TitleScreen : GameScreen
    {
        private MenuManager menuManager;

        public override void LoadContent()
        {
            menuManager = new MenuManager();
            menuManager.LoadContent("Load/TitleMenu");
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            menuManager.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            menuManager.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menuManager.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
