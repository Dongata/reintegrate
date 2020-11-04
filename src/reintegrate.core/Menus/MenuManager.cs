using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using reintegrate.Core.Images;
using reintegrate.Core.Images.Effects;
using reintegrate.Core.Inputs;
using reintegrate.Core.Readers;
using reintegrate.Core.Screens;
using reintegrate.Core.Transitioners;

namespace reintegrate.Core.Menus
{
    public class MenuManager
    {
        private Menu menu;
        private bool isTransitioning => Transition.IsTransitioning;

        public MenuManager()
        {
            Transition = new FadeTransitioner();
            Transition.Image = new Image();
            Transition.Image.Path = "ScreenManager/BlackDot";
            Transition.Image.Alpha = 0;
            Transition.Image.FadeEffect = new FadeEffect();
            Transition.Image.FadeEffect.FadeSpeed = 1;
            Transition.Image.FadeEffect.Increase = true;
        }

        public FadeTransitioner Transition { get; set; }

        public void LoadContent(string menuPath)
        {
            Transition.LoadContent();
            Transition.Image.Scale = ScreenManager.Instance.Dimensions;
            Transition.OnTransitionFades = OnTransitionFades;

            if (!string.IsNullOrEmpty(menuPath))
            {
                menu = JsonReader.Load<Menu>(menuPath);
                menu.OnMenuChange += Menu_OnMenuChange;
                menu.LoadContent();
                menu.Id = menuPath;
            }
        }

        private void OnTransitionFades(object sender, EventArgs e)
        {
            LoadContent(menu.CurrentLinkId);
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
            Transition.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
            Transition.ApplyTransition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
            Transition.Draw(spriteBatch);

            if (InputManager.Instance.KeyPressed(Keys.Enter) && !isTransitioning)
            {
                if(menu.IsScreenSelected())
                {
                    ScreenManager.Instance.ChangeScreen(menu.CurrentLinkId);
                }

                if (menu.IsMenuSelected())
                {
                    Transition.Start();
                }
            }
        }

        private void Menu_OnMenuChange(object sender, System.EventArgs e)
        {
            menu.UnloadContent();
            menu = JsonReader.Load<Menu>(menu.Id);
            menu.LoadContent();
        }
    }
}
