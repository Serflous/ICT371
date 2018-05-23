using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.NPC
{
    class Question
    {

        public enum QuestionType
        {
            Addition, Multiplication, Subtraction, Division
        }

        private float m_a, m_b;
        private QuestionType m_type;
        private string m_questionString;

        public string QuestionString
        {
            get
            {
                return m_questionString;
            }
        }

        public Question(float a, QuestionType type, float b, string questionString)
        {
            m_a = a;
            m_b = b;
            m_type = type;
            m_questionString = questionString;
        }

        public bool AnswerQuestion(float answer)
        {
            float actualAnswer = 0;
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
            return answer == actualAnswer;

        }


    }
}
