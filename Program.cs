using System;
using DataTypes;
using BuiltInFunctions;
using Learning.NumericUtilities;
using KeywordsDemo.KeywordsDemoClass;
using ControlFlowDemo.ControlFlowExamples;
using ArraysDemo.Arr;
using ListDemo.ListMethodsDemo;
using ClassDemoWithExamples.ClassDemo;
using LinkConceptsDemo;

//is bad design because:namespace should not usually include class name IMPORTANT

//A namespace in C# is used to organize code into logical groups and avoid naming conflicts.
//Think of it like a folder structure:
//Company
// ├── ProjectA
// │    └── Class1.cs
// └── ProjectB
//      └── Class1.cs


//namespace App1
//{
//    class Program { }
//}

//namespace App2
//{
//    class Program { }
//}




namespace MainApp
{
   class Program
   {
       static void Main(string[] args)
       {

            // DataTypesClass.ShowDataTypes();
            // BuiltInFunctionsClass.ShowBuiltInFunctions();
            // NumericUtilitiesDemo.Run();
            
            // KeywordsDemoClass.ShowKeywords();
            // ControlFlowExamples.RunAllExamples();
            // Arr.TakeArrayInput();

            // ListMethodsDemo.ShowListMethods();
            // ClassDemo.Run();

                LinkConceptsScenario.Run();


       }
       
   }

}



