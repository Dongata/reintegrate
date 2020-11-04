using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Generics;
using System.Collections.Generic;

namespace reintegrate.Core.Maps
{
    public class Layer :
        IUnloadContent,
        IDrawThings,
        IUpdateThings,
        IHaveContent
    {
        public Layer()
        {
            Tiles = new List<Tile>();
        }

        public List<Tile> Tiles { get; set; }

        public void Draw(SpriteBatch spriteBatch) => Tiles.ForEach(a => a.Draw(spriteBatch));

        public void LoadContent() => Tiles.ForEach(a => a.LoadContent());

        public void UnloadContent() => Tiles.ForEach(a => a.UnloadContent());

        public void Update(GameTime gameTime) => Tiles.ForEach(a => a.Update(gameTime));
    }
}
