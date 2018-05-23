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
    class StateManager : IStateManager
    {


        public enum States
        {
            STATE_MAIN_MENU = 1,
            STATE_GAME = 2
        }

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
            StateMainMenu stateMainMenu = new StateMainMenu(spriteBatch, content);
            StateGame stateGame = new StateGame(spriteBatch, content);

            stateMainMenu.Init(this);
            stateGame.Init(this);

            m_stateList.Add(stateMainMenu);
            m_stateList.Add(stateGame);

            PushState((int)States.STATE_MAIN_MENU);
        }

        public void PushState(IState state)
        {
            state.Load();
            m_activeStates.Push(state);
        }

        public void PushState(int stateName)
        {
            foreach(IState state in m_stateList)
            {
                if(state.GetID() == stateName)
                {
                    PushState(state);
                    break;
                }
            }
        }

        public void PopState()
        {
            m_activeStates.Peek().Unload();
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
