using Assignment2.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment2
{

    public class MathGame : Game
    {
        GraphicsDeviceManager m_graphics;
        SpriteBatch m_spriteBatch;
        StateManager m_stateManager;

        public MathGame()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_stateManager = new StateManager();
            m_stateManager.NoActiveStates += M_stateManager_NoActiveStates;
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {
            m_graphics.PreferredBackBufferWidth = Properties.Settings.Default.SCREEN_RES_X;
            m_graphics.PreferredBackBufferHeight = Properties.Settings.Default.SCREEN_RES_Y;
            m_graphics.IsFullScreen = Properties.Settings.Default.FULL_SCREEN;
            IsFixedTimeStep = Properties.Settings.Default.VSYNC;
            m_graphics.SynchronizeWithVerticalRetrace = Properties.Settings.Default.VSYNC;
            m_graphics.ApplyChanges();

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_stateManager.Init(m_spriteBatch, Content);
        }

        protected override void UnloadContent()
        {
            
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            m_stateManager.Update(gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_stateManager.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void M_stateManager_NoActiveStates(object sender, System.EventArgs e)
        {
            Exit();
        }
    }
}
