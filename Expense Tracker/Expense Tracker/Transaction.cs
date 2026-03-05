using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Expense_Tracker;

public class Transaction
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsIncome { get; set; }

    public string CategoryName { get; set; }
    
    private Category _category;
    [JsonIgnore]
    public Category Category
    {
        get { return _category; }
        set
        {
            _category = value;
            if (value != null)
            {
                Category.Name = value.Name;
            }
        }
    }


    public Transaction(string description, decimal amount, bool isIncome, Category category)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Částka musí být větší než nula.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Popis nesmí být prázdný.");
        }

        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        IsIncome = isIncome;

        CategoryName = category.Name;
        Category = category;
    }

    public Transaction() { }

    public void PrintTransactionInfo() 
    {
        Console.ForegroundColor = Category.Color;
        string categoryDisplayName = Category?.Name ?? CategoryName ?? "Bez kategorie";

        Console.WriteLine($"{Date.ToShortDateString()} | {Description} | {Amount} Kč | {categoryDisplayName}");
        Console.ResetColor();
        

    }
}
