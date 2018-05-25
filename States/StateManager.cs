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
            STATE_GAME = 2,
            STATE_RESULTS = 3
        }

        public event EventHandler NoActiveStates;
        
        private Stack<IState> m_activeStates;
        private List<IState> m_stateList;

        /// <summary>
        /// Creates an instance of the state manager
        /// </summary>
        public StateManager()
        {
            m_activeStates = new Stack<IState>();
            m_stateList = new List<IState>();
        }
        /// <summary>
        /// Creates all the states for the state manager, initializes them, and adds them to the state list
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="content"></param>
        public void Init(SpriteBatch spriteBatch, ContentManager content)
        {
            StateMainMenu stateMainMenu = new StateMainMenu(spriteBatch, content);
            StateGame stateGame = new StateGame(spriteBatch, content);
            StateResults stateResults = new StateResults(spriteBatch, content);

            stateMainMenu.Init(this);
            stateGame.Init(this);
            stateResults.Init(this);

            m_stateList.Add(stateMainMenu);
            m_stateList.Add(stateGame);
            m_stateList.Add(stateResults);

            PushState((int)States.STATE_MAIN_MENU);
        }
        /// <summary>
        /// Pushes a state onto the stack
        /// </summary>
        /// <param name="state"></param>
        public void PushState(IState state)
        {
            state.Load();
            m_activeStates.Push(state);
        }
        /// <summary>
        /// Pushes a state onto the stack, using its ID
        /// </summary>
        /// <param name="stateName"></param>
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
        /// <summary>
        /// Pops a state off the stack
        /// </summary>
        public void PopState()
        {
            m_activeStates.Peek().Unload();
            m_activeStates.Pop();
            if(m_activeStates.Count == 0)
            {
                OnNoActiveStates(null);
            }
        }
        /// <summary>
        /// Calls the current states update method
        /// </summary>
        /// <param name="time">The Game Time</param>
        public void Update(GameTime time)
        {
            m_activeStates.Peek().Update(time);
        }
        /// <summary>
        /// Calls the current states draw method
        /// </summary>
        /// <param name="time">The game time</param>
        public void Draw(GameTime time)
        {
            m_activeStates.Peek().Draw(time);
        }

        public void Dispose()
        {
            
        }
        /// <summary>
        /// Overridable method when there arn't any more active states.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnNoActiveStates(EventArgs e)
        {
            NoActiveStates?.Invoke(this, e);
        }
    }
}
