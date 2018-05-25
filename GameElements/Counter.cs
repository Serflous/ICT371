using Assignment2.GameElements.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements
{
    class Counter
    {


        private static Counter me;

        private Dictionary<Question.QuestionType, int> m_incorrectResponses;
        /// <summary>
        /// Gets the singleton instance of the class
        /// </summary>
        public static Counter Instance
        {
            get
            {
                if (me == null)
                    me = new Counter();
                return me;
            }
        }
        /// <summary>
        /// Private constructor ensuring singleton.
        /// Creates the dictionary and sets the initial values to 0
        /// </summary>
        private Counter()
        {
            m_incorrectResponses = new Dictionary<Question.QuestionType, int>();
            m_incorrectResponses.Add(Question.QuestionType.Addition, 0);
            m_incorrectResponses.Add(Question.QuestionType.Subtraction, 0);
            m_incorrectResponses.Add(Question.QuestionType.Multiplication, 0);
            m_incorrectResponses.Add(Question.QuestionType.Division, 0);
        }
        /// <summary>
        /// Called when the player inputs a wrong answer, and will increment that type.
        /// </summary>
        /// <param name="type">The type to increment</param>
        public void WrongAnswer(Question.QuestionType type)
        {
            m_incorrectResponses[type]++;
        }
        /// <summary>
        /// Gets the count of wrong answers for the specified type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns></returns>
        public int GetWrongAnswers(Question.QuestionType type)
        {
            return m_incorrectResponses[type];
        }
        /// <summary>
        /// Resets the counter values back to 0
        /// </summary>
        public void Reset()
        {
            m_incorrectResponses[Question.QuestionType.Addition] = 0;
            m_incorrectResponses[Question.QuestionType.Subtraction] = 0;
            m_incorrectResponses[Question.QuestionType.Multiplication] = 0;
            m_incorrectResponses[Question.QuestionType.Division] = 0;
        }


    }
}
