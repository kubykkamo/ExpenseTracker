using System;
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

    public Account() 
    {
        Categories = new List<Category>();
        Transactions = new List<Transaction>();
        

    }

    public List<Transaction> GetAllTransactions() 
    {

        return Transactions;
    }
    public void AddTransaction(string desc, decimal amount, bool isIncome, Category category) 
    {
        var transaction = new Transaction(desc, amount, isIncome, category);
        Transactions.Add(transaction);
        SaveToFile();
    }
    public string Name { get; set; }
    public decimal TotalIncome => Transactions
        .Where(t => t.IsIncome == true)
        .Sum(t => t.Amount);
    public decimal TotalOutcome => Transactions
        .Where(t => t.IsIncome == false)
        .Sum(t => t.Amount);
    public decimal Balance => TotalIncome - TotalOutcome;




    public void SaveToFile()
    {
       
        var jsonData = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(filePath, jsonData);
        ConsoleHelper.WriteSuccess("Autosave");
    }
    public void LoadFromFile()
    {
        

        if (!File.Exists(filePath))
        {
            Categories = new List<Category>();
            Transactions = new List<Transaction>();
            return;
        }

        try
        {
            string json = File.ReadAllText(filePath);
            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true            
            };

            Account loadedAccount = System.Text.Json.JsonSerializer.Deserialize<Account>(json, options);

            if (loadedAccount != null)
            {
                this.Categories = loadedAccount.Categories ?? new List<Category>();
                this.Transactions = loadedAccount.Transactions ?? new List<Transaction>();

                foreach (var t in this.Transactions)
                {
                    var originalCategory = this.Categories.FirstOrDefault(c => c.Name == t.CategoryName);
                    if (originalCategory != null)
                    {
                        t.Category = originalCategory;
                    }
                    else
                    {
                        t.Category = new Category(t.CategoryName ?? "Unknown", ConsoleColor.Gray, false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CHYBA] Nepodařilo se načíst data: {ex.Message}");
            // Inicializace prázdných seznamů, aby program nespadl dál
            Categories = new List<Category>();
            Transactions = new List<Transaction>();
        }
    }

    public Account(string accountName)
    {
        Name = accountName;
        Transactions = new List<Transaction>();
        Categories = new List<Category>();
        LoadFromFile();

    }


}