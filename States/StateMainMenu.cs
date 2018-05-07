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


        private StateManager m_stateManager;
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
        private int m_option = 0;

        public StateMainMenu(StateManager stateManager, SpriteBatch spriteBatch, ContentManager content)
        {
            m_stateManager = stateManager;
            m_spriteBatch = spriteBatch;
            m_content = content;
        }

        public void Init()
        {
            m_blackboard = m_content.Load<Texture2D>("Blackboard");
            m_titleText = m_content.Load<Texture2D>("TitleText");
            m_playGameText = m_content.Load<Texture2D>("PlayGameText");
            m_optionsText = m_content.Load<Texture2D>("OptionsText");
            m_exitText = m_content.Load<Texture2D>("ExitText");

            int expectedWidth = m_blackboard.Width;
            int expectedHeight = m_blackboard.Height;
            float adjustedWidthFactor = m_spriteBatch.GraphicsDevice.DisplayMode.Width / (float)expectedWidth;
            float adjustedHeightFactor = m_spriteBatch.GraphicsDevice.DisplayMode.Height / (float)expectedHeight;
            
            m_titleTextRectangle = new Rectangle((int)(86 * adjustedWidthFactor), (int)(49 * adjustedHeightFactor), (int)(m_titleText.Width * adjustedWidthFactor), (int)(m_titleText.Height * adjustedHeightFactor));
            m_playGameTextRectangle = new Rectangle((int)(657 * adjustedWidthFactor), (int)(201 * adjustedHeightFactor), (int)(m_playGameText.Width * adjustedWidthFactor), (int)(m_playGameText.Height * adjustedHeightFactor));
            m_optionsTextRectangle = new Rectangle((int)(657 * adjustedWidthFactor), (int)(358 * adjustedHeightFactor), (int)(m_optionsText.Width * adjustedWidthFactor), (int)(m_optionsText.Height * adjustedHeightFactor));
            m_exitTextRectangle = new Rectangle((int)(657 * adjustedWidthFactor), (int)(509 * adjustedHeightFactor), (int)(m_exitText.Width * adjustedWidthFactor), (int)(m_exitText.Height * adjustedHeightFactor));
        }
        
        public void Update(GameTime time)
        {
            
            if(Keyboard.GetState().IsKeyDown(Keys.Down) && !m_oldKeyState.IsKeyDown(Keys.Down))
            {
                m_option++;
                if (m_option > 2)
                    m_option = 0;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Up) && !m_oldKeyState.IsKeyDown(Keys.Up))
            {
                m_option--;
                if (m_option < 0)
                    m_option = 2;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !m_oldKeyState.IsKeyDown(Keys.Enter))
            {
                if(m_option == 2)
                {
                    m_stateManager.PopState();
                }
            }

            m_oldKeyState = Keyboard.GetState();
        }

        public void Draw(GameTime time)
        {
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_blackboard, new Rectangle(0, 0, m_spriteBatch.GraphicsDevice.DisplayMode.Width, m_spriteBatch.GraphicsDevice.DisplayMode.Height), Color.White);
            m_spriteBatch.Draw(m_titleText, m_titleTextRectangle, Color.White);
            m_spriteBatch.Draw(m_playGameText, m_playGameTextRectangle, (m_option == 0) ? Color.Tan : Color.White);
            m_spriteBatch.Draw(m_optionsText, m_optionsTextRectangle, (m_option == 1) ? Color.Tan : Color.White);
            m_spriteBatch.Draw(m_exitText, m_exitTextRectangle, (m_option == 2) ? Color.Tan : Color.White);
            m_spriteBatch.End();
        }
        
        public void Dispose()
        {

        }


    }
}
