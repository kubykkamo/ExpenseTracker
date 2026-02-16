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
                string input = ConsoleHelper.GetInputString(question);
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

        static public decimal GetInputDecimal(string question)
        {

            while (true)
            {
                string input = ConsoleHelper.GetInputString(question);
                if (input == "q" || input == "Q")
                {
                    return -1;
                }


                if (decimal.TryParse(input, out decimal number))
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
            while (true)
            {
                Console.WriteLine(question + ": ");
                string input = Console.ReadLine() ?? "";

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                ConsoleHelper.WriteError("Invalid input, enter a number!");
                
            }
        }
        
        public static void PrintMenuFromEnum<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));

            foreach (var value in values)
            {
                int number = (int)value;
                string name = value.ToString() ?? "";

                
                string polishedName = System.Text.RegularExpressions.Regex.Replace(name, "(\\B[A-Z])", " $1");

                Console.WriteLine($"{number}. {polishedName}");
            }
        }

        public enum CategoryColors 
        {
            Red = 1,
            Green,
            Blue,
            Yellow,
            Cyan,       // Azurová (světle modrá)
            Magenta,    // Fialová
            White,
            Purple,
            Pink,  
            Gray        // Šedá (pro méně důležité věci)
        }

        public static ConsoleColor ToConsoleColor(CategoryColors categoryColor)
        {
            return categoryColor switch
            {
                CategoryColors.Red => ConsoleColor.Red,
                CategoryColors.Green => ConsoleColor.Green,
                CategoryColors.Blue => ConsoleColor.Blue,
                CategoryColors.Yellow => ConsoleColor.Yellow,
                CategoryColors.Cyan => ConsoleColor.Cyan,
                CategoryColors.Magenta => ConsoleColor.Magenta,
                CategoryColors.White => ConsoleColor.White,
                CategoryColors.Purple => ConsoleColor.DarkMagenta,
                CategoryColors.Pink => ConsoleColor.Magenta,
                CategoryColors.Gray => ConsoleColor.Gray,
                _ => ConsoleColor.White
            };
        }
    }



    
}
