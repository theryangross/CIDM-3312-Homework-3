using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Homework_3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            } 

        SeedDatabase();

        Console.WriteLine();
        Console.WriteLine($"Please Log in. ");
        Console.WriteLine();
        LogIn();
        }

        static void SeedDatabase()
        {
            using (var db = new AppDbContext())
            {
                List<User> Users = new List <User>();
                List<Question> Questions = new List <Question>();
                List<Answer> Answers = new List <Answer>();
            }
        }
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
                    Console.WriteLine();
                    Console.WriteLine($"Enter your first name: ");
                    newUser.FirstName = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine($"Enter your last name: ");
                    newUser.LastName = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine($"Enter your email: ");
                    newUser.Email = Console.ReadLine();
                    Console.WriteLine();
                    newUser.RegistrationDate = DateTime.Now;

                    db.Add(newUser);
                    db.SaveChanges();
                    UserInput();
                }
            }
        }

        static void UserInput()
        {
            string wrong = "";
            int UserChoice;
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();
            Console.WriteLine("Type 1 to list all questions");
            Console.WriteLine("Type 2 to list only unanswered questions"); 
            Console.WriteLine("Type 3 to ask a question");
            Console.WriteLine("Type 4 to remove a question");
            Console.WriteLine("Type 5 to answer a question");
            Console.WriteLine("Type 0 to quit");
            
            
            try{
                UserChoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch(UserChoice)
                {
                    case 0:
                        Console.WriteLine($"You have quit.");
                        break;
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
            catch
            {
                Console.WriteLine($"Unexpected value of ({wrong})");
            }
            
        }

        static void ListQuestions()
        {
            using (var db = new AppDbContext())
            {
                if(db.Questions.Count() == 0)
                {
                    Console.WriteLine($"There are currently no questions.");
                }
                else
                {
                    var listQ = db.Questions.Include(q => q.Answers);
                    foreach (var q in listQ)
                    {
                        Console.WriteLine(q.ToString()); 
                        foreach (var a in q.Answers)
                        {
                            Console.WriteLine("\t" + a.ToString());
                            Console.WriteLine();
                        }
                    }
                    
                }
                Console.WriteLine();
                UserInput();
            }
        }

        static void ListUnanswered()
        {
               using (var db = new AppDbContext())
            {
                if(db.Questions.Count() == 0)
                {
                    Console.WriteLine($"There are no unanswered questions to list.");
                }
                else
                {
                    var questions = db.Questions.Where(q => q.Answers.Count() == 0);
                    foreach (var q in questions)
                    {
                        Console.WriteLine(q.ToString());
                    }                
                }
            }
            Console.WriteLine();
            UserInput();
        }

        static void AskQuestion()
        {
            Question askQuestion = new Question();
            Console.WriteLine($"What is your question? ");
            askQuestion.QuestionText = Console.ReadLine();

            using (var db = new AppDbContext())
            {
                db.Add(askQuestion);
                db.SaveChanges();
                Console.WriteLine();
                UserInput();
            }
        }

        static void RemoveQuestion()
        {
            Console.WriteLine($"What is the QuestionID of the question you want to remove? ");
            int ID = Convert.ToInt32(Console.ReadLine());

            using (var db = new AppDbContext())
            {
                try
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
                catch
                {
                    Console.WriteLine($"That QuestionID is not valid.");
                }
                Console.WriteLine();
                UserInput();
            }
        }

        static void Answer()
        {
            using (var db = new AppDbContext())
            {
            if(db.Questions.Count() == 0)
            {
                Console.WriteLine($"There are no questions to answer.");
                Console.WriteLine();
                UserInput();
            }
            else
            {
             string UserAnswer = "";

            Console.WriteLine($"What is the QuestionID you want to answer? ");
            int QID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            
                try
                {
                    if(db.Questions.Any(a => a.QuestionID == a.QuestionID))
                    {
                        Question QtoAnswer = db.Questions.Find(QID);
                        Console.WriteLine($"Enter your answer: ");
                        UserAnswer = Console.ReadLine();

                        db.Add(UserAnswer);
                        db.SaveChanges();
                    
                    }
                    else
                    {
                        Console.WriteLine($"That QuestionID is not vailid.");
                        
                    }
                    Console.WriteLine();
                    UserInput();
                }
                catch
                {
                    Console.WriteLine();
                    UserInput();
                }
            }
            }
        }
    }
}
