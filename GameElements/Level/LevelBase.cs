using Assignment2.GameElements.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.Level
{
    class LevelBase
    {


        public event EventHandler EndOfLevel;

        protected List<QuestionableNPCBase> m_npcs;
        protected int m_currentNpc;
        
        public LevelBase()
        {
            m_npcs = new List<QuestionableNPCBase>();
        }

        public virtual void Initialize()
        {
            m_currentNpc = 0;
            foreach(QuestionableNPCBase npc in m_npcs)
            {
                npc.OutOfQuestions += OnOutOfQuestions;
            }
        }

        private void OnOutOfQuestions(object sender, EventArgs e)
        {
            m_currentNpc++;
            if (m_currentNpc >= m_npcs.Count)
            {
                OnEndOfLevel();
            }
        }

        public string IntroduceNPC()
        {
            return m_npcs[m_currentNpc].GetIntroduction();
        }

        public string AskQuestion()
        {
            return m_npcs[m_currentNpc].GetQuestion();
        }

        public bool AnswerQuestion(float answer)
        {
            return m_npcs[m_currentNpc].AnswerQuestion(answer);
        }

        public virtual void OnEndOfLevel()
        {
            EndOfLevel?.Invoke(this, null);
        }


    }
}
