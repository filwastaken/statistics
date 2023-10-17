using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        // List (or Array or LinkedList):
        List<string> carsList = new List<string> { "Saab", "Volvo", "BMW" };

        // Loop by indexes
        for (int i = 0; i < carsList.Count; i++)
        {
            // This is to get the i-th element
            string element = carsList[i];
        }

        // Remove first element
        carsList.RemoveAt(0);
        // Add an element
        carsList.Add("Toyota");
        // Set an element
        carsList[0] = "Fiat";
        // Check the existence
        bool includesVolvo = carsList.Contains("Volvo"); // true

        // Dictionary
        Dictionary<string, string> carDict = new Dictionary<string, string>
        {
            { "Brand", "Fiat" },
            { "Model", "Punto" },
            { "Color", "White" }
        };

        // Loop on dictionary
        foreach (var entry in carDict)
        {
            string key = entry.Key;
            string value = entry.Value;
            Console.WriteLine(key);
            Console.WriteLine(value);
        }

        // Get a value given the key
        string dictElement = carDict["Brand"];
        // Set an element
        carDict["Model"] = "Panda";
        // Check key existence
        bool keyExists = carDict.ContainsKey("key");
        // Check value existence
        List<string> carDictValues = carDict.Values.ToList();
        bool valueExists = carDictValues.Contains("value"); // false

        // Sorted list
        SortedList<string> sortedList = new SortedList<string>();
        sortedList.Add("Saab");
        sortedList.Remove("Volvo");
        sortedList.RemoveAt(0);
        sortedList.Contains("BMW");

        // Set (or HashSet)
        HashSet<string> carsSet = new HashSet<string>();
        carsSet.Add("Volvo");
        carsSet.Contains("Volvo");
        carsSet.Remove("Volvo");

        // Sorted set
        SortedSet<string> carSet = new SortedSet<string> { "Volvo", "Fiat", "Mercedes" };
        bool hasToyota = carSet.Contains("Toyota");
        carSet.Add("Toyota");
        carSet.Remove("Volvo");

        // Queue
        Queue<string> queue = new Queue<string>();
        queue.Enqueue("Volvo");
        queue.Enqueue("BMW");
        string dequeuedItem = queue.Dequeue();
        bool queueIncludes = queue.Contains("Volvo");

        // Stack
        Stack<string> stack = new Stack<string>();
        stack.Push("Volvo");
        stack.Push("Fiat");
        string poppedItem = stack.Pop();
        bool stackIncludes = stack.Contains("Fiat");
    }
}