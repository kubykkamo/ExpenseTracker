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
        Console.Write("Enter transaction description: ");
        string desc = Console.ReadLine() ?? "";
        Console.Write("Enter transaction amount: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            Console.Write("Is it an income? (y/n) ");
            bool isIncome = Console.ReadLine() == "y";
            if (isIncome)
            {

                account.AddTransaction(desc, amount, isIncome, Category.Income);
            }

            
            if (!isIncome) 
            {
                Console.WriteLine("Enter transaction category: ");
                var AllCategories = Enum.GetValues<Category>();
                foreach (Category k in AllCategories)
                {
                    Console.WriteLine($"{(int)k} - {k}");
                }
                if (int.TryParse(Console.ReadLine(), out int indexChoice))
                {
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
                else
                {
                    Console.WriteLine("Error, wrong input!");
                }
              
            }
            
        }
        else 
        {
            Console.WriteLine("Wrong input!");
        }

        


    }

    private void PrintTransactions()
    {
        var list = account.getAllTransactions();
        if (!list.Any()) ConsoleHelper.WriteError("No transactions yet.");
        foreach (var t in list)
        {
            Console.WriteLine($"{t.Date.ToShortDateString()} | {t.Description} | {t.Amount} Kč | {t.Category} ");

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