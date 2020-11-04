using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Generics;
using reintegrate.Core.Images;

namespace reintegrate.Core.Maps
{
    public class Tile :
        IUnloadContent,
        IDrawThings,
        IHaveContent,
        IUpdateThings
    {
        public Image Image { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool ShouldCollide { get; set; }

        public void LoadContent()
        {
            Image.LoadContent();
            Image.Position = new Vector2(X * Image.Width, Y * Image.Height);
        }

        public void UnloadContent() => Image.UnloadContent();

        public void Update(GameTime gameTime) => Image.Update(gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
