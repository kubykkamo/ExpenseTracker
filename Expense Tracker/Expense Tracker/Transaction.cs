using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Expense_Tracker;

public class Transaction
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsIncome { get; set; }
    public Category Category { get; set; }

    public Transaction(string description, decimal amount, bool isIncome, Category category)
    {
        Description = description;
        Amount = amount;
        Date = DateTime.Now;
        IsIncome = isIncome;
        Category = category;
    }

    public void PrintTransactionInfo() 
    {
        string categoryName = Category.ToString();
        categoryName = System.Text.RegularExpressions.Regex.Replace(categoryName, "(\\B[A-Z])", " $1");
        Console.WriteLine($"{Date.ToShortDateString()} | {Description} | {Amount} Kč | {categoryName} ");

    }
}
