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
    
    // Song
    private Song _bgm;
    
    // Piranha Plant
    private PiranhaPlant piranha1;
    private PiranhaPlant piranha2;

    // Pipes
    private Texture2D _shortPipeTex;
    private Texture2D _tallPipeTex;
    private Vector2 _shortPipePos = new Vector2(600, 242); 
    private Vector2 _tallPipePos = new Vector2(678, 212);
    private float _shortPipeScale = .8f;
    private float _tallPipeScale = .8f;
    // -----------------------

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

        // Background Song
        _bgm = Content.Load<Song>("background_music");
        MediaPlayer.Volume = 0.3f; 
        MediaPlayer.IsRepeating = true; 
        MediaPlayer.Play(_bgm);
        
        // Background
        _background = Content.Load<Texture2D>("background");

        // Pipes
        _shortPipeTex = Content.Load<Texture2D>("Short Pipe 1");
        _tallPipeTex = Content.Load<Texture2D>("Tall Pipe");

        // Piranha Plant
        Texture2D stem = Content.Load<Texture2D>("Stem");
        Texture2D jLeft = Content.Load<Texture2D>("Jaw Left");
        Texture2D jRight = Content.Load<Texture2D>("Jaw Right");
        
        piranha1 = new PiranhaPlant(stem, jLeft, jRight, new Vector2(610, 245), 3.0f, .15f, 0.0f); 
        piranha2 = new PiranhaPlant(stem, jLeft, jRight, new Vector2(687, 215), 6.0f, .15f, 1.5f); 
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

        _spriteBatch.Begin();
        
        // Short Pipe
        _spriteBatch.Draw(_shortPipeTex, _shortPipePos, null, Color.White, 0f, Vector2.Zero, _shortPipeScale, SpriteEffects.None, 0f);
        
        // Long Pipe
        _spriteBatch.Draw(_tallPipeTex, _tallPipePos, null, Color.White, 0f, Vector2.Zero, _tallPipeScale, SpriteEffects.None, 0f);
        
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}