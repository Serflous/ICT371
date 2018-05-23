using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.NPC
{
    class NPCJill : QuestionableNPCBase
    {


        public NPCJill()
        {

        }

        public override void Initialize()
        {
            m_name = "Jill";
            m_introduction = "Hi all. I'm Jill. I work at a nearby farm. I hope you are good at numbers in the hundreds, because I deal with a lot of stuff.";
            m_questions.Add(new Question(100, Question.QuestionType.Multiplication, 3, "I have 100 chickens. Each one is expected to lay 3 eggs a day. How many eggs should I get each day?"));
            m_questions.Add(new Question(300, Question.QuestionType.Multiplication, 7, "Yup 300 eggs. Now how many is that per week?"));
            m_questions.Add(new Question(238, Question.QuestionType.Addition, 192, "I have 238 apple trees. I've planted another 192. How many apple trees do I have now?"));
            m_questions.Add(new Question(464, Question.QuestionType.Subtraction, 233, "I had 464 pear trees, but a fire burned through 233 of them. How many trees do I have left?"));
            m_questions.Add(new Question(714, Question.QuestionType.Subtraction, 654, "My last corn crop produced 714kg of corn. This one produced 654kg of corn. How much did it go down by?"));
            m_questions.Add(new Question(264, Question.QuestionType.Division, 2, "I had 264 calves born. I sold half of them to a neighbouring farm. How many do I have left."));
            m_questions.Add(new Question(333, Question.QuestionType.Division, 3, "I now have 333 chickens. I have 3 large areas for them to live in. If I was to split them equally, how many would be in each area?"));
            m_questions.Add(new Question(192, Question.QuestionType.Addition, 433, "I had 192kgs of apples. A neighbouring farm gave me an extra 433kgs. How many do I have now?"));
            m_questions.Add(new Question(625, Question.QuestionType.Addition, 127, "Yep, 625kgs. I just picked another 127kgs. How many do I have now?"));
            m_questions.Add(new Question(752, Question.QuestionType.Subtraction, 493, "Well done, 752kg. I just sold 493kgs. How many do I have left?"));
            base.Initialize();
        }


    }
}
