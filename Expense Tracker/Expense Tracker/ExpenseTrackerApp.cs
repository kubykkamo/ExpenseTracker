using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker;

public class ExpenseTrackerApp
{
    private Account account = new Account("Petr Pavel");

    public void Run()
    {
        Console.WriteLine("--- Welcome in your expense tracker! ---");
        while (true)
        {
            int input = ConsoleHelper.GetInputNumber("Choose an action: (1. Add Transaction | 2. All Transactions | 3. Account Information | q Exit)");

            if (input == -1) break; ;
            switch (input)
            {
                case 1:
                    AddTransaction();
                    break;
                case 2:
                    PrintTransactions();
                    break;
                case 3:
                    PrintAccountStatus();
                    break;
                
                default:
                    ConsoleHelper.WriteError("Wrong input");
                    continue;


            }
        }
    }












    private void AddTransaction() 
    {
        string desc = ConsoleHelper.GetInputString("Enter transaction description");
        int amount = ConsoleHelper.GetInputNumber("Enter transaction amount");
        
        
            Console.Write("Is it an income? (y/n) ");
            bool isIncome = Console.ReadLine() == "y";
            if (isIncome)
            {

                account.AddTransaction(desc, amount, isIncome, Category.Income);
            }

            
            if (!isIncome) 
            {
                
                var AllCategories = Enum.GetValues<Category>();
                Console.WriteLine("---Your categories---");
                foreach (Category k in AllCategories)
                {
                    Console.WriteLine($"{(int)k} - {k}");
                }
            Console.WriteLine    ("---------------------");
                int indexChoice = ConsoleHelper.GetInputNumber("Enter a category");


            if (Enum.IsDefined(typeof(Category), indexChoice))
                {
                        Category chosenCategory = (Category)indexChoice;
                        account.AddTransaction(desc, amount, isIncome, chosenCategory);
                }
                else
                {
                    Console.WriteLine("Error, non existing category!");
                }
                    

              
            }
            
        }
        

        


    

    private void PrintTransactions()
    {
        var list = account.getAllTransactions();
        if (!list.Any()) ConsoleHelper.WriteError("No transactions yet.");
        else
        {
            Console.WriteLine("--- Your transactions history ---");
            foreach (var t in list)
            {
                if (t.IsIncome)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{t.Date.ToShortDateString()} | {t.Description} | {t.Amount} Kč | {t.Category} ");
                    Console.ResetColor();
                }

                if (!t.IsIncome)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{t.Date.ToShortDateString()} | {t.Description} | {t.Amount} Kč | {t.Category} ");
                    Console.ResetColor();
                }


            }
            Console.WriteLine("---------------------------------");
        }
    }

    private void PrintAccountStatus()
    {
        Console.WriteLine($"Name: {account._accountName}");
        Console.WriteLine($"Balance: {account.Balance} kč.");
        Console.WriteLine($"Total income: {account.TotalIncome} kč.");
        Console.WriteLine($"Total outcome: {account.TotalOutcome} kč. ");
            
    }



}