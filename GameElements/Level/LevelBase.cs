using Assignment2.GameElements.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.Level
{
    /// <summary>
    /// Abstract class that defines a level
    /// </summary>
    abstract class LevelBase
    {


        public event EventHandler EndOfLevel;

        protected List<QuestionableNPCBase> m_npcs;
        protected int m_currentNpc;
        
        /// <summary>
        /// Default constructor. Initializes NPC list
        /// </summary>
        public LevelBase()
        {
            m_npcs = new List<QuestionableNPCBase>();
        }

        /// <summary>
        /// Initializes the level. Setting the current NPC to 0 and adds the event handler to all the levels NPCs
        /// </summary>
        public virtual void Initialize()
        {
            m_currentNpc = 0;
            foreach(QuestionableNPCBase npc in m_npcs)
            {
                npc.OutOfQuestions += OnOutOfQuestions;
            }
        }

        /// <summary>
        /// The out of questions event handler
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">Event ages</param>
        private void OnOutOfQuestions(object sender, EventArgs e)
        {
            m_currentNpc++;
            if (m_currentNpc >= m_npcs.Count)
            {
                OnEndOfLevel();
            }
        }

        /// <summary>
        /// Introduce the NPC.
        /// </summary>
        /// <returns>The current NPCs introduction text</returns>
        public string IntroduceNPC()
        {
            return m_npcs[m_currentNpc].GetIntroduction();
        }

        /// <summary>
        /// Gets the current NPCs question text
        /// </summary>
        /// <returns>The current NPCs question text</returns>
        public string AskQuestion()
        {
            return m_npcs[m_currentNpc].GetQuestion();
        }
        /// <summary>
        /// Attempt to answer the question from the NPC
        /// </summary>
        /// <param name="answer">The answer supplied by the user</param>
        /// <returns>True if the question was answered correctly, false if not.</returns>
        public bool AnswerQuestion(int answer)
        {
            return m_npcs[m_currentNpc].AnswerQuestion(answer);
        }

        /// <summary>
        /// Virtual function for when the level is over.
        /// </summary>
        public virtual void OnEndOfLevel()
        {
            EndOfLevel?.Invoke(this, null);
        }


    }
}
