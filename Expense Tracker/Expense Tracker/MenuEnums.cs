using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker
{
    public enum MainMenuOptions
    {
        NewTransaction = 1,
        NewCategory = 2,
        Transactions = 3,
       
        AccountInformation = 4,
        Quit = 0
    }

    public enum FilterOptions
    {
        NoFilter = 1,
        SortByIncome = 2,
        SortByHighest = 3,
        BackToMainMenu = 0
    }
}
