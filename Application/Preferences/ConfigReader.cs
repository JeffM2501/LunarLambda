using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LunarLambda.Preferences
{
    public class ConfigReader
    {
        public  SortedDictionary<string, string> Values = new SortedDictionary<string, string>();

        public bool Read(Stream stream)
        {
            if (stream == null)
                return false;

            try
            {
                StreamReader sr = new StreamReader(stream);
                List<string> lines = new List<string>();
                while (!sr.EndOfStream)
                    lines.Add(sr.ReadLine());

                sr.Close();
                ReadValues(lines);
            }
            catch (Exception)
            {

                return false;
            }
            return Values.Count > 0;
        }

        public bool Read(string fileName)
        {
            if (!File.Exists(fileName))
                return false;

            ReadValues(File.ReadAllLines(fileName));

            return Values.Count > 0;
        }

        protected void ReadValues(IEnumerable<string> lines)
        {
            foreach(string line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                string[] parts = line.Split(new string[] { " = " }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                    Values[parts[0]] = parts[1];
            }
        }
    }
}
