using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using reintegrate.Core.Images;
using reintegrate.Core.Inputs;
using reintegrate.Core.Screens;

namespace reintegrate.GameScreens
{
    public class SplashScreen : GameScreen
    {
        public Image Image { get; set; }

        public override void LoadContent()
        {
            Image.LoadContent();
            Image.ActivateEffect("FadeEffect");
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            Image.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
            base.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Space, Keys.Enter))
            {
                ScreenManager.Instance.ChangeScreen("TitleScreen");
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
