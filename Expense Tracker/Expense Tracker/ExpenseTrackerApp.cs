using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker;

public class ExpenseTrackerApp
{
    private Account _account = new Account("Petr Pavel");

    public void Run()
    {
        Console.WriteLine("--- Welcome in your expense tracker! ---");
        while (true)
        {
            Console.WriteLine("Choose an action: (1. Add Transaction, 2. All Transactions, 3. Account Information q. Exit)");
            string input = Console.ReadLine() ?? "";
            if (input == "q") break;
            switch (input)
            {
                case "1":
                    AddTransaction();
                    break;
                case "2":
                    PrintTransactions();
                    break;
                case "3":
                    PrintAccountStatus();
                    break;
                
                default:
                    Console.WriteLine("Wrong input");
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
                        _account.AddTransaction(desc, amount, isIncome, chosenCategory);
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
        var list = _account.getAllTransactions();
        foreach (var t in list)
        {
            Console.WriteLine($"{t._Date.ToShortDateString()} | {t._Description} | {t._Amount} Kč | {t._Category} ");

        }

    }

    private void PrintAccountStatus()
    {
        Console.WriteLine($"Name: {_account._accountName}");
        Console.WriteLine($"Balance: {_account.Balance} kč.");
        Console.WriteLine($"Total income: {_account.TotalIncome} kč.");
        Console.WriteLine($"Total outcome: {_account.TotalOutcome} kč. ");
            
    }



}