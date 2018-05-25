using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.GameElements.NPC
{
    class NPCGreg : QuestionableNPCBase
    {

        /// <summary>
        /// Creates the NPC greg.
        /// </summary>
        public NPCGreg()
        {

        }
        /// <summary>
        /// Initializes greg and sets all his questions
        /// </summary>
        public override void Initialize()
        {
            m_name = "Greg";
            m_introduction = "Hello there. My name is Greg. Good to meet all fo you. I work at the local grocery store. My questions will be slightly harder than Tim's, but I'm sure you will be fine.";
            m_questions.Add(new Question(200, Question.QuestionType.Addition, 33, "We have 200 apples. A truck dropped off a crate of 33. How many do I have now?"));
            m_questions.Add(new Question(44, Question.QuestionType.Addition, 31, "I have $44 in the till, if someone buys $31 of groceries off me, how much do I have in the till?"));
            m_questions.Add(new Question(50, Question.QuestionType.Subtraction, 23, "Someone buys $23 worth of groceries but he pays with a $50 note. How much change do they get back?"));
            m_questions.Add(new Question(300, Question.QuestionType.Division, 50, "I have $300 in $50 notes. How many notes is that?"));
            m_questions.Add(new Question(6, Question.QuestionType.Multiplication, 2, "I am thinking of doubling the amount of shelves I have. I currently have 6 shelves, how many will I have after?"));
            m_questions.Add(new Question(14, Question.QuestionType.Multiplication, 4, "I have 14 punnets of strawberries. I am selling them for $4 each. How much is it all worth?"));
            m_questions.Add(new Question(66, Question.QuestionType.Division, 6, "I have 6 pizzas. If I want to make $66, how much should I sell each one for?"));
            m_questions.Add(new Question(20, Question.QuestionType.Subtraction, 7, "Someone gave me a $20 note for $7 worth of products. What change should I give him?"));
            m_questions.Add(new Question(48, Question.QuestionType.Addition, 35, "I had 2 people give me apples to sell. One gave me 48 and the other gave me 35. How many do I have?"));
            m_questions.Add(new Question(83, Question.QuestionType.Addition, 47, "That’s right. I have 83. Now someone else gave me another 47. How many do I have now?"));
        }


    }
}
