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

        public static Counter Instance
        {
            get
            {
                if (me == null)
                    me = new Counter();
                return me;
            }
        }

        private Counter()
        {
            m_incorrectResponses = new Dictionary<Question.QuestionType, int>();
            m_incorrectResponses.Add(Question.QuestionType.Addition, 0);
            m_incorrectResponses.Add(Question.QuestionType.Subtraction, 0);
            m_incorrectResponses.Add(Question.QuestionType.Multiplication, 0);
            m_incorrectResponses.Add(Question.QuestionType.Division, 0);
        }

        public void WrongAnswer(Question.QuestionType type)
        {
            m_incorrectResponses[type]++;
        }

        public int GetWrongAnswers(Question.QuestionType type)
        {
            return m_incorrectResponses[type];
        }

        public void Reset()
        {
            m_incorrectResponses[Question.QuestionType.Addition] = 0;
            m_incorrectResponses[Question.QuestionType.Subtraction] = 0;
            m_incorrectResponses[Question.QuestionType.Multiplication] = 0;
            m_incorrectResponses[Question.QuestionType.Division] = 0;
        }


    }
}
