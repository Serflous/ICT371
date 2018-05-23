using Assignment2.GameElements.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements
{
    class GameManager
    {

        private enum Stage
        {
            Introduction,
            Question,
            Reponse
        }

        public event EventHandler OutOfLevels;

        private List<LevelBase> m_levels;
        private int m_currentLevel;
        private Stage m_currentStage;
        private KeyboardState m_oldKeyboardState;
        private GamePadState m_oldGamePadState;

        private int m_answer;
        private bool m_lastResponse;
        private bool m_nextStage;

        private double m_responseTimeCounter;

        public GameManager()
        {
            m_levels = new List<LevelBase>();
            m_currentLevel = 0;
            m_answer = 0;
            m_responseTimeCounter = 3000;
            m_nextStage = false;
        }

        public void Initialize()
        {
            LevelOne levelOne = new LevelOne();
            LevelTwo levelTwo = new LevelTwo();
            LevelThree levelThree = new LevelThree();
            levelOne.EndOfLevel += onEndOfLevel;
            levelTwo.EndOfLevel += onEndOfLevel;
            levelThree.EndOfLevel += onEndOfLevel;
            levelOne.Initialize();
            levelTwo.Initialize();
            levelThree.Initialize();
            m_levels.Add(levelOne);
            m_levels.Add(levelTwo);
            m_levels.Add(levelThree);
            m_currentStage = Stage.Introduction;
            m_oldKeyboardState = Keyboard.GetState();
        }

        private void onEndOfLevel(object sender, EventArgs e)
        {
            m_currentLevel++;
            m_nextStage = true;
            if (m_currentLevel >= m_levels.Count)
                OnOutOfLevels();
        }

        public virtual void OnOutOfLevels()
        {
            OutOfLevels?.Invoke(this, null);
        }

        public void Update(GameTime time)
        {
            if (m_currentStage == Stage.Introduction)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !m_oldKeyboardState.IsKeyDown(Keys.Enter) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && !m_oldGamePadState.IsButtonDown(Buttons.A))
                {
                    m_currentStage = Stage.Question;
                    m_answer = 0;
                }
            }
            else if(m_currentStage == Stage.Question)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !m_oldKeyboardState.IsKeyDown(Keys.Up) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadUp) && !m_oldGamePadState.IsButtonDown(Buttons.DPadUp) ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.95 && m_oldGamePadState.ThumbSticks.Left.Y <= 0.95)
                {
                    m_answer += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && !m_oldKeyboardState.IsKeyDown(Keys.Down) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadDown) && !m_oldGamePadState.IsButtonDown(Buttons.DPadDown) ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.95 && m_oldGamePadState.ThumbSticks.Left.Y >= -0.95)
                {
                    m_answer -= 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && !m_oldKeyboardState.IsKeyDown(Keys.Right) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadRight) && !m_oldGamePadState.IsButtonDown(Buttons.DPadRight) ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.95 && m_oldGamePadState.ThumbSticks.Left.X <= 0.95)
                {
                    m_answer++;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && !m_oldKeyboardState.IsKeyDown(Keys.Left) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.DPadLeft) && !m_oldGamePadState.IsButtonDown(Buttons.DPadLeft) ||
                    GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.95 && m_oldGamePadState.ThumbSticks.Left.X >= -0.95)
                {
                    m_answer--;
                }
                if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !m_oldKeyboardState.IsKeyDown(Keys.Enter) ||
                    GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A) && !m_oldGamePadState.IsButtonDown(Buttons.A))
                {
                    m_lastResponse = m_levels[m_currentLevel].AnswerQuestion(m_answer);
                    m_currentStage = Stage.Reponse;
                }
            }
            else if(m_currentStage == Stage.Reponse)
            {
                m_responseTimeCounter -= time.ElapsedGameTime.TotalMilliseconds;
                if (m_responseTimeCounter <= 0)
                {
                    m_responseTimeCounter = 3000;
                    if (m_nextStage)
                    {
                        m_currentStage = Stage.Introduction;
                        m_nextStage = false;
                    }
                    else
                        m_currentStage = Stage.Question;
                    m_answer = 0;
                }
            }
            m_oldKeyboardState = Keyboard.GetState();
            m_oldGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public string GetString()
        {
            string output = "";

            switch(m_currentStage)
            {
                case Stage.Introduction:
                    output = m_levels[m_currentLevel].IntroduceNPC();
                    break;
                case Stage.Question:
                    output = m_levels[m_currentLevel].AskQuestion() + "\nAnswer: " + m_answer;
                    break;
                case Stage.Reponse:
                    output = "That is " + (m_lastResponse ? "Correct." : "Incorrect.");
                    break;
            }

            return output;
        }


    }
}
