using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Expense_Tracker;

public class Account
{
    private List<Transaction> transactions = new List<Transaction>();


    public List<Transaction> getAllTransactions() 
    {

        return transactions;
    }
    public void AddTransaction(string desc, decimal amount, bool isIncome, Category category) 
    {
        var transaction = new Transaction(desc, amount, isIncome, category);
        transactions.Add(transaction);
        ConsoleHelper.WriteSuccess("Transaction added.");
    }
    public string _accountName { get; set; }
    public decimal TotalIncome => transactions
        .Where(t => t.IsIncome == true)
        .Sum(t => t.Amount);
    public decimal TotalOutcome => transactions
        .Where(t => t.IsIncome == false)
        .Sum(t => t.Amount);
    public decimal Balance => TotalIncome - TotalOutcome;
               
                                    


    

    public Account(string accountName)
    {
        _accountName = accountName;
        
    }


}