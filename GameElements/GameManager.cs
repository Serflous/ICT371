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

        private double m_responseTimeCounter;

        public GameManager()
        {
            m_levels = new List<LevelBase>();
            m_currentLevel = 0;
            m_answer = 0;
            m_responseTimeCounter = 3000;
        }

        public void Initialize()
        {
            LevelOne levelOne = new LevelOne();
            levelOne.EndOfLevel += onEndOfLevel;
            levelOne.Initialize();
            m_levels.Add(levelOne);
            m_currentStage = Stage.Introduction;
            m_oldKeyboardState = Keyboard.GetState();
        }

        private void onEndOfLevel(object sender, EventArgs e)
        {
            m_currentLevel++;
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
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !m_oldKeyboardState.IsKeyDown(Keys.Enter))
                {
                    m_currentStage = Stage.Question;
                    m_answer = 0;
                }
            }
            else if(m_currentStage == Stage.Question)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && !m_oldKeyboardState.IsKeyDown(Keys.Up))
                {
                    m_answer += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && !m_oldKeyboardState.IsKeyDown(Keys.Down))
                {
                    m_answer -= 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && !m_oldKeyboardState.IsKeyDown(Keys.Right))
                {
                    m_answer++;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && !m_oldKeyboardState.IsKeyDown(Keys.Left))
                {
                    m_answer--;
                }
                if(Keyboard.GetState().IsKeyDown(Keys.Enter) && !m_oldKeyboardState.IsKeyDown(Keys.Enter))
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
                    m_currentStage = Stage.Question;
                }
            }
            m_oldKeyboardState = Keyboard.GetState();
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
