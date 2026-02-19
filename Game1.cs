using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace group_19_assignment4;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    // Background
    private Texture2D _background;
    
    // Music Variable
    private Song _bgm;
    
    // Piranha Plant
    private PiranhaPlant piranha1;
    private PiranhaPlant piranha2;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    
    protected override void Initialize()
    {
        // Background Size
        _graphics.PreferredBackBufferWidth = 778;
        _graphics.PreferredBackBufferHeight = 350;
        _graphics.ApplyChanges();
        
        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Background Sound
        _bgm = Content.Load<Song>("background_music");
        MediaPlayer.Volume = 0.3f; 
        MediaPlayer.IsRepeating = true; 
        MediaPlayer.Play(_bgm);
        
        // Background
        _background = Content.Load<Texture2D>("background");

        // Piranha Plant
        Texture2D stem = Content.Load<Texture2D>("Stem");
        Texture2D jLeft = Content.Load<Texture2D>("Jaw Left");
        Texture2D jRight = Content.Load<Texture2D>("Jaw Right");
        
        piranha1 = new PiranhaPlant(stem, jLeft, jRight, new Vector2(610, 245), 3.0f); 
        piranha2 = new PiranhaPlant(stem, jLeft, jRight, new Vector2(687, 215), 6.0f); 
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        piranha1.Update(gameTime);
        piranha2.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
        _spriteBatch.End();
        
        piranha1.Draw(_spriteBatch);
        piranha2.Draw(_spriteBatch);
        
        base.Draw(gameTime);
    }
}