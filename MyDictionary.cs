using System;
using System.Collections.Generic;

namespace Word_Data_Manager
{
    // Custom generic dictionary class
    public class MyDictionary<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>> entries;

        // Constructor initializes the entries list
        public MyDictionary()
        {
            entries = new List<KeyValuePair<TKey, TValue>>();
        }

        // Add a key-value pair to the dictionary
        public void Add(TKey key, TValue value)
        {
            entries.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        // Check if the dictionary contains a specific key
        public bool ContainsKey(TKey key)
        {
            return entries.Any(entry => EqualityComparer<TKey>.Default.Equals(entry.Key, key));
        }

        // Find and return the value associated with a specific key
        public TValue Find(TKey key)
        {
            var entry = entries.FirstOrDefault(e => EqualityComparer<TKey>.Default.Equals(e.Key, key));
            return entry.Value;
        }

        // Delete a key-value pair from the dictionary based on the key
        public void Delete(TKey key)
        {
            if (entries.Any(entry => EqualityComparer<TKey>.Default.Equals(entry.Key, key)))
            {
                entries.RemoveAll(entry => EqualityComparer<TKey>.Default.Equals(entry.Key, key));
                Console.WriteLine($"Word '{key}' deleted from the dictionary.");
            }
            else
            {
                Console.WriteLine($"Word '{key}' not found in the dictionary.");
            }
        }

        // Clear all entries from the dictionary
        public void Clear()
        {
            entries.Clear();
        }

        // Print all key-value pairs in the dictionary
        public void Print()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
            }
        }

        // Get a list of all key-value pairs in the dictionary
        public List<KeyValuePair<TKey, TValue>> GetEntries()
        {
            return entries;
        }
    }
}