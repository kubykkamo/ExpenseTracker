using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker
{
    public enum MainMenuOptions
    {
        AddTransaction = 1,
        AllTransactions = 2,
        AccountInformation = 3,
        Quit = 0
    }

    public enum FilterOptions
    {
        All = 1,
        Income = 2,
        ByHighest = 3,
        BackToMainMenu = 0
    }
}
