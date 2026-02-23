using System;
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
    
    // Goomba
    private Goomba goomba;
    private bool squashed = false;
    private MadGoomba _madGoomba;
    
    // Character
    private Mario _mario;
    private Luigi _luigi;
    
    // Animation Timer
    private float _timer = 0f;
    
    // Item Blocks
    private Texture2D _itemBlockTex;
    private Texture2D _questionTex;
    private ItemBlock _block1;
    private ItemBlock _block2;
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
        
        // Goomba
        Texture2D goombaBody = Content.Load<Texture2D>("goombaBody");
        Texture2D eyes = Content.Load<Texture2D>("goombaEyes");
        Texture2D feet = Content.Load<Texture2D>("goombaFeet");
        goomba = new Goomba(goombaBody, feet, eyes, new Vector2(290f, 285f));
        _madGoomba = new MadGoomba(goombaBody, feet, eyes, new Vector2(25f, 285f)); 
        
        // Character
        Texture2D characterBody = Content.Load<Texture2D>("characterBody");
        Texture2D characterArm = Content.Load<Texture2D>("characterArm");
        Texture2D characterLeg = Content.Load<Texture2D>("characterLeg");
        _mario = new Mario(new Vector2(230f, 285f), 1f, 1.25f, characterBody, characterArm, characterLeg, Color.White);
        _luigi = new Luigi(new Vector2(580f, 285f), 0.5f, 1.25f, characterBody, characterArm,  characterLeg, Color.LimeGreen);
        
        
        // Item Block
        _itemBlockTex = Content.Load<Texture2D>("ItemBlock");
        _questionTex = Content.Load<Texture2D>("QuestionMark");
        float desiredBlockPixels = 10f;
        float blockBaseScale = desiredBlockPixels / _itemBlockTex.Width;

        float questioBaseScale = .65f;
        
        // Item block instance 1
        _block1 = new ItemBlock(
            blockTexture: _itemBlockTex,
            questionTexture: _questionTex,
            basePosition: new Vector2(135f, 155f),
            blockColor: new Color(240, 200, 40),
            questionColor: Color.Yellow,
            bobAmplitude: 10f,
            bobSpeed: .05f,
            baseBlockScale: blockBaseScale,
            scaleAmplitude: .01f,
            scaleSpeed: 1.0f,
            baseQuestionScale: 6.0f,
            questionScaleAmplitude: .18f,
            questionPulseSpeed: 4.0f
            );
        _block2 = new ItemBlock(
            blockTexture: _itemBlockTex,
            questionTexture: _questionTex,
            basePosition: new Vector2(640f, 90f),
            blockColor: new Color(160, 110, 60),
            questionColor: Color.Brown,
            bobAmplitude: 10f,
            bobSpeed: 1.0f,
            baseBlockScale: blockBaseScale * 2.0f,
            scaleAmplitude: .01f,
            scaleSpeed: 2.0f,
            baseQuestionScale: 8.0f,
            questionScaleAmplitude: .50f,
            questionPulseSpeed: 5.0f
        );
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        piranha1.Update(gameTime);
        piranha2.Update(gameTime);
        goomba.Move(gameTime, [260f, 480f]);
        
        // SQUASH THE GOOMBA
        if (_timer >= 4.25f)
        {
            squashed = true;
        }

        if (squashed)
        {
            goomba.Squash();
        }
        _madGoomba.Move(gameTime, [25f, 85f]);
        _mario.Move(gameTime);
        _luigi.Move(gameTime);
        
        // Item Block
        _block1.Update(gameTime);
        _block2.Update(gameTime);
        
        // Resets all objects
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _timer += dt;
        if (_timer >= 15f)
        {
            _timer = 0f;
            _mario.ResetSteps();
            _luigi.ResetSteps();
            goomba.Reset();
            _madGoomba.Reset();
            piranha1.Reset();
            piranha2.Reset();
            squashed = false;
            _block1.Reset();
            _block2.Reset();
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
        _spriteBatch.End();
        
        // Piranha
        piranha1.Draw(_spriteBatch);
        piranha2.Draw(_spriteBatch);
        
        // Item Block
        _spriteBatch.Begin(transformMatrix: _block1.RootTransform);
        _block1.DrawLocal(_spriteBatch);
        _spriteBatch.End();
        
        _spriteBatch.Begin(transformMatrix: _block2.RootTransform);
        _block2.DrawLocal(_spriteBatch);
        _spriteBatch.End();
        
        // Short Pipe
        _spriteBatch.Begin();
        _spriteBatch.Draw(_shortPipeTex, _shortPipePos, null, Color.White, 0f, Vector2.Zero, _shortPipeScale, SpriteEffects.None, 0f);
        
        // Long Pipe
        _spriteBatch.Draw(_tallPipeTex, _tallPipePos, null, Color.White, 0f, Vector2.Zero, _tallPipeScale, SpriteEffects.None, 0f);
        _spriteBatch.End();
        
        // Goomba
        goomba.Draw(_spriteBatch);
        _madGoomba.Draw(_spriteBatch);
        
        // Character
        _mario.Draw(_spriteBatch);
        _luigi.Draw(_spriteBatch);
        
        base.Draw(gameTime);
    }
}