using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker
{
    public static class ConsoleHelper
    {
        public static void WriteError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }

        static public int GetInputNumber(string question)
        {

            while (true)
            {
                Console.Write(question + " ");
                string input = Console.ReadLine() ?? "";
                if (input == "q" || input == "Q")
                {
                    return -1;
                }


                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int number))
                {
                    return number;
                }

                ConsoleHelper.WriteError("Invalid input, enter a number!");
            }
        }
        public static void WriteSuccess(string successMessge)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(successMessge);
            Console.ResetColor();
        }

        public static string GetInputString(string question)
        {
            string outpout = "";
            return outpout;
        }
    }

    
}
