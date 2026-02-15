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

            
            switch (input)
            {
                case 1:
                    AddTransaction();
                    break;
                case 2:
                    int sortInput = ConsoleHelper.GetInputNumber("Choose your filter: (1. No filter | 2. Incomes first | 3. Sort from lowest)");
                    switch (sortInput)
                    { 
                        case 1:
                            PrintTransactions();
                            break;
                        case 2:
                            PrintSpecificTransactions(SortByIncome());
                            break;
                        case 3:
                            PrintSpecificTransactions(SortTransactions());
                            break;
                        default:
                            ConsoleHelper.WriteError("Wrong input.");
                            break;
                    }
                    break;
                case 3:
                    PrintAccountStatus();
                    break;
                case 4:
                    PrintSpecificTransactions(SortByIncome());
                    break;
                case -1:
                    return;
                default:
                    ConsoleHelper.WriteError("Wrong input");
                    continue;


            }
        }
    }












    private void AddTransaction() 
    {
        string desc = ConsoleHelper.GetInputString("Enter transaction description");
        decimal amount = ConsoleHelper.GetInputDecimal("Enter transaction amount");
        
        
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
                    ConsoleHelper.WriteSuccess("Transaction added.");
            }
                else
                {
                    Console.WriteLine("Error, non existing category!");
                }
                    

              
            }
            
        }
        
    List<Transaction> GetTransactions()
    {
        return account.GetAllTransactions();
    }
    List<Transaction> SortTransactions()
    {

        var transactions =
                GetTransactions()
                .OrderBy(t => t.Amount)
                .ToList();
        return transactions;
    }

    List<Transaction> SortByIncome()  
    {
        var transactions = GetTransactions()
            .GroupBy(t => t.IsIncome)
            .SelectMany(g => g.OrderByDescending(t => t.Date))
            .ToList();
        return transactions;
    }




    private void PrintSpecificTransactions(List<Transaction> transactionsToPrint)
    {
        account.GetAllTransactions();
        if (!transactionsToPrint.Any()) ConsoleHelper.WriteError("No transactions yet.");
        else
        {
            Console.WriteLine("--- Your transactions history ---");
            foreach (var t in transactionsToPrint)
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

    private void PrintTransactions()
    {
        var transactions = account.GetAllTransactions();
        if (!transactions.Any()) { ConsoleHelper.WriteError("No transactions yet."); } 
       
        else
        {
            var transactionsToPrint = transactions
            .OrderByDescending(t => t.Date)
            .ToList();
            Console.WriteLine("--- Your transactions history ---");
            foreach (var t in transactionsToPrint)
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
        Console.WriteLine($"Name: {account.Name}");
        Console.WriteLine($"Balance: {account.Balance} kč.");
        Console.WriteLine($"Total income: {account.TotalIncome} kč.");
        Console.WriteLine($"Total outcome: {account.TotalOutcome} kč. ");
            
    }



}