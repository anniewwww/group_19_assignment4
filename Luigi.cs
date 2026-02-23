using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace group_19_assignment4;

public class Luigi : Character
{
    private float _timer;
    private int _step;
    
    public Luigi(Vector2 position, float speed, float scale, Texture2D body, Texture2D arm, Texture2D leg, Color color)
        : base(position, speed, scale, body, arm, leg, color)
    {
    }
    
    public override void Move(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _timer += dt;

        switch (_step)
        {
            case 0:
                _velocity.X = 1f;
                if (_timer >= 2f)
                {
                    NextStep();
                }
                break;
            
            case 1:
                _velocity.X = 1f;
                _position.X -= _speed;
                Jump(gameTime, _groundY - 60f);

                if (!_isJumping && _timer > 1f && _position.Y == _groundY - 60f)
                {
                    NextStep();
                }
                break;
            
            case 2:
                _velocity.X = 1f;
                _position.X -= _speed;
                Jump(gameTime, _groundY);
                
                if (!_isJumping && _timer > 1f && _position.Y == _groundY)
                {
                    NextStep();
                }
                break;
            
            case 3:
                _velocity.X = 1f;
                _position.X -= _speed;
                if (_timer >= 6f)
                {
                    NextStep();
                }
                break;
            
            case 4:
                _velocity.X = 1f;
                _position.X -= _speed;
                Jump(gameTime, _groundY - 170f);

                if (!_isJumping && _timer > 1f && _position.Y == _groundY - 170f)
                {
                    NextStep();
                }
                break;
            
            case 5:
                _velocity.X = 1f;
                _position.X -= _speed;
                if (_timer >= 10f)
                {
                    ResetSteps();
                }
                break;
        }
        AnimateLimbs(dt);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        _rootMatrix = Matrix.CreateScale(_scale, Math.Abs(_scale), 1f) *
                      Matrix.CreateTranslation(_position.X, _position.Y, 0f);

        spriteBatch.Begin(transformMatrix: _rootMatrix);
        
        
        Vector2 bodyOrigin = new Vector2(_bodyTexture.Width / 2f, _bodyTexture.Height / 2f);
        Vector2 armOrigin = new Vector2(_armTexture.Width / 2f, _armTexture.Height / 2f);
        Vector2 legOrigin = new Vector2(_legTexture.Width / 2f, _legTexture.Height / 2f);
        
        spriteBatch.Draw(_armTexture,
            new Vector2((bodyOrigin.X - 18f), bodyOrigin.Y - 43f),
            null,
            _color,
            _leftArmRotation,
            armOrigin,
            1f,
            SpriteEffects.FlipHorizontally,
            0f);
        
        spriteBatch.Draw(_bodyTexture,
            new Vector2(0f, -15f),
            null,
            _color,
            0f,
            bodyOrigin,
            1f,
            SpriteEffects.FlipHorizontally,
            0f);
        
        spriteBatch.Draw(_armTexture,
            new Vector2((bodyOrigin.X - 18f), bodyOrigin.Y - 43f),
            null,
            _color,
            _rightArmRotation,
            armOrigin,
            1f,
            SpriteEffects.FlipHorizontally,
            0f);
        
        spriteBatch.Draw(_legTexture,
            new Vector2((bodyOrigin.X - 18f), bodyOrigin.Y - 58f),
            null,
            _color,
            _leftLegRotation,
            legOrigin,
            1f,
            SpriteEffects.FlipHorizontally,
            0f);
        spriteBatch.Draw(_legTexture,
            new Vector2((bodyOrigin.X - 18f), bodyOrigin.Y - 58f),
            null,
            _color,
            _rightLegRotation,
            legOrigin,
            1f,
            SpriteEffects.FlipHorizontally,
            0f);
        
        spriteBatch.End();
    }
    
    private void NextStep()
    {
        _timer = 0f;
        _step++;
    }

    public void ResetSteps()
    {
        _timer = 0f;
        _step = 0;
        Reset();
    }
}