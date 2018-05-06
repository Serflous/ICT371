using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.States
{
    class StateManager : IDisposable
    {


        public event EventHandler NoActiveStates;
        
        private Stack<IState> m_activeStates;
        private List<IState> m_stateList;

        public StateManager()
        {
            m_activeStates = new Stack<IState>();
            m_stateList = new List<IState>();
        }

        public void Init(SpriteBatch spriteBatch, ContentManager content)
        {
            StateMainMenu stateMainMenu = new StateMainMenu(this, spriteBatch, content);

            stateMainMenu.Init();

            m_stateList.Add(stateMainMenu);

            PushState(stateMainMenu);
        }

        public void PushState(IState state)
        {
            m_activeStates.Push(state);
        }

        public void PopState()
        {
            m_activeStates.Pop();
            if(m_activeStates.Count == 0)
            {
                OnNoActiveStates(null);
            }
        }

        public void Update(GameTime time)
        {
            m_activeStates.Peek().Update(time);
        }

        public void Draw(GameTime time)
        {
            m_activeStates.Peek().Draw(time);
        }

        public void Dispose()
        {
            
        }

        public virtual void OnNoActiveStates(EventArgs e)
        {
            NoActiveStates?.Invoke(this, e);
        }
    }
}
