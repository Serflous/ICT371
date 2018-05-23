using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.NPC
{
    class NPCTim : QuestionableNPCBase
    {

        public NPCTim()
        {

        }

        public override void Initialize()
        {
            m_name = "Tim";
            m_introduction = "Hello everyone. My name is Tim, and I'm the local builder. I have a couple questions that someone smart might be able to help me with.";
            m_questions.Add(new Question(20, Question.QuestionType.Subtraction, 3, "I have a pack of 20 nails. I used 3 for a different project. How many nails do I have left?"));
            m_questions.Add(new Question(17, Question.QuestionType.Addition, 20, "So I have 17. I bought another pack of 20. How many do I have now?"));
            m_questions.Add(new Question(6, Question.QuestionType.Multiplication, 4, "Now that I have an accurate nail count, I need some wood. I need 6 lots of 4 planks, how many planks do I need?"));
            m_questions.Add(new Question(24, Question.QuestionType.Division, 3, "These planks of wood need to divided equally between 3 people. How many does each person get?"));
            m_questions.Add(new Question(24, Question.QuestionType.Multiplication, 10, "I need to use 10 nails per plank. I have 24 planks all up, how many nails do I need?"));
            m_questions.Add(new Question(240, Question.QuestionType.Division, 20, "So I need 240 nails. They come in packs of 20. How many packs do I need to buy?"));
            m_questions.Add(new Question(12, Question.QuestionType.Multiplication, 4, "Each pack costs $4. Since I need 12 packs, how much will that cost?"));
            m_questions.Add(new Question(30, Question.QuestionType.Addition, 25, "One of the tree people are going to pay me $30, and anther is going to pay me $25. How much will I get from them?"));
            m_questions.Add(new Question(55, Question.QuestionType.Addition, 40, "The last person is going to pay me $40. In addition to the $55 I got from the last 2, how much will I have now?"));
            m_questions.Add(new Question(95, Question.QuestionType.Subtraction, 5, "And finally, I need to give $5 of this to someone, how much will I have left over?"));
        }


    }
}
