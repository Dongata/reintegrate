using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace reintegrate.Core.Images.Effects
{
    public class SpriteSheetEffect : ImageEffect
    {
        private int frameCounter;
        private int frameIndex;

        public SpriteSheetEffect()
        {
            frameCounter = 0;
            frameIndex = 0;
            IsAnimationCompleted = false;
        }

        public Frame CurrentFrame { get; set; }
        public string CurrentState { get; set; }
        public Dictionary<string, List<Frame>> Frames { get; set; }
        public bool IsAnimationCompleted { get; private set; }

        public void SetState(string state)
        {
            if (state != CurrentState)
            {
                frameIndex = 0;
                frameCounter = 0;
                IsAnimationCompleted = false;
                CurrentState = state;
                CurrentFrame = Frames[state][frameIndex];
            }
        }

        public override void LoadContent(Image image)
        {
            base.LoadContent(image);
            CurrentFrame = Frames[CurrentState][frameIndex];
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (frameCounter >= CurrentFrame.Switch)
                {
                    frameCounter = 0;
                    frameIndex += 1;
                    IsAnimationCompleted = false;

                    if (frameIndex >= Frames[CurrentState].Count)
                    {
                        frameIndex = 0;
                        IsAnimationCompleted = true;
                    }

                    
                    CurrentFrame = Frames[CurrentState][frameIndex];
                }

                image.SourceRect = new Rectangle(CurrentFrame.X, CurrentFrame.Y, CurrentFrame.Width, CurrentFrame.Heigth);
            }
        }
    }
}
