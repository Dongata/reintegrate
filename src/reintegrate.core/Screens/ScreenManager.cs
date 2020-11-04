using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Generics;
using reintegrate.Core.Readers;
using reintegrate.Core.Transitioners;
using System;
using System.Collections.Generic;

namespace reintegrate.Core.Screens
{
    public class ScreenManager : 
        IUseContent, 
        IUnloadContent, 
        IDrawThings, 
        IUpdateThings
    {
        #region Fields

        private Dictionary<string, GameScreen> screens;
        private string newScreen;
        #endregion

        #region Properties

        public Vector2 Dimensions { get; set; }

        public ContentManager Content { get; private set; }

        public GameScreen CurrentScreen { get; set; }

        public GraphicsDevice GraphicsDevice { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public FadeTransitioner Transition { get; set; }

        public bool IsTransitioning => Transition.IsTransitioning;
        #endregion

        #region Singelton Components

        private static ScreenManager instance;

        private ScreenManager()
        {
            screens = new Dictionary<string, GameScreen>();            
        }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = JsonReader.Load<ScreenManager>("Load/ScreenManager");
                    instance.Transition.Image.Scale = instance.Dimensions;
                    instance.Transition.OnTransitionFades = OnTransitionFades;
                }

                return instance;
            }
        }

        #endregion

        #region Public Methods

        public void AddScreen(string name, GameScreen gameScreen)
        {
            screens.Add(name, gameScreen);
        }

        public void ChangeScreen(string screenName)
        {
            if (!screens.ContainsKey(screenName))
                throw new KeyNotFoundException(screenName);

            Transition.Start();
            newScreen = screenName;

        }

        public void LoadContent(ContentManager contentManager)
        {
            Content = new ContentManager(contentManager.ServiceProvider, "Content");
            CurrentScreen.LoadContent();
            Transition.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
            Transition.ApplyTransition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
            Transition.Draw(spriteBatch);
        }
        #endregion

        #region Private Methods

        private static void OnTransitionFades(object sender, EventArgs e)
        {
            instance.CurrentScreen.UnloadContent();
            instance.CurrentScreen = instance.screens[instance.newScreen];
            instance.CurrentScreen.LoadContent();
        }

        #endregion
    }
}
