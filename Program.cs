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

        }

        static void AskQuestion()
        {

        }

        static void RemoveQuestion()
        {

        }

        static void Answer()
        {

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
