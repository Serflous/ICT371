using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.NPC
{
    interface IQuestionableNPC
    {
        
        void Initialize();
        string GetName();
        string GetQuestion();
        string GetIntroduction();
        bool AnswerQuestion(int x);


    }
}
