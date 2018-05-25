using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.NPC
{
    abstract class QuestionableNPCBase : IQuestionableNPC
    {


        public event EventHandler OutOfQuestions;

        protected List<Question> m_questions;
        protected int m_currentIndex;
        protected string m_name;
        protected string m_introduction;

        /// <summary>
        /// Base constructor for a questionable npc. Creates a new list of questions and sets the current index to 0
        /// </summary>
        public QuestionableNPCBase()
        {
            m_questions = new List<Question>();
            m_currentIndex = 0;
        }
        /// <summary>
        /// Overridable function to initialize the NPC
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// Attempt to answer the current question with an answer supplied by the player
        /// </summary>
        /// <param name="x">The answer</param>
        /// <returns>True if correct, false if not</returns>
        public bool AnswerQuestion(int x)
        {
            bool result = m_questions[m_currentIndex].AnswerQuestion(x);
            if (result)
                m_currentIndex++;
            if (m_currentIndex >= m_questions.Count)
                OnOutOfQuestions();
            return result;
        }
        /// <summary>
        /// Gets the name of the NPC
        /// </summary>
        /// <returns>the name of the npc</returns>
        public string GetName()
        {
            return m_name;
        }
        /// <summary>
        /// Gets the introductory text of the npc
        /// </summary>
        /// <returns>The introduction text</returns>
        public string GetIntroduction()
        {
            return m_introduction;
        }
        /// <summary>
        /// Gets the question text being asked
        /// </summary>
        /// <returns>The question text</returns>
        public string GetQuestion()
        {
            return m_questions[m_currentIndex].QuestionString;
        }
        /// <summary>
        /// Overidable function called when theres no more questions.
        /// </summary>
        public virtual void OnOutOfQuestions()
        {
            OutOfQuestions?.Invoke(this, null);
        }
    }
}
