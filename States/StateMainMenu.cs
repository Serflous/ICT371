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
        private Texture2D m_titleScreen;
        private SpriteFont m_font;
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
            m_titleScreen = m_content.Load<Texture2D>("Title");
            m_font = m_content.Load<SpriteFont>("Selector");
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
            m_spriteBatch.Draw(m_titleScreen, new Rectangle(0, 0, m_spriteBatch.GraphicsDevice.DisplayMode.Width, m_spriteBatch.GraphicsDevice.DisplayMode.Height), Color.White);
            m_spriteBatch.DrawString(m_font, ">", new Vector2(m_spriteBatch.GraphicsDevice.DisplayMode.Width / 1.6f, 350 + (m_option * 260)), Color.White);
            m_spriteBatch.End();
        }
        
        public void Dispose()
        {

        }


    }
}
