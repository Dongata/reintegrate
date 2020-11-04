using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Generics;

namespace reintegrate.Core.Maps
{
    public class Map : 
        IUnloadContent, 
        IDrawThings,
        IHaveContent,
        IUpdateThings
    { 
        public Map()
        {
            GroundLayer = new Layer();
            MiddleLayer = new Layer();
            TopLayer = new Layer();
        }

        #region Properties

        public string Name { get; set; }

        public Layer GroundLayer { get; set; }

        public Layer MiddleLayer { get; set; }

        public Layer TopLayer { get; set; }

        #endregion

        #region Public methods

        public void Draw(SpriteBatch spriteBatch)
        {
            GroundLayer.Draw(spriteBatch);
            MiddleLayer.Draw(spriteBatch);
            TopLayer.Draw(spriteBatch);
        }

        public void LoadContent()
        {
            GroundLayer.LoadContent();
            MiddleLayer.LoadContent();
            TopLayer.LoadContent();
        }

        public void UnloadContent()
        {
            GroundLayer.UnloadContent();
            MiddleLayer.UnloadContent();
            TopLayer.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            GroundLayer.Update(gameTime);
            MiddleLayer.Update(gameTime);
            TopLayer.Update(gameTime);
        }

        #endregion
    }
}
