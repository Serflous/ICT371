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
    class StateGame : IState
    {


        private IStateManager m_stateManager;
        private ContentManager m_contentManager;

        private Model m_classroom;

        public StateGame(ContentManager manager)
        {
            m_contentManager = new ContentManager(manager.ServiceProvider, "Content");
        }

        public void Init(IStateManager manager)
        {
            m_stateManager = manager;
        }

        public void Load()
        {
            //m_classroom = m_contentManager.Load<Model>("Classroom/classroom");
        }

        public void Draw(GameTime time)
        {
            
        }
        
        public void Update(GameTime time)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                m_stateManager.PopState();
            }
        }

        public void Unload()
        {
            
        }

        public int GetID()
        {
            return (int)StateManager.States.STATE_GAME;
        }


        public void Dispose()
        {
            
        }

    }
}
