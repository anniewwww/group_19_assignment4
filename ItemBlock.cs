using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace group_19_assignment4
{
    public class ItemBlock
    {
        private readonly Texture2D _blockTexture;
        private readonly Texture2D _questionTexture;
        private Color _blockColor;
        private Color _questionColor;

        private readonly Vector2 _startPosition;
        private readonly float _bobDistance;
        private readonly float _bobSpeed;

        private readonly float _baseBlockScale;
        private readonly float _scaleAmplitude;
        private readonly float _scaleSpeed;

        private readonly float _baseQuestionScale;
        private readonly float _questionScaleAmplitude;
        private readonly float _questionPulseSpeed;
        
        // timers
        private float _bobTime;
        private float _scaleTime;
        private float _questionTime;

        private Vector2 _currentPosition;
        private float _currentBlockScale;
        private float _currentQuestionScale;

        private readonly Vector2 _blockOrigin;
        private readonly Vector2 _questionOrigin;

        public Matrix RootTransform { get; private set; }
        
        public ItemBlock(
            Texture2D blockTexture,
            Texture2D questionTexture,
            Vector2 basePosition,
            Color blockColor,
            Color questionColor,
            float bobAmplitude = 5f,
            float bobSpeed = 2.5f,
            float baseBlockScale = 1.0f,
            float scaleAmplitude = .10f,
            float scaleSpeed = 2.0f,
            float baseQuestionScale = 1.0f,
            float questionScaleAmplitude = .20f,
            float questionPulseSpeed = 4.0f)
        {
            _blockTexture = blockTexture;
            _questionTexture = questionTexture;
            
            _blockColor = blockColor;
            _questionColor = questionColor;
            
            _startPosition = basePosition;
            _bobDistance = bobAmplitude;
            _bobSpeed = bobSpeed;
            
            _baseBlockScale = baseBlockScale;
            _scaleAmplitude = scaleAmplitude;
            _scaleSpeed = scaleSpeed;
            
            _baseQuestionScale = baseQuestionScale;
            _questionScaleAmplitude = questionScaleAmplitude;
            _questionPulseSpeed = questionPulseSpeed;
            
            _currentPosition = _startPosition;
            _currentBlockScale = _baseBlockScale;
            _currentQuestionScale = _baseQuestionScale;
            
            _blockOrigin = new Vector2(_blockTexture.Width / 2f, _blockTexture.Height / 2f);
            _questionOrigin = new Vector2(_questionTexture.Width / 2f, _questionTexture.Height / 2f);

            BuildRootTransform();
        }

        public void SetColors(Color block, Color question)
        {
            _blockColor = block;
            _questionColor = question;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Move(dt);
            Enlarge(dt);
            PulseQuestion(dt);

            BuildRootTransform();
        }

        private void Move(float dt)
        {
            _bobTime += dt * _bobSpeed;
            float yOffset = (float)Math.Sin(_bobTime) * _bobDistance;
            _currentPosition = _startPosition + new Vector2(0f, yOffset);
        }

        private void Enlarge(float dt)
        {
            _scaleTime += dt * _scaleSpeed;
            float pulse = (float)Math.Sin(_scaleTime) * _scaleAmplitude;
            _currentBlockScale = _baseBlockScale + pulse;
        }

        private void PulseQuestion(float dt)
        {
            _questionTime += dt * _questionPulseSpeed;
            float pulse = (float)Math.Sin(_questionTime) * _questionScaleAmplitude;
            _currentQuestionScale = _baseQuestionScale + pulse;
        }

        private void BuildRootTransform()
        {
            RootTransform =
                Matrix.CreateScale(_currentBlockScale, _currentBlockScale, 1f) *
                Matrix.CreateTranslation(_currentPosition.X, _currentPosition.Y, 0f);
        }

        public void DrawLocal(SpriteBatch spriteBatch)
        { 
            // Block at origin (root matrix places it in world space)
            spriteBatch.Draw(
                texture: _blockTexture,
                position: Vector2.Zero,
                sourceRectangle: null,
                color: _blockColor,
                rotation: 0f,
                origin: _blockOrigin,
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f
                );

            // Question mark: second-level local scale
            spriteBatch.Draw(
                texture: _questionTexture,
                position: Vector2.Zero,
                sourceRectangle: null,
                color: _questionColor,
                rotation: 0f,
                origin: _questionOrigin,
                scale: _currentQuestionScale,
                effects: SpriteEffects.None,
                layerDepth: 0.1f);
        }
    }
}