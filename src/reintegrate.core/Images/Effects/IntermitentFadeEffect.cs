using Microsoft.Xna.Framework;

namespace reintegrate.Core.Images.Effects
{
    public class IntermitentFadeEffect : FadeEffect
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.isActive)
            {
                if ((Increase && image.Alpha >= 1) || (!Increase && image.Alpha <= 0))
                {
                    Increase = !Increase;
                }
            }
        }
    }
}
