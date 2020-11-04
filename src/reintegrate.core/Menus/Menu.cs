using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using reintegrate.Core.Generics;
using reintegrate.Core.Inputs;
using reintegrate.Core.Screens;
using System;
using System.Collections.Generic;

namespace reintegrate.Core.Menus
{
    public class Menu : IHaveContent, IUnloadContent, IUpdateThings, IDrawThings
    {
        #region Fields

        private string id;
        private int itemNumber;

        #endregion

        #region Events

        public event EventHandler OnMenuChange;

        #endregion

        #region Properties

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        public string Axis { get; set; }

        public IList<MenuItem> Items { get; set; }

        public string CurrentLinkId => Items[itemNumber].LinkId;

        #endregion

        public Menu()
        {
            id = string.Empty;
            itemNumber = 0;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public bool IsScreenSelected()
        {
            return Items[itemNumber].LinkType == "Screen";
        }

        public bool IsMenuSelected()
        {
            return Items[itemNumber].LinkType == "Menu";
        }

        public void LoadContent()
        {
            foreach (var item in Items)
            {
                item.LoadContent();
            }

            AllignItems();
            Items[itemNumber].Image.ActivateEffects();
        }

        public void UnloadContent()
        {
            foreach (var item in Items)
            {
                item.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            var lastItemNumber = itemNumber;

            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Left))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Right))
                {
                    itemNumber--;
                }
            }

            if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                {
                    itemNumber--;
                }
            }

            if (itemNumber < 0)
            {
                itemNumber = 0;
            }

            if (itemNumber > Items.Count - 1)
            {
                itemNumber = Items.Count - 1;
            }

            if (lastItemNumber != itemNumber)
            {
                Items[lastItemNumber].Image.DeactivateEffects();
                Items[lastItemNumber].Image.Alpha = 1.0f;
                Items[lastItemNumber].UnloadContent();

                Items[itemNumber].LoadContent();
                Items[itemNumber].Image.ActivateEffects();
            }

            Items[itemNumber].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in Items)
            {
                item.Draw(spriteBatch);
            }
        }

        #region Private Methods

        private void AllignItems()
        {
            var dimensions = Vector2.Zero;
            foreach (var item in Items)
            {
                dimensions += 
                    new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height);
            }

            dimensions = new Vector2(
                (ScreenManager.Instance.Dimensions.X - dimensions.X) / 2,
                (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2
            );

            foreach (var item in Items)
            {
                if (Axis == "X")
                {
                    item.Image.Position = new Vector2(
                        dimensions.X,
                        (ScreenManager.Instance.Dimensions.Y - item.Image.SourceRect.Height) / 2
                    );
                }

                if (Axis == "Y")
                {
                    item.Image.Position = new Vector2(
                        (ScreenManager.Instance.Dimensions.X - item.Image.SourceRect.Width) / 2,
                        dimensions.Y
                    );
                }

                dimensions +=
                    new Vector2(item.Image.SourceRect.Width, item.Image.SourceRect.Height);
            }
        }

        #endregion
    }
}
