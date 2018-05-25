using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.NPC
{
    public class Question
    {

        public enum QuestionType
        {
            Addition, Multiplication, Subtraction, Division
        }

        private int m_a, m_b;
        private QuestionType m_type;
        private string m_questionString;
        /// <summary>
        /// The question text.
        /// </summary>
        public string QuestionString
        {
            get
            {
                return m_questionString;
            }
        }
        /// <summary>
        /// Creates a question to ask the player
        /// </summary>
        /// <param name="a">The first value</param>
        /// <param name="type">The type of question</param>
        /// <param name="b">The second value</param>
        /// <param name="questionString">The worded questions</param>
        public Question(int a, QuestionType type, int b, string questionString)
        {
            m_a = a;
            m_b = b;
            m_type = type;
            m_questionString = questionString;
        }
        /// <summary>
        /// Attempts to answer the question with the value supplied by the player
        /// </summary>
        /// <param name="answer">True if the answer is correct, false if not.</param>
        /// <returns></returns>
        public bool AnswerQuestion(int answer)
        {
            int actualAnswer = 0;
            switch(m_type)
            {
                case QuestionType.Addition:
                    actualAnswer = m_a + m_b;
                    break;
                case QuestionType.Subtraction:
                    actualAnswer = m_a - m_b;
                    break;
                case QuestionType.Multiplication:
                    actualAnswer = m_a * m_b;
                    break;
                case QuestionType.Division:
                    actualAnswer = m_a / m_b;
                    break;
            }
            bool result = answer == actualAnswer;
            if (!result)
                Counter.Instance.WrongAnswer(m_type);
            return result;

        }

    }
}
