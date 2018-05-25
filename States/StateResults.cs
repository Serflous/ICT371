using Assignment2.GameElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.States
{
    class StateResults : IState
    {


        private SpriteBatch m_spriteBatch;
        private ContentManager m_content;
        private IStateManager m_stateManager;

        private Texture2D m_blackboard;
        private Texture2D m_titleText;
        private SpriteFont m_font;

        private Rectangle m_titleTextRectangle;

        private float adjustedWidthFactor, adjustedHeightFactor;
        /// <summary>
        /// Creates an instance of StateResults
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to allow 2D drawing</param>
        /// <param name="manager">The content manager to allow loading in values</param>
        public StateResults(SpriteBatch spriteBatch, ContentManager content)
        {
            m_spriteBatch = spriteBatch;
            m_content = content;
        }

        public void Dispose()
        {
            
        }
        /// <summary>
        /// Draws the results state. Draws the classroom and the writing.
        /// </summary>
        /// <param name="time">The gametime</param>
        public void Draw(GameTime time)
        {
            m_spriteBatch.Begin();
            m_spriteBatch.Draw(m_blackboard, new Rectangle(0, 0, Properties.Settings.Default.SCREEN_RES_X, Properties.Settings.Default.SCREEN_RES_Y), Color.White);
            m_spriteBatch.Draw(m_titleText, m_titleTextRectangle, Color.White);
            m_spriteBatch.DrawString(m_font, "Number of wrong answers;", new Vector2((int)(20 * adjustedWidthFactor), (int)(130 * adjustedHeightFactor)), Color.White);
            m_spriteBatch.DrawString(m_font, "Addition: " + Counter.Instance.GetWrongAnswers(GameElements.NPC.Question.QuestionType.Addition), new Vector2((int)(20 * adjustedWidthFactor), (int)(150 * adjustedHeightFactor)), Color.White);
            m_spriteBatch.DrawString(m_font, "Subtraction: " + Counter.Instance.GetWrongAnswers(GameElements.NPC.Question.QuestionType.Subtraction), new Vector2((int)(20 * adjustedWidthFactor), (int)(170 * adjustedHeightFactor)), Color.White);
            m_spriteBatch.DrawString(m_font, "Multiplication: " + Counter.Instance.GetWrongAnswers(GameElements.NPC.Question.QuestionType.Multiplication), new Vector2((int)(20 * adjustedWidthFactor), (int)(190 * adjustedHeightFactor)), Color.White);
            m_spriteBatch.DrawString(m_font, "Division: " + Counter.Instance.GetWrongAnswers(GameElements.NPC.Question.QuestionType.Division), new Vector2((int)(20 * adjustedWidthFactor), (int)(210 * adjustedHeightFactor)), Color.White);
            m_spriteBatch.End();
        }
        /// <summary>
        /// Gets the ID of the results state.
        /// </summary>
        /// <returns>The ID of the game state</returns>
        public int GetID()
        {
            return (int)StateManager.States.STATE_RESULTS;
        }
        /// <summary>
        /// Initializes the results state
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
            m_font = m_content.Load<SpriteFont>("font");

            int expectedWidth = m_blackboard.Width;
            int expectedHeight = m_blackboard.Height;
            adjustedWidthFactor = Properties.Settings.Default.SCREEN_RES_X / (float)expectedWidth;
            adjustedHeightFactor = Properties.Settings.Default.SCREEN_RES_Y / (float)expectedHeight;

            m_titleTextRectangle = new Rectangle((int)(86 * adjustedWidthFactor), (int)(49 * adjustedHeightFactor), (int)(m_titleText.Width * adjustedWidthFactor), (int)(m_titleText.Height * adjustedHeightFactor));
        }
        /// <summary>
        /// Unloads the data
        /// </summary>
        public void Unload()
        {
            m_content.Unload();
        }
        /// <summary>
        /// Updates the game state
        /// </summary>
        /// <param name="time">The game time</param>
        public void Update(GameTime time)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape) ||
               GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.B))
            {
                m_stateManager.PopState();
            }
        }
    }
}
