using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Expense_Tracker;


public class Category {

    public string Name { get; set; }

    public decimal Limit { get; set; }

    public bool isIncome { get; set; }

    public ConsoleColor Color { get; set; }

    public Category() { }

    public Category(string description, ConsoleColor color, bool income, decimal limit)
    {
        Name = description;
        Color = color;
        isIncome = income;
        Limit = limit;
    }
    public Category(string description, ConsoleColor color, bool income)
    {

        Name = description;
        Color = color;
        Limit = -1;
        isIncome = income;
    }


    

};
