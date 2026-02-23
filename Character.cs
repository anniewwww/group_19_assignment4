using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace group_19_assignment4;

public class Character
{
    private Vector2 _initialPosition;
    
    protected Vector2 _position;
    protected Vector2 _velocity;
    protected float  _speed;
    protected float _scale;

    protected Texture2D _bodyTexture;
    protected Texture2D _armTexture;
    protected Texture2D _legTexture;
    
    protected float _leftArmRotation;
    protected float _rightArmRotation;
    protected float _leftLegRotation;
    protected float _rightLegRotation;

    protected float _groundY;
    private float _jumpSpeed;
    protected bool _isJumping;

    private float _time;
    protected Color _color;
    protected Matrix _rootMatrix;

    public Character(Vector2 position, float speed, float scale, Texture2D bodyTexture, Texture2D armTexture,
        Texture2D legTexture, Color color)
    {
        _initialPosition = position;
        _position = position;
        _speed = speed;
        _scale = scale;
        
        _bodyTexture = bodyTexture;
        _armTexture = armTexture;
        _legTexture = legTexture;
        
        _color = color;

        _velocity = new Vector2(1, 0);
        _groundY = position.Y;
    }

    public void Jump(GameTime gameTime, float endHeight)
    {
        if (!_isJumping)
        {
            _isJumping = true;
            _jumpSpeed = -10;
        }

        if (_isJumping)
        {
            _position.Y += _jumpSpeed;
            _jumpSpeed += 0.25f;
            if (_position.Y >= endHeight)
            {
                _position.Y = endHeight;
                _isJumping = false;
            }
        }
    }

    public virtual void Move(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _position += _velocity * _speed;
        AnimateLimbs(dt);
    }
    
    public void AnimateLimbs(float dt)
    {
        _time += dt;
        if (Math.Abs(_velocity.X) > 0.1f)
        {
            _leftArmRotation = 0.5f * (float)Math.Sin(_time);
            _rightArmRotation = -0.5f * (float)Math.Sin(_time);
            _leftLegRotation = -0.25f * (float)Math.Sin(_time);
            _rightLegRotation = 0.25f * (float)Math.Sin(_time);
        }
        else
        {
            _leftArmRotation = 0f;
            _rightArmRotation = 0f;
            _leftLegRotation = 0f;
            _rightLegRotation = 0f;
        }
    }

    public void Reset()
    {
        _position =  _initialPosition;
        _isJumping = false;
        _jumpSpeed = 0f;
        
        _leftArmRotation = 0f;
        _rightArmRotation = 0f;
        _leftLegRotation = 0f;
        _rightLegRotation = 0f;

        _time = 0f;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
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
            SpriteEffects.None,
            0f);
        
        spriteBatch.Draw(_bodyTexture,
            new Vector2(0f, -15f),
            null,
            _color,
            0f,
            bodyOrigin,
            1f,
            SpriteEffects.None,
            0f);
        
        spriteBatch.Draw(_armTexture,
            new Vector2((bodyOrigin.X - 18f), bodyOrigin.Y - 43f),
            null,
            _color,
            _rightArmRotation,
            armOrigin,
            1f,
            SpriteEffects.None,
            0f);
        
        spriteBatch.Draw(_legTexture,
            new Vector2((bodyOrigin.X - 18f), bodyOrigin.Y - 58f),
            null,
            _color,
            _leftLegRotation,
            legOrigin,
            1f,
            SpriteEffects.None,
            0f);
        spriteBatch.Draw(_legTexture,
            new Vector2((bodyOrigin.X - 18f), bodyOrigin.Y - 58f),
            null,
            _color,
            _rightLegRotation,
            legOrigin,
            1f,
            SpriteEffects.None,
            0f);
        
        spriteBatch.End();
    }
}