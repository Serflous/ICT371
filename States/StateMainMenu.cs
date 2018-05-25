using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment2.States
{
    class StateMainMenu : IState
    {


        private IStateManager m_stateManager;
        private SpriteBatch m_spriteBatch;
        private ContentManager m_content;
        private Texture2D m_blackboard;
        private Texture2D m_titleText;
        private Texture2D m_playGameText;
        private Texture2D m_optionsText;
        private Texture2D m_exitText;

        // Texture Positioning
        private Rectangle m_titleTextRectangle;
        private Rectangle m_playGameTextRectangle;
        private Rectangle m_optionsTextRectangle;
        private Rectangle m_exitTextRectangle;

        private KeyboardState m_oldKeyState;
        private GamePadState m_oldGamePadState;
        private int m_option = 0;
        /// <summary>
        /// Creates an instance of StateMainMenu
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to allow 2D drawing</param>
        /// <param name="manager">The content manager to allow loading in values</param>
        public StateMainMenu(SpriteBatch spriteBatch, ContentManager content)
        {
            m_spriteBatch = spriteBatch;
            m_content = new ContentManager(content.ServiceProvider, "Content");
        }

        /// <summary>
        /// Initializes the main menu
        /// </summary>
        /// <param name="manager">The state manager so it can call another state.</param>
        public void Init(IStateManager manager)
        {
            m_stateManager = manager;
        }
        /// <summary>
        /// Loads all the assets this state uses.
        /// </summary>
        public void Load()
        {
            m_blackboard = m_content.Load<Texture2D>("Blackboard");
            m_titleText = m_content.Load<Texture2D>("TitleText");
            m_playGameText = m_content.Load<Texture2D>("PlayGameText");
            m_optionsText = m_content.Load<Texture2D>("OptionsText");
            m_exitText = m_content.Load<Texture2D>("ExitText");

            int expectedWidth = m_blackboard.Width;
            int expectedHeight = m_blackboard.Height;
            float adjustedWidthFactor = Properties.Settings.Default.SCREEN_RES_X / (float)expectedWidth;
            float adjustedHeightFactor = Properties.Settings.Default.SCREEN_RES_Y / (float)expectedHeight;

            m_titleTextRectangle = new Rectangle((int)(86 * adjustedWidthFactor), (int)(49 * adjustedHeightFactor), (int)(m_titleText.Width * adjustedWidthFactor), (int)(m_titleText.Height * adjustedHeightFactor));
            m_playGameTextRectangle = new Rectangle((int)(657 * adjustedWidthFactor), (int)(201 * adjustedHeightFactor), (int)(m_playGameText.Width * adjustedWidthFactor), (int)(m_playGameText.Height * adjustedHeightFactor));
            m_optionsTextRectangle = new Rectangle((int)(657 * adjustedWidthFactor), (int)(358 * adjustedHeightFactor), (int)(m_optionsText.Width * adjustedWidthFactor), (int)(m_optionsText.Height * adjustedHeightFactor));
            m_exitTextRectangle = new Rectangle((int)(657 * adjustedWidthFactor), (int)(509 * adjustedHeightFactor), (int)(m_exitText.Width * adjustedWidthFactor), (int)(m_exitText.Height * adjustedHeightFactor));
            
        }

        /// <summary>
        /// Unloads the data
        /// </summary>
        public void Unload()
        {
            m_content.Unload();
        }

        /// <summary>
        /// Updates the main menu state
        /// </summary>
        /// <param name="time">The game time</param>
        public void Update(GameTime time)
        {
            
            if(Keyboard.GetState().IsKeyDown(Keys.Down) && !m_oldKeyState.IsKeyDown(Keys.Down) ||
               GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0 && m_oldGamePadState.ThumbSticks.Left.Y >= 0 ||
               GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown) && !m_oldGamePadState.IsButtonDown(Buttons.DPadDown))
            {
                m_option++;
                if (m_option > 2)
                    m_option = 0;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Up) && !m_oldKeyState.IsKeyDown(Keys.Up) ||
                GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0 && m_oldGamePadState.ThumbSticks.Left.Y <= 0 ||
                GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp) && !m_oldGamePadState.IsButtonDown(Buttons.DPadUp))
            {
                m_option--;
                if (m_option < 0)
                    m_option = 2;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !m_oldKeyState.IsKeyDown(Keys.Enter) ||
               GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && !m_oldGamePadState.IsButtonDown(Buttons.A))
            {
                if(m_option == 0)
                {
                    m_stateManager.PushState((int)StateManager.States.STATE_GAME);
                    m_option = 3;
                }
                if(m_option == 1)
                {
                    // Options
                }
                if(m_option == 2)
                {
                    m_stateManager.PopState();
                }
            }

            m_oldKeyState = Keyboard.GetState();
            m_oldGamePadState = GamePad.GetState(PlayerIndex.One);
        }
        /// <summary>
        /// Draws the main menu state. Draws the classroom and the writing.
        /// </summary>
        /// <param name="time">The gametime</param>
        public void Draw(GameTime time)
        {
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_blackboard, new Rectangle(0, 0, Properties.Settings.Default.SCREEN_RES_X, Properties.Settings.Default.SCREEN_RES_Y), Color.White);
            m_spriteBatch.Draw(m_titleText, m_titleTextRectangle, Color.White);
            m_spriteBatch.Draw(m_playGameText, m_playGameTextRectangle, (m_option == 0) ? Color.Tan : Color.White);
            m_spriteBatch.Draw(m_optionsText, m_optionsTextRectangle, (m_option == 1) ? Color.Tan : Color.White);
            m_spriteBatch.Draw(m_exitText, m_exitTextRectangle, (m_option == 2) ? Color.Tan : Color.White);
            m_spriteBatch.End();
        }
        /// <summary>
        /// Gets the ID of the main menu state.
        /// </summary>
        /// <returns>The ID of the game state</returns>
        public int GetID()
        {
            return (int)StateManager.States.STATE_MAIN_MENU;
        }
        
        public void Dispose()
        {

        }
    }
}
