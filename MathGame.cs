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
            m_stateManager.NoActiveStates += oNoActiveStates;
            Content.RootDirectory = "Content";
            
        }
        /// <summary>
        /// Initalizes the game.
        /// </summary>
        protected override void Initialize()
        {
            m_graphics.PreferredBackBufferWidth = Properties.Settings.Default.SCREEN_RES_X;
            m_graphics.PreferredBackBufferHeight = Properties.Settings.Default.SCREEN_RES_Y;
            m_graphics.IsFullScreen = Properties.Settings.Default.FULL_SCREEN;
            IsFixedTimeStep = Properties.Settings.Default.VSYNC;
            m_graphics.SynchronizeWithVerticalRetrace = Properties.Settings.Default.VSYNC;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            m_graphics.ApplyChanges();

            base.Initialize();
        }
        /// <summary>
        /// Loads the content. Creates the new sprite batch and initializes the state manager
        /// </summary>
        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_stateManager.Init(m_spriteBatch, Content);
        }

        protected override void UnloadContent()
        {
            
        }
        /// <summary>
        /// Updates the game. Calls the current states update method
        /// </summary>
        /// <param name="gameTime">The game time</param>
        protected override void Update(GameTime gameTime)
        {
            m_stateManager.Update(gameTime);

            base.Update(gameTime);
        }
        /// <summary>
        /// Calls the current states draw method
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_stateManager.Draw(gameTime);
            base.Draw(gameTime);
        }
        /// <summary>
        /// Called when theres no active states.
        /// Exits the game
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event Args</param>
        private void oNoActiveStates(object sender, System.EventArgs e)
        {
            Exit();
        }
    }
}
