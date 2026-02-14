using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace Expense_Tracker;

public class Account
{
    private List<Transaction> transactions = new List<Transaction>();
    private const string FilePath = "account_data.json";



    public List<Transaction> GetAllTransactions() 
    {

        return transactions;
    }
    public void AddTransaction(string desc, decimal amount, bool isIncome, Category category) 
    {
        var transaction = new Transaction(desc, amount, isIncome, category);
        transactions.Add(transaction);
        SaveToFile();
    }
    public string Name { get; set; }
    public decimal TotalIncome => transactions
        .Where(t => t.IsIncome == true)
        .Sum(t => t.Amount);
    public decimal TotalOutcome => transactions
        .Where(t => t.IsIncome == false)
        .Sum(t => t.Amount);
    public decimal Balance => TotalIncome - TotalOutcome;
               
                                    


    private void SaveToFile()
    {
        var jsonData = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true});
        File.WriteAllText(FilePath, jsonData);
        ConsoleHelper.WriteSuccess("Autosave");
    }

    private void LoadFromFile()
    {
        if (File.Exists(FilePath))
        {
            var jsonData = File.ReadAllText(FilePath);
            transactions = JsonSerializer.Deserialize<List<Transaction>>(jsonData) ?? new List<Transaction>();
        }
    }

    public Account(string accountName)
    {
        Name = accountName;
        LoadFromFile();

    }


}