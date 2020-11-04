using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using reintegrate.Core.Generics;
using reintegrate.Core.Images;
using reintegrate.Core.Inputs;

namespace reintegrate.Players
{
    public class Player :
        IHaveContent,
        IUnloadContent,
        IDrawThings,
        IUpdateThings
    {
        #region Constants

        private const string SpellCast = "SpellCast";
        private const string Thrust = "Thrust";
        private const string Walk = "Walk";
        private const string Idle = "Idle";

        private const string Back = "Back";
        private const string Front = "Front";
        private const string Right = "Right";
        private const string Left = "Left";

        #endregion


        private Vector2 _velocity;
        private readonly InputManager _inputManager;
        private string action;
        private string facing;

        public Player()
        {
            _velocity = Vector2.Zero;
            _inputManager = InputManager.Instance;
            action = Idle;
            facing = Front;
        }

        public Image Image { get; set; }
        public float MovementSpeed { get; set; }

        public void LoadContent()
        {
            Image.LoadContent();
            Image.ActivateEffect("SpriteSheetEffect");
        }

        public void UnloadContent() => Image.UnloadContent();

        public void Update(GameTime gameTime)
        {
            if (action != SpellCast && action != Thrust)
            {

                if (_inputManager.KeyDown(Keys.S))
                {
                    _velocity.Y = MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    action = Walk;
                    facing = Front;
                }
                else if (_inputManager.KeyDown(Keys.W))
                {
                    _velocity.Y = -MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    action = Walk;
                    facing = Back;
                }
                else
                {
                    _velocity.Y = 0;
                }

                if (_inputManager.KeyDown(Keys.A))
                {
                    _velocity.X = -MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    action = Walk;
                    facing = Right;
                }
                else if (_inputManager.KeyDown(Keys.D))
                {
                    _velocity.X = MovementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    action = Walk;
                    facing = Left;
                }
                else
                {
                    _velocity.X = 0;
                }
            }

            if (_velocity.X == 0 && _velocity.Y == 0)
            {
                if ((action == SpellCast || action == Thrust))
                {
                    if (Image.SpriteSheetEffect.IsAnimationCompleted)
                    {
                        action = Idle;
                    }
                }
                else
                {
                    action = Idle;
                }
            }

            if (_inputManager.KeyDown(Keys.M))
            {
                _velocity.X = 0;
                _velocity.Y = 0;
                action = SpellCast;
            }

            if (_inputManager.KeyDown(Keys.N))
            {
                _velocity.X = 0;
                _velocity.Y = 0;
                action = Thrust;
            }

            Image.SpriteSheetEffect.SetState(action + facing);

            Image.Position += _velocity;

            Image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) => Image.Draw(spriteBatch);
    }
}
