using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManager
{
      public sealed class ConfigurationManager
    {
        private static ConfigurationManager instance = null;
        private static readonly object padlock = new object();
        private Dictionary<string, string> settings = new Dictionary<string, string>();

        ConfigurationManager()
        {
        }

        public static ConfigurationManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ConfigurationManager();
                    }
                    return instance;
                }
            }
        }

        public string GetSetting(string key)
        {
            return settings.ContainsKey(key) ? settings[key] : null;
        }

        public void SetSetting(string key, string value)
        {
            settings[key] = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter command (get, set, or exit):");
                string command = Console.ReadLine();

                if (command == "exit")
                {
                    break;
                }

                if (command == "get")
                {
                    Console.WriteLine("Enter key:");
                    string key = Console.ReadLine();
                    string value = ConfigurationManager.Instance.GetSetting(key);
                    Console.WriteLine("Value: " + value);
                }
                else if (command == "set")
                {
                    Console.WriteLine("Enter key:");
                    string key = Console.ReadLine();
                    Console.WriteLine("Enter value:");
                    string value = Console.ReadLine();
                    ConfigurationManager.Instance.SetSetting(key, value);
                }
            }
        }
    }

}
