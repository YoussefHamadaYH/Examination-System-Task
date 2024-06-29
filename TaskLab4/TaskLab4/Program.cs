using System.Collections.Generic;
using System.IO;
namespace TaskLab4
{
    // cerate class Question with abstarct
     abstract class Questions
    {
        public string Body { get; set; }
        public int Marks { get; set; }
        public string Header { get; set; }

        public Questions(string header, string body, int marks)
        {
            Body = body;
            Marks = marks;
            Header = header;
        }
        public override string ToString() { 
            return $"A- {Header} Question \n  Question Body: {Body}  Marks:({Marks})";
        }
    }
    //Class True & False 
     class TrueAndFalse : Questions
    {
        public bool TrueOrFalse { get; set; }

        public TrueAndFalse(  string header, string body, int marks)
            : base( header,  body,  marks)
        {
           this.TrueOrFalse = TrueOrFalse;
        }
        public override string ToString() { 
            string txt = base.ToString();
            return txt+"\n" + $"(True or False?)" ;
        }
    }
    //Class Chooses
    class ChooseOne : Questions
    {
        public List<string> Choices { get; set; }
        //public string CorrectChoice { get; set; }
        public ChooseOne(string body, int marks, string header,List<string> choices,string CorrectChoice="") :base(header, body, marks) 
        {
           this.Choices = choices;
           //this.CorrectChoice = CorrectChoice;
        }
        public override string ToString()
        {
            string txt = base.ToString();
            txt += "Choose one: ";
            for (int i = 0; i < Choices.Count; i++)
            {
                txt += $"  {i + 1} - {Choices[i]} ";
            }
            return txt;
        }
    }
    class ChooseAll : Questions
    {
        public List<string> Choices { get; set; }
        //public List<int> CorrectChoices { get; set; }
        public ChooseAll(string body, int marks, string header, List<string> choices, List<int> correctChoices=null) : base(header, body, marks)
        {
            this.Choices = choices;
            //this.CorrectChoices = correctChoices;
        }
        public override string ToString()
        {
            string txt = base.ToString();
            txt += "Choose All Right: ";
            for (int i = 0; i < Choices.Count; i++)
            {
                txt += $" {Choices[i]}";
            }

            return txt;
        }
    }
    //Question list Class
    class QuestionList:List<Questions>
    {
        readonly string path;
       
        public QuestionList(string path)
        {
            this.path = path;
        }
        public new void Add(Questions q)
        {
            base.Add(q);
            TextWriter writer = new StreamWriter(path);
            writer.WriteLine(q);
            writer.Close();
        }
    }
    class Subject
    {
        public string subjectName { get; set; }

        public Subject(string subjectName)
        {
            this.subjectName = subjectName;
        }
        public override string ToString()
        {
            return $"Subject Name : {subjectName}";
        }
    }

    //Class Answer
    abstract class Exam
    {
        public int time { get; set; }
        public int numberOfQuestions { get; set; }
        public Subject Subject { get; set; }
        public Dictionary<Questions, string> QuestionAnswer { get; set; }

        protected Exam(int time, int numberOfQuestions, Subject subject)
        {
            this.time = time;
            this.numberOfQuestions = numberOfQuestions;
            Subject = subject;
            QuestionAnswer = new Dictionary<Questions, string>();
        }
        public abstract void ShowExam();
    }

    class practiceExam : Exam
    {
        public QuestionList questions  { get; set; }
        public practiceExam(int time, int numberOfQuestions, Subject subject, string path)
           : base(time, numberOfQuestions, subject)
        {
            this. time=time;
            questions = new QuestionList(path);
        }
        public override void ShowExam()
        {
            foreach (Questions question in questions)
            {
                //Console.WriteLine();
                Console.WriteLine($" Time Exam :({time} hours) {question }");
                Console.WriteLine("Answer : ");
                string result = Console.ReadLine();
                if (result == QuestionAnswer[question])
                {
                    Console.WriteLine($"Right result");

                }
                else
                {
                    Console.WriteLine($" Wrong result. Correct Answer is : {QuestionAnswer[question]}");
                }
                
            }
        }

    }
    class FinalExam : Exam
    {
        public QuestionList Questions { get; set; }

        public FinalExam(int time, int numberOfQuestions, Subject subject, string path)
            : base(time, numberOfQuestions, subject)
        {
            Questions = new QuestionList(path);
        }

        public override void ShowExam()
        {
            foreach (Questions question in Questions)
            {
                Console.WriteLine(question);
                Console.WriteLine("Solve it . if there exist multi value enter sperated by ,");
                string result = Console.ReadLine();
                if (result.Trim() == QuestionAnswer[question])
                {
                    Console.WriteLine($"Right result");
                }
                else
                {
                    Console.WriteLine($" Wrong result. Correct Answer is : {QuestionAnswer[question]}");
                }

            }
        }
    }

   internal class Program
     {
        static void Main(string[] args)
        {
            //Firstly Create 2 Subjects
            Subject mathematics = new Subject("Mathematics");
            Subject geography = new Subject("Geography");
            // Create Practice Exam
            practiceExam practiceExam = new practiceExam(3, 2, mathematics, "G:\\ADVDay4\\practExam.txt");
            //Modify TrueAndFalse Question
            TrueAndFalse Q1 = new TrueAndFalse("Mathematics", "Is 1 + 1 equal to 2?", 2);
            //Add Q1 to practiceExam
            practiceExam.questions.Add(Q1);
            //Add Answer for Q1
            practiceExam.QuestionAnswer.Add(Q1, "True");

            //Create Question 2 
            ChooseOne Q2 = new ChooseOne("What is the capital of France?", 3, "Geography", new List<string> { "Paris", "London", "Berlin" }, "First Choice");
            //Add Q2 to practiceExam
            practiceExam.questions.Add(Q2);
            //Add Answer for Q2
            practiceExam.QuestionAnswer.Add(Q2 , "Paris");

            //Create Final Exam
            FinalExam finalExam = new FinalExam(3, 2, mathematics, "G:\\ADVDay4\\finalExam.txt");
            //Creat Choose All Q3
            ChooseAll Q3 = new ChooseAll("Select all prime numbers:", 2, "Mathematics", new List<string> { "2", "3", "5", "7", "9" });
            //Add Q3 to FinalExam
            finalExam.Questions.Add(Q3);
            //Add Answer for Q3
            finalExam.QuestionAnswer.Add(Q3, "2,3,5,7");
            Console.WriteLine("Select exam type: (1). Practice Exam (2). Final Exam");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                practiceExam.ShowExam();
            }
            if (choice == 2)
            {
                finalExam.ShowExam();
            }
        }
    }
}
