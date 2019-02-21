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
                if(db.Users.Any(e => e.Email == email))
                {
                    Console.WriteLine("You are now logged in. ");
                }
                else
                {
                    Console.WriteLine($"Your email does not exsist.");
                    User newUser = new User();
                    Console.WriteLine($"Enter your first name: ");
                    newUser.FirstName = Console.ReadLine();
                    Console.WriteLine($"Enter your last name: ");
                    newUser.LastName = Console.ReadLine();
                    Console.WriteLine($"Enter your email: ");
                    newUser.Email = Console.ReadLine();
                    newUser.RegistrationDate = DateTime.Now;

                    db.Add(newUser);
                    db.SaveChanges();
                }
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
                var questions = db.Questions.Include(q => q).Where(q => q.Answers.Count() == 0);
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
            Console.WriteLine($"What is the QuestionID of the question you want to remove? ");
            int ID = Convert.ToInt32(Console.ReadLine());

            using (var db = new AppDbContext())
            {
                if(db.Users.Any(u => u.UserID == u.UserID)){
                    Question QtoRemove = db.Questions.Find(ID);
                    db.Remove(QtoRemove);
                    db.SaveChanges();
                    Console.WriteLine($"Question {QtoRemove} has been removed.");
                }
                else
                {
                    Console.WriteLine($"You can not delete questions you didn't ask.");
                }
            }
        }

        static void Answer()
        {
             string UserAnswer = "";

            Console.WriteLine($"What is the QuestionID you want to answer? ");
            int QID = Convert.ToInt32(Console.ReadLine());

            using (var db = new AppDbContext())
            {
                Question QtoAnswer = db.Questions.Find(QID);
                UserAnswer = Console.ReadLine();

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

         
        }
    }
}
