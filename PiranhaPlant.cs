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
        // private float _extensionY;
        private float _jawRotation;
        private float _snapSpeed;
        private float _timer;
        private float _scale;
        private float _timeOffset;
        
        private Matrix _rootMatrix;

        public PiranhaPlant(Texture2D stem, Texture2D jawLeft, Texture2D jawRight, Vector2 basePos, float snapSpeed, float scale, float timeOffset)
        {
            _stemTex = stem;
            _jawLeftTex = jawLeft;
            _jawRightTex = jawRight;
            _basePosition = basePos;
            _snapSpeed = snapSpeed;
            _scale = scale;
            _timeOffset = timeOffset;
        }

        public void Update(GameTime gameTime)
        {
            // We use (_timer + _timeOffset) to ensure unique instances have unique starting positions
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float activeTime = _timer + _timeOffset;
            
            // Level 1: Root Hierarchy (Requirement: Composition of at least two transformations)
            // Using your preferred vertical movement math
            float verticalMove = MathF.Sin(activeTime * 2) * 20f;
            _rootMatrix = Matrix.CreateScale(_scale) * Matrix.CreateTranslation(_basePosition.X, _basePosition.Y + verticalMove, 0);
            
            // Level 2: Local Hierarchy (Requirement: Animation relative to the parent)
            // Using your preferred snap speed and rotation limit
            _jawRotation = MathF.Abs(MathF.Sin(activeTime * _snapSpeed)) * 0.6f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Applying Level 1 transformations via the Root Matrix
            spriteBatch.Begin(transformMatrix: _rootMatrix);

            // Level 1 Object: The Stem (Drawn at local origin 0,0)
            spriteBatch.Draw(_stemTex, Vector2.Zero, Color.White);

            // Level 2 Objects: The Jaws (Drawn relative to the Stem)
            // Keeping your exact offsets and positions
            float leftOffset = 8.5f;
            float verticalPosition = 8f;
            Vector2 jawPivot = new Vector2((_stemTex.Width / 2) - leftOffset, verticalPosition);
                
            Vector2 leftHinge = new Vector2(_jawLeftTex.Width, _jawLeftTex.Height); 
            spriteBatch.Draw(_jawLeftTex, 
                jawPivot, // Positioned relative to Stem local coordinates
                null, Color.White, -_jawRotation, leftHinge, 1.0f, SpriteEffects.None, 0);

            Vector2 rightHinge = new Vector2(0, _jawRightTex.Height);
            spriteBatch.Draw(_jawRightTex, 
                jawPivot, // Positioned relative to Stem local coordinates
                null, Color.White, _jawRotation, rightHinge, 1.0f, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}