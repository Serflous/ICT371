using Assignment2.GameElements.NPC;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Tests
{
    [TestFixture]
    public class QuestionTest
    {

        [Test]
        public void TestAddition()
        {
            Question question = new Question(2, Question.QuestionType.Addition, 2, "2+2");
            Assert.IsTrue(question.AnswerQuestion(4));
        }

        [Test]
        public void TestSubtraction()
        {
            Question question = new Question(2, Question.QuestionType.Subtraction, 2, "2-2");
            Assert.IsTrue(question.AnswerQuestion(0));
        }

        [Test]
        public void TestMultiplication()
        {
            Question question = new Question(2, Question.QuestionType.Multiplication, 2, "2*2");
            Assert.IsTrue(question.AnswerQuestion(4));
        }

        [Test]
        public void TestDivision()
        {
            Question question = new Question(2, Question.QuestionType.Division, 2, "2/2");
            Assert.IsTrue(question.AnswerQuestion(1));
        }

        [Test]
        public void TestQuestionReturnQuestion()
        {
            Question question = new Question(2, Question.QuestionType.Division, 2, "2/2");
            bool equals = question.QuestionString.Equals("2/2");
            Assert.IsTrue(equals);
        }

    }
}
