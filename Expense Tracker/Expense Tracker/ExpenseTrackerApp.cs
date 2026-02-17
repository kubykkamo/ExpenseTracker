using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Expense_Tracker;


public class ExpenseTrackerApp
{
    public Account account = new Account("Petr Pavel");
    

    

    public void Run()
    {
        Console.WriteLine("--- Welcome in your expense tracker! ---");
        
        while (true)
        {
            Console.WriteLine("--- Main Menu ---");
            ConsoleHelper.PrintMenuFromEnum<MainMenuOptions>();
            Console.WriteLine("-----------------");
            int input = ConsoleHelper.GetInputNumber("Choose an action");
            MainMenuOptions action = (MainMenuOptions)input;
            
            switch (action)
            {
                case MainMenuOptions.NewTransaction:
                    AddTransaction();
                    break;
                case MainMenuOptions.NewCategory:
                    AddCateogry();
                    break;
                case MainMenuOptions.Transactions:
                    ShowAndChooseFilter();
                    break;
                case MainMenuOptions.AccountInformation:
                    PrintAccountStatus();
                    break;                
                case MainMenuOptions.Quit:
                    account.SaveToFile();
                    return;
                default:
                    ConsoleHelper.WriteError("Wrong input");
                    continue;


            }
        }
    }









    private void ShowAndChooseFilter()
    {
        bool inFilterMenu = true;
        while (inFilterMenu)
        {
            
            Console.Clear();
            Console.WriteLine("--- Filters ---");
            ConsoleHelper.PrintMenuFromEnum<FilterOptions>();
            Console.WriteLine("---------------");
            int sortInput = ConsoleHelper.GetInputNumber("Choose your filter");
            FilterOptions action = (FilterOptions)sortInput;
            switch (action)
            {
                case FilterOptions.NoFilter:
                    PrintTransactions();
                    break;
                case FilterOptions.SortByIncome:
                    PrintSpecificTransactions(SortByIncome());
                    break;
                case FilterOptions.SortByHighest:
                    PrintSpecificTransactions(SortTransactions());
                    break;
                case FilterOptions.SortByCategory:
                    PrintFilteredTransactions();
                    break;
                case FilterOptions.BackToMainMenu:
                    inFilterMenu = false;
                    break;
                default:
                    ConsoleHelper.WriteError("Wrong input.");
                    break;
            }

            if (inFilterMenu)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    bool IsWithinLimit(Category cat, decimal amount)
    {
        if (cat.Limit == -1) return true;
        decimal sum = account.GetAllTransactions()
            .Where(x => x.Category.Name == cat.Name)
            .Sum (x => x.Amount);
        sum += amount;
        return sum <= cat.Limit;
    }

    Category ChooseCategory() 
    {
        var categories = account.Categories;
        account.PrintCategories();
        int indexChoice = ConsoleHelper.GetInputNumber("Choose a category");
        indexChoice--;
        if (indexChoice < 0 || indexChoice >= categories.Count)
        {
            ConsoleHelper.WriteError("Wrong input. Transaction not added.");
            return null;
        }

        else
        {

            return categories[indexChoice];
        }

    }
    void AddCateogry()
    {
        string categoryName = ConsoleHelper.GetInputString("Enter category name");

        ConsoleHelper.PrintMenuFromEnum<ConsoleHelper.CategoryColors>();
        int categoryColor = ConsoleHelper.GetInputNumber("Enter category color");
        ConsoleHelper.CategoryColors selectedEnum = (ConsoleHelper.CategoryColors)categoryColor;
        ConsoleColor finalColor = ConsoleHelper.ToConsoleColor(selectedEnum);
        string incomeBool = ConsoleHelper.GetInputString("Is it an income category? (y/n)").ToLower();
        bool isIncome;
        if (incomeBool == "y")
        {
            isIncome = true;
        }
        else if (incomeBool == "n")
        {
            isIncome = false;
            string limitBool = ConsoleHelper.GetInputString("Do you want to set a limit? (y/n)").ToLower();
            if (limitBool == "y")
            {
                decimal limit = ConsoleHelper.GetInputDecimal("Enter your limit");
                account.Categories.Add(new Category(categoryName, finalColor, isIncome, limit));
                ConsoleHelper.WriteSuccess("Category added.");

            }
            else 
            {
                account.Categories.Add(new Category(categoryName, finalColor, isIncome));
                ConsoleHelper.WriteSuccess("Category added.");
                return;
            }
            
        }
        else
        {
            ConsoleHelper.WriteError("Wrong input. Category not added.");
            return;
        }
        
        

    }
    private void AddTransaction() 
    {
        string desc = ConsoleHelper.GetInputString("Enter transaction description");
        decimal amount = ConsoleHelper.GetInputDecimal("Enter transaction amount");
        bool isIncome;

        
        string incomeInput = ConsoleHelper.GetInputString(("Is it an income? (y/n)")).ToLower();
        if (incomeInput != "y" && incomeInput != "n")
        {
            ConsoleHelper.WriteError("Wrong input. Transaction not added.");

        }
        else
        {
            isIncome = incomeInput == "y";


            

            Category chosenCategory = ChooseCategory();
            if (chosenCategory == null)
            {
                return;
            }

            else
            {
                account.AddTransaction(desc, amount, isIncome, chosenCategory);
                ConsoleHelper.WriteSuccess("Transaction added.");
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
                .OrderByDescending(t => t.Amount)
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


    private void PrintFilteredTransactions()
    {
        Category chosenCategory = ChooseCategory();
        if (chosenCategory == null)
        {
            return;
        }
        
        var transactionsToPrint = account.GetAllTransactions()
            .Where(t => t.Category.Name == chosenCategory.Name)
            .OrderByDescending(t => t.Date)
            .ToList();
        PrintSpecificTransactions(transactionsToPrint);
    }



    private void PrintSpecificTransactions(List<Transaction> transactionsToPrint)
    {
        account.GetAllTransactions();
        if (!transactionsToPrint.Any()) ConsoleHelper.WriteError("No transactions yet.");
        else
        {
            Console.WriteLine("--- Your transaction history ---");
            foreach (var t in transactionsToPrint)
            {
                if (t.IsIncome)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    t.PrintTransactionInfo();
                    Console.ResetColor();
                }

                if (!t.IsIncome)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    t.PrintTransactionInfo();
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
                    t.PrintTransactionInfo();
                    Console.ResetColor();
                }

                if (!t.IsIncome)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    t.PrintTransactionInfo();
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