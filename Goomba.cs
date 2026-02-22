using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace group_19_assignment4;

public class Goomba
{
    private Vector2 _position;
    protected Vector2 _velocity;
    private Vector2 _eyePos;
    private Vector2 _eyeVel;
    private Vector2 _RFeetPos;
    private Vector2 _LFeetPos;
    private Vector2 _bodyScale;
    private Vector2 _RFeetScale;
    private Vector2 _LFeetScale;
    protected float _stompSpeed;
    private Vector3 _ultScale =  new Vector3(0.06f, 0.06f, 0.06f);
    private Matrix rootMatrix;

    private Texture2D _goombaBody;
    private Texture2D _goombaRFeet;
    private Texture2D _goombaLFeet;
    private Texture2D _eye;
    private float _opacity;
    private bool _squashed;
    protected Color _color;
    private float _time;

    public Goomba(Texture2D body, Texture2D feet, Texture2D eye, Vector2 position)
    {
        _goombaBody = body;
        _goombaRFeet = feet;
        _goombaLFeet = feet;
        _RFeetPos = new Vector2(-2f, -1f);
        _LFeetPos = new Vector2(-480f, -1f);
        _eye = eye;
        _eyePos = new Vector2(50f, -30f);
        _opacity = 1f;
        _position = position;
        _velocity = new Vector2(1f,0f);
        _bodyScale = Vector2.One;
        _squashed = false;
        _color = Color.White;
        _RFeetScale = Vector2.One;
        _LFeetScale = Vector2.One;
        _eyeVel =  new Vector2(-3f, 0f);
        _stompSpeed = 3f;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        rootMatrix = Matrix.CreateScale(_ultScale) * Matrix.CreateTranslation(_position.X, _position.Y, 0f);
        Vector2 goombaOrigin = new Vector2(_goombaBody.Width / 2f, _goombaBody.Height / 2f);
        spriteBatch.Begin(transformMatrix: rootMatrix);
        
        // Level 1: Goomba Body
        spriteBatch.Draw(_goombaBody, Vector2.Zero, null, _color * _opacity, 0.0f, goombaOrigin, _bodyScale, SpriteEffects.None, 0f);
        
        // Level 2: Eyes & Feet
        Vector2 eyeOrigin = new Vector2(_eye.Width / 2f, _eye.Height / 2f);
        spriteBatch.Draw(_eye, _eyePos, null, _color*_opacity, 0.0f, eyeOrigin, _bodyScale, SpriteEffects.None, 0f);
        
        Vector2 LFeetOrigin = new Vector2(_goombaLFeet.Width / 2f, _goombaLFeet.Height / 2f);
        Vector2 RFeetOrigin = new Vector2(_goombaRFeet.Width / 2f, _goombaRFeet.Height / 2f);
        spriteBatch.Draw(_goombaLFeet, _LFeetPos, null, _color * _opacity, 0.0f,  LFeetOrigin, _LFeetScale, SpriteEffects.None, 0f);
        spriteBatch.Draw(_goombaRFeet, _RFeetPos, null, _color * _opacity, 0.0f, RFeetOrigin, _RFeetScale, SpriteEffects.None, 0f);
        
        spriteBatch.End();
    }

    public void Move(GameTime gameTime, float[] bounds)
    { 
        _position += _velocity;
        if (_squashed)
        {
            return;
        }
        
        // Translate Body
        if (_position.X < bounds[0] || _position.X > bounds[1])
        {
            _velocity *= -1;
        }
        
        // Move Eye Direction
        if ((_position.X > bounds[1] - 15f) && (_eyePos.X < 55f || _eyePos.X > 45f))
        {
            _eyePos += _eyeVel;
        }
        else if ((_position.X < bounds[0] + 15f) && (_eyePos.X < 55f || _eyePos.X > 45f))
        {
            _eyePos -= _eyeVel;
        }
        
        // Move Feet
        _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
        _RFeetScale.Y = 1f - 0.2f * (float)Math.Sin(_time * _velocity.X * _stompSpeed);
        _LFeetScale.Y = 1f + 0.2f * (float)Math.Sin(_time * _velocity.X * _stompSpeed);
        _LFeetPos.Y += 0.2f * (float)Math.Sin(_time * _velocity.X * _stompSpeed);
        _RFeetPos.Y -= 0.2f * (float)Math.Sin(_time * _velocity.X * _stompSpeed);
    }

    public void Squash()
    {
        _velocity.X = 0.0f;
        _ultScale.Y *= 0.95f;
        _ultScale.X *= 0.98f;
        _opacity *= 0.95f;
    }

}