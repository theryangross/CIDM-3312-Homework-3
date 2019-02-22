/*  
    Each user can post many questions; each question is posted by one user.
    Each user can post many answers; each answer is posted by one user.
    Each question can have many answers; each answer applies to one question.
*/
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Homework_3
{
public class AppDbContext : DbContext
    {
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database.db");
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Question> Questions {get; set;}
        public DbSet<Answer> Answers {get; set;}
    }

    public class User
    {
        public string UserID {get; set;}    // PK
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public DateTime RegistrationDate {get; set;}
        public List<Question> Questions {get; set;}
        public List<Answer> Answers {get; set;}

        public override string ToString()
        {
            return ($"ID: {UserID}: {FirstName} {LastName}, {Email}, {RegistrationDate}");
        }
    }

    public class Question
    {
        public int QuestionID {get; set;}   // PK
        public string QuestionText {get; set;}
        public DateTime QuestionDate {get; set;}
        public string UserID {get; set;}    // FK
        public List<Answer> Answers {get; set;}

        public override string ToString()
        {
            return ($"ID: {QuestionID}, User: {UserID} - {QuestionText} - {QuestionDate}");
        }
    }

    public class Answer
    {
        public int AnswerID {get; set;}     // PK
        public string AnswerText {get; set;}
        public DateTime AnswerDate {get; set;}
        public string UserID {get; set;}    // FK
        public int QuestionID {get; set;}   //FK

        public override string ToString()
        {
            return ($"ID: {AnswerID}: {AnswerText} - {AnswerDate}");
        }
    }
}