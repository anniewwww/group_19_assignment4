using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace group_19_assignment4
{
    public class PiranhaPlant
    {
        private Texture2D _stemTex;
        private Texture2D _jawRightTex;
        private Texture2D _jawLeftTex;
        
        private Vector2 _basePosition;
        private float _extensionY;
        private float _jawRotation;
        private float _snapSpeed;
        private float _timer;
        
        private Matrix _rootMatrix;

        public PiranhaPlant(Texture2D stem, Texture2D jawLeft, Texture2D jawRight, Vector2 basePos, float snapSpeed)
        {
            _stemTex = stem;
            _jawLeftTex = jawLeft;
            _jawRightTex = jawRight;
            _basePosition = basePos;
            _snapSpeed = snapSpeed;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            _extensionY = MathF.Sin(_timer * 3) * 17f - 35f; 
            
            _rootMatrix = Matrix.CreateScale(0.15f) * Matrix.CreateTranslation(_basePosition.X, _basePosition.Y + _extensionY, 0);

            _jawRotation = MathF.Abs(MathF.Sin(_timer * _snapSpeed)) * 0.6f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: _rootMatrix);

            spriteBatch.Draw(_stemTex, Vector2.Zero, Color.White);

            float leftOffset = 8.5f;
            float verticalPosition = 8f;
                
            Vector2 leftHinge = new Vector2(_jawLeftTex.Width, _jawLeftTex.Height); 
            spriteBatch.Draw(_jawLeftTex, 
                new Vector2((_stemTex.Width / 2) - leftOffset, verticalPosition),
                null, Color.White, -_jawRotation, leftHinge, 1.0f, SpriteEffects.None, 0); // left
            Vector2 rightHinge = new Vector2(0, _jawRightTex.Height);
            spriteBatch.Draw(_jawRightTex, 
                new Vector2((_stemTex.Width / 2) - leftOffset, verticalPosition),
                null, Color.White, _jawRotation, rightHinge, 1.0f, SpriteEffects.None, 0); // right
            spriteBatch.End();
        }
    }
}




