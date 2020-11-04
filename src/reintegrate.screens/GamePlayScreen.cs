using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Maps;
using reintegrate.Core.Readers;
using reintegrate.Core.Screens;
using reintegrate.Players;
using System.Collections.Generic;

namespace reintegrate.GameScreens
{
    public class GamePlayScreen : GameScreen
    {
        private readonly Player player;
        private readonly List<Map> maps;
        private Map currentMap;

        public GamePlayScreen()
        {
            player = JsonReader.Load<Player>("Load/Player");
            maps = new List<Map>();
        }

        public Dictionary<string, string> Maps { get; set; }

        public string DefaultMap { get; set; }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            currentMap.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
        public override void LoadContent()
        {
            base.LoadContent();

            foreach (var map in Maps)
            {
                var realMap = JsonReader.Load<Map>(map.Value);
                realMap.LoadContent();
                maps.Add(realMap);
            }

            currentMap = maps.Find(a => a.Name == DefaultMap);

            player.LoadContent();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();

            foreach (var map in maps)
            {
                map.UnloadContent();
            }

            player.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            currentMap.Update(gameTime);
            player.Update(gameTime);
        }
    }
}
