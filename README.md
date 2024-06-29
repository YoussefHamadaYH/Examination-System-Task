# Examination-System-Task
A C# Examination system with functionalities for different question types, logging exam in file, and exam management.
Features:
Question Object: Base class and derived classes for different question types (True/False, Choose One, Choose All).
Question List: Inherits from List<Question>, overrides Add method to log questions to a file.
Exam Class: Base class with common attributes and methods. Derived classes for Practice Exam and Final Exam.
Main Program: Allows the user to select the exam type and displays the exam.
Usage:
Define questions and add them to QuestionList.
Create Exam objects with defined questions.
Run the main program to select and display exam type.
