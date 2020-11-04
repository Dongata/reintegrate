using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Images;
using System;

namespace reintegrate.Core.Transitioners
{
    public class FadeTransitioner
    {
        public Image Image;
        public bool IsTransitioning = false;

        public EventHandler OnTransitionStarts;
        public EventHandler OnTransitionFades;
        public EventHandler OnTransitionEnds;

        public void ApplyTransition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                Image.Update(gameTime);
                if (Image.Alpha >= 1.0f)
                {
                    OnTransitionFades?.Invoke(this, null);
                    Image.FadeEffect.Increase = false;
                }

                if (Image.Alpha <= 0.0f)
                {
                    OnTransitionEnds?.Invoke(this, null);
                    Image.FadeEffect.Increase = true;
                    Image.FadeEffect.Deactivate();
                    IsTransitioning = false;
                }
            }
        }

        public void Start()
        {
            if (!IsTransitioning)
            {
                OnTransitionStarts?.Invoke(this, null);
                Image.FadeEffect.Activate();
                IsTransitioning = true;
            }
        }
        public void LoadContent()
        {
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
