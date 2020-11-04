
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Generics;
using reintegrate.Core.Images;

namespace reintegrate.Core.Menus
{
    public class MenuItem : IHaveContent, IUnloadContent, IUpdateThings, IDrawThings
    {
        #region Properties 

        public string LinkType { get; set; }
        public string LinkId { get; set; }
        public Image Image { get; set; }

        #endregion

        #region Public Methods

        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }

        #endregion

    }
}
