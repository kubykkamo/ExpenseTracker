using System;
using System.Collections.Generic;
using System.Text;

namespace Expense_Tracker;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class FileStorageService
{
    private readonly string _transactionsPath = "transactions.json";
    private readonly string _categoriesPath = "categories.json"; // Nová cesta

    // --- Metody pro Transakce (už máš) ---
    public void SaveTransactions(List<Transaction> transactions)
    {
        string jsonString = JsonSerializer.Serialize(transactions);
        File.WriteAllText(_transactionsPath, jsonString);
    }

    public List<Transaction> LoadTransactions()
    {
        if (!File.Exists(_transactionsPath)) return new List<Transaction>();
        string jsonString = File.ReadAllText(_transactionsPath);
        return JsonSerializer.Deserialize<List<Transaction>>(jsonString) ?? new List<Transaction>();
    }

    // --- NOVÉ: Metody pro Kategorie ---
    public void SaveCategories(List<Category> categories)
    {
        // Kategorie jsou jen seznam textů, JSON si s tím poradí úplně stejně
        string jsonString = JsonSerializer.Serialize(categories);
        File.WriteAllText(_categoriesPath, jsonString);
    }

    // Ve FileStorageService.cs změň návratový typ:
    public List<Category> LoadCategories()
    {
        if (!File.Exists(_categoriesPath)) return new List<Category>();
        string jsonString = File.ReadAllText(_categoriesPath);
        // Tady řekni JsonSerializeru, že má vytvořit List<Category>
        return JsonSerializer.Deserialize<List<Category>>(jsonString) ?? new List<Category>();
    }
}