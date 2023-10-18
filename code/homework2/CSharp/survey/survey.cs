using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourNamespace
{
  public class FrequencyClass
  {
    public async Task DistributionAsync()
    {
      string csvUrl = "https://bluecheese-fil.github.io/src/hw2/survey/professional_life.csv";
      using (HttpClient client = new HttpClient())
      {
        string data = await client.GetStringAsync(csvUrl);
        string[] lines = data.Split('\n');
        string[] badkeys = lines[0].Split(',');
        
        List<string> keys = new List<string>();
        for (int i = 0; i < badkeys.Length; i++) keys.Add(badkeys[i].Trim());
        
        List<List<string>> table = new List<List<string>>();
        for (int i = 0; i < lines.Length - 1; i++) table.Add(new List<string>());
        
        for (int i = 0; i < lines.Length - 1; i++)
        {
          string[] row = lines[i + 1].Split(',');
          for (int j = 0; j < keys.Count; j++) table[i].Add(row[j].Trim());
        }

        return keys, table;
      }
    }
    
    public void CalculateDistribution(string key1, string key2)
    {
      List<string> keys = ...; // Retrieve keys from the DistributionAsync method result
      List<List<string>> table = ...; // Retrieve table from the DistributionAsync method result
    
      // Find the index of key1 in the keys list
      int index1 = keys.IndexOf(key1);
    
      // Create a dictionary to store the items and their frequencies
      Dictionary<string, int> items = new Dictionary<string, int>();

      if (key2 == key1 || key2 == "None")
      {
        for (int i = 0; i < table.Count; i++)
        {
          string answer = table[i][index1];
          if (!items.ContainsKey(answer)) items[answer] = 1;
          else items[answer]++;
        }
      }
      else
      {
        // Find the index of key2 in the keys list
        int index2 = keys.IndexOf(key2);

        for (int i = 0; i < table.Count; i++)
        {
          string answer = table[i][index1] + "|" + table[i][index2];
          if (!items.ContainsKey(answer)) items[answer] = 1;
          else items[answer]++;
        }
      }

      // Calculate relative and percentage frequencies
      Dictionary<string, double> relativeFrequencies = new Dictionary<string, double>();
      Dictionary<string, double> percentageFrequencies = new Dictionary<string, double>();

      int totalCount = table.Count;
      foreach (var item in items)
      {
        double relativeFrequency = (double)item.Value / totalCount;
        double percentageFrequency = relativeFrequency * 100;
        
        relativeFrequencies[item.Key] = relativeFrequency;
        percentageFrequencies[item.Key] = percentageFrequency;
      }

      return table, relativeFrequencies, percentageFrequencies
    }
  }
  
  class Program
  {
    static async Task Main(string[] args)
    {
      FrequencyClass freq = new FrequencyClass();
      await freq.DistributionAsync();

      CalculateDistribution('key1', 'key2');
    }
  }
}
