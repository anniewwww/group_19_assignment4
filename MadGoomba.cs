using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace group_19_assignment4;

public class MadGoomba: Goomba
{
    public MadGoomba(Texture2D body, Texture2D feet, Texture2D eye, Vector2 position) : base(body, feet, eye, position)
    {
        _color = Color.Red;
        _velocity = new Vector2(3f,0f);
        _stompSpeed = 6f;
    }
}