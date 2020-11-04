using Microsoft.Xna.Framework;
using reintegrate.Core.Generics;

namespace reintegrate.Core.Images.Effects
{
    public abstract class ImageEffect : 
        IUpdateThings, 
        IUnloadContent
    {
        protected Image image;
        protected bool isActive;

        public ImageEffect()
        {
            isActive = false;
        }

        public virtual void Activate()
        {
            isActive = true;
        }

        public virtual void Deactivate()
        {
            isActive = false;
        }

        public virtual void LoadContent(Image image)
        {
            this.image = image;
        }

        public virtual void UnloadContent()
        {
        }

        public abstract void Update(GameTime gameTime);

    }
}
