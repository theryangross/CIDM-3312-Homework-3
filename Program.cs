using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Homework_3
{
    class Program
    {
        static void LogIn()
        {
            string email = "";

            Console.WriteLine("What is your email?");
            email = Console.ReadLine();

            using (var db = new AppDbContext())
            {
                User Email = db.User.Where(u => u.Email == email);
            }
        }


        static void ListQuestions()
        {
            using (var db = new AppDbContext())
            {
                var questions = db.Questions.Include(q => q);
                foreach (var q in questions)
                {
                    Console.WriteLine(db.ToString());
                }
            }
        }

        static void ListUnanswered()
        {
               using (var db = new AppDbContext())
            {
                var questions = db.Questions.Include(q => q).Where(q => q.QuestionID);
                foreach (var q in questions)
                {
                    Console.WriteLine(db.ToString());
                }
            }
        }

        static void AskQuestion()
        {
            string UserAsk = "";

            Console.WriteLine($"What is your question? ");
            UserAsk = Console.ReadLine();

            using (var db = new AppDbContext())
            {
                Question askQuestion = new Question {QuestionText = UserAsk};
                // Question.User = db.Users.First();
                db.Add(UserAsk);
                db.SaveChanges();
            }
        }

        static void RemoveQuestion()
        {
            int UserRemove = 0;

            Console.WriteLine($"What is the QuestionID of the question you want to remove? ");
            UserRemove = Convert.ToInt32(Console.ReadLine());

            using (var db = new AppDbContext())
            {
                Question.Find(q => q.QuestionID == UserRemove);
                db.Remove(UserRemove);
                db.SaveChanges();
            }
        }

        static void Answer()
        {
             string UserAnswer = "";

            Console.WriteLine($"Type your answer: ");
            UserAnswer = Console.ReadLine();

            using (var db = new AppDbContext())
            {
                Question answerQuestion = new Question {AnswerText = UserAnswer};
                // Question.User = db.Users.First();
                db.Add(UserAnswer);
                db.SaveChanges();
            }
        }


        static void Main(string[] args)
        {
             using (var db = new AppDbContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        } 

            int UserChoice;
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("Type 1 to list all questions");
            Console.WriteLine("Type 2 to list only unanswered questions"); 
            Console.WriteLine("Type 3 to ask a question");
            Console.WriteLine("Type 4 to remove a question");
            Console.WriteLine("Type 5 to answer a question");
            
            UserChoice = Convert.ToInt32(Console.ReadLine());
            switch(UserChoice)
            {
                case 1:
                    ListQuestions();
                    break;
                case 2:
                    ListUnanswered();
                    break;
                case 3:
                    AskQuestion();
                    break;
                case 4:
                    RemoveQuestion();
                    break;
                case 5:
                    Answer();
                    break;
                default:
                    Console.WriteLine($"Unexpected value of ({UserChoice})");
                    break;
            }
        }
    }
}
