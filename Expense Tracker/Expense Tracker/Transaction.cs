using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker;

public class Transaction
{
    public string _Description { get; set; }
    public decimal _Amount { get; set; }
    public DateTime _Date { get; set; }
    public bool _IsIncome { get; set; }
    public Category _Category { get; set; }

    public Transaction(string description, decimal amount, bool isIncome, Category category)
    {
        _Description = description;
        _Amount = amount;
        _Date = DateTime.Now;
        _IsIncome = isIncome;
        _Category = category;
    }
}
