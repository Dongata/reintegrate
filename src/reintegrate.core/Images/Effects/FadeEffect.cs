using Microsoft.Xna.Framework;

namespace reintegrate.Core.Images.Effects
{
    public class FadeEffect : ImageEffect
    {
        public float FadeSpeed { get; set; }
        public bool Increase { get; set; }

        public FadeEffect()
        {
            FadeSpeed = 1;
            Increase = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.isActive)
            {
                if (!Increase)
                    image.Alpha -= FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    image.Alpha += FadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (image.Alpha < 0)
                    image.Alpha = 0;

                if (image.Alpha > 1)
                    image.Alpha = 1;
            }   
        }
    }
}
