
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace Expense_Tracker;

public class Account
{
    
    public List<Transaction> Transactions{ get; set; }
    public List<Category> Categories { get; set; }
    private const string filePath = "account_data.json";

    public void PrintCategories() 
    {
        int i = 1;
        Console.WriteLine("---Your categories---");
        foreach (Category k in Categories)
        {
            Console.WriteLine($"{i++} - {k.Name}");
        }
        Console.WriteLine("---------------------");
    }


    public List<Transaction> GetAllTransactions() 
    {

        return Transactions;
    }
    public void AddTransaction(string desc, decimal amount, bool isIncome, Category category) 
    {
        
        var transaction = new Transaction(desc, amount, isIncome, category);
        Transactions.Add(transaction);
    }
    public string Name { get; set; }
    public decimal TotalIncome => Transactions
        .Where(t => t.IsIncome == true)
        .Sum(t => t.Amount);
    public decimal TotalOutcome => Transactions
        .Where(t => t.IsIncome == false)
        .Sum(t => t.Amount);
    public decimal Balance => TotalIncome - TotalOutcome;



    public Account(List<Transaction> transactions, List<Category> categories)
    {
        Transactions = transactions;
        Categories = categories;
        foreach (var t in transactions)
        {

            t.Category = Categories.FirstOrDefault(c => c.Name == t.CategoryName);
        }



    }
}