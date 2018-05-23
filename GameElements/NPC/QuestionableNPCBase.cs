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


        public QuestionableNPCBase()
        {
            m_questions = new List<Question>();
            m_currentIndex = 0;
        }

        public virtual void Initialize()
        {

        }

        public bool AnswerQuestion(float x)
        {
            bool result = m_questions[m_currentIndex].AnswerQuestion(x);
            if (result)
                m_currentIndex++;
            if (m_currentIndex >= m_questions.Count)
                OnOutOfQuestions();
            return result;
        }

        public string GetName()
        {
            return m_name;
        }

        public string GetIntroduction()
        {
            return m_introduction;
        }

        public string GetQuestion()
        {
            return m_questions[m_currentIndex].QuestionString;
        }

        public virtual void OnOutOfQuestions()
        {
            OutOfQuestions?.Invoke(this, null);
        }
    }
}
