using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Expense_Tracker;

public class Account
{
    private List<Transaction> _transactions = new List<Transaction>();


    public List<Transaction> getAllTransactions() 
    {

        return _transactions;
    }
    public void AddTransaction(string desc, decimal amount, bool isIncome, Category category) 
    {
        var transaction = new Transaction(desc, amount, isIncome, category);
        _transactions.Add(transaction);
        Console.WriteLine("Transaction added.");
    }
    public string _accountName { get; set; }
    public decimal TotalIncome => _transactions
        .Where(t => t._IsIncome == true)
        .Sum(t => t._Amount);
    public decimal TotalOutcome => _transactions
        .Where(t => t._IsIncome == false)
        .Sum(t => t._Amount);
    public decimal Balance => TotalIncome - TotalOutcome;
               
                                    


    

    public Account(string accountName)
    {
        _accountName = accountName;
        
    }


}