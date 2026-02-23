using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace group_19_assignment4;

public class Mario : Character
{
    private float _timer;
    private int _step;

    public Mario(Vector2 position, float speed, float scale, Texture2D body, Texture2D arm, Texture2D leg, Color color)
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
                _position.X += _speed;
                if (_timer >= 3f)
                {
                    NextStep();
                }
                break;
            
            case 1:
                _velocity.X = 1f;
                Jump(gameTime, _groundY);

                if (!_isJumping && _position.Y == _groundY)
                {
                    NextStep();
                }
                break;
            
            case 2:
                _velocity.X = 1f;
                _position.X += _speed;
                if (_timer >= 0.75f)
                {
                    NextStep();
                }
                break;
            
            case 3:
                _velocity.X = 1f;
                _position.X += _speed;
                Jump(gameTime, _groundY - 60f);

                if (!_isJumping && _timer > 1f && _position.Y == _groundY - 60f)
                {
                    NextStep();
                }
                break;
            
            case 4:
                _velocity.X = 1f;
                _position.X += _speed;
                Jump(gameTime, _groundY - 230f);
                
                if (!_isJumping && _timer > 1f && _position.Y == _groundY - 230f)
                {
                    NextStep();
                }
                break;
            
            case 5:
                _velocity.X = 1f;
                _position.X += _speed;
                if (_timer >= 10f)
                {
                    ResetSteps();
                }
                break;
        }
        AnimateLimbs(dt);
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