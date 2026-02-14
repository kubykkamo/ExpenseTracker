using System;
using System.Collections.Generic;
using System.Text;

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
}
