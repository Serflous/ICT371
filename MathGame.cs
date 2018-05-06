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
            m_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            m_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            m_graphics.IsFullScreen = true;
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
