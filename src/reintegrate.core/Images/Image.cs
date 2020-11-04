using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using reintegrate.Core.Generics;
using reintegrate.Core.Images.Effects;
using reintegrate.Core.Screens;
using System;
using System.Collections.Generic;

namespace reintegrate.Core.Images
{
    public class Image :
        IHaveContent,
        IUnloadContent,
        IDrawThings,
        IUpdateThings
    {

        #region Fields

        private Vector2 origin;
        private ContentManager content;
        private RenderTarget2D renderTarget;
        private SpriteFont font;
        private Dictionary<string, ImageEffect> effectList;

        #endregion

        #region Constructor

        public Image()
        {
            Alpha = 1.0f;
            Path = string.Empty;
            Text = string.Empty;
            FontName = "Fonts/Calibri";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            SourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        #endregion

        #region Properties

        public int Width => Texture.Width;

        public int Height => Texture.Height;

        public float Alpha { get; set; }

        public string Text { get; set; }

        public string FontName { get; set; }

        public string Path { get; set; }

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Scale { get; set; }

        public Rectangle SourceRect { get; set; }

        public FadeEffect FadeEffect
        {
            get
            {
                if (effectList.ContainsKey("FadeEffect"))
                {
                    return effectList["FadeEffect"] as FadeEffect;
                }

                return null;
            }
            set
            {
                if (effectList.ContainsKey("FadeEffect"))
                {
                    effectList["FadeEffect"] = value;
                }
                else
                {
                    effectList.Add("FadeEffect", value);
                }
            }
        }

        public IntermitentFadeEffect IntermitentFadeEffect
        {
            get
            {
                if (effectList.ContainsKey("IntermitentFadeEffect"))
                {
                    return effectList["IntermitentFadeEffect"] as IntermitentFadeEffect;
                }

                return null;
            }
            set
            {
                if (effectList.ContainsKey("IntermitentFadeEffect"))
                {
                    effectList["IntermitentFadeEffect"] = value;
                }
                else
                {
                    effectList.Add("IntermitentFadeEffect", value);
                }
            }
        }

        public SpriteSheetEffect SpriteSheetEffect
        {
            get
            {
                if (effectList.ContainsKey("SpriteSheetEffect"))
                {
                    return effectList["SpriteSheetEffect"] as SpriteSheetEffect;
                }

                return null;
            }
            set
            {
                if (effectList.ContainsKey("SpriteSheetEffect"))
                {
                    effectList["SpriteSheetEffect"] = value;
                }
                else
                {
                    effectList.Add("SpriteSheetEffect", value);
                }
            }
        }

        #endregion

        #region Public Methods

        public void ActivateEffects()
        {
            foreach (var effect in effectList)
            {
                if (effect.Value != null)
                {
                    ActivateEffect(effect.Key);
                }
            }
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].Activate();
            }
        }

        public void DeactivateEffects()
        {
            foreach (var effect in effectList)
            {
                if (effect.Value != null)
                {
                    DeactivateEffect(effect.Key);
                }
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].Deactivate();
                effectList[effect].UnloadContent();
            }
        }

        public void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (!string.IsNullOrEmpty(Path))
            {
                Texture = content.Load<Texture2D>(Path);
            }

            font = content.Load<SpriteFont>(FontName);
            var dimensions = GetDimensions();

            if (SourceRect == Rectangle.Empty)
            {
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }

            renderTarget = new RenderTarget2D(
                ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
            {
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            }

            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);

            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            foreach (var effect in effectList)
            {
                effect.Value?.LoadContent(this);
            }
        }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
            {
                effect.Value?.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                effect.Value?.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);

            spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha, 0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
        #endregion

        #region Private Methods

        private Vector2 GetDimensions()
        {
            var textDimension = font.MeasureString(Text);

            var dimensions = Vector2.Zero;

            if (Texture != null)
            {
                dimensions.X += Texture.Width;
                dimensions.Y += Math.Max(Texture.Height, textDimension.Y);
            }
            else
            {
                dimensions.Y += textDimension.Y;
            }

            dimensions.X += textDimension.X;
            return dimensions;
        }

        #endregion
    }
}
