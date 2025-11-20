using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IbrahKit
{
    public static class Dropdown_Utilities
    {
        private static string DROPDOWN_DIR = "Assets/Resources/DropdownFiles/";
        private const string TXT = ".txt";

        public static void CreateDropdown(List<string> input, string fileName)
        {
            input = new(input);

            if (input == null)
            {
                IbrahDebug.LogError("Passed input list is null");
                return;
            }

            if (input.Count == 0)
            {
                IbrahDebug.LogWarning("Passed input list is empty");
                return;
            }

            if (String_Utilities.IsEmpty(fileName))
            {
                IbrahDebug.LogWarning("File name is empty or null");
                return;
            }

            for (int i = input.Count - 1; i >= 0; i--)
            {
                if (String_Utilities.IsEmpty(input[i]))
                {
                    input.RemoveAt(i);
                    IbrahDebug.Log("Removed empty element at index " + i);
                }
            }
            List<string> distinct = input.Distinct().ToList();

            if (input.Count != distinct.Count)
            {
                IbrahDebug.LogWarning("Duplicate keys found in input");

                List<string> duplicates = input.Except(distinct).ToList();

                for (int i = 0; i < duplicates.Count; i++)
                {
                    IbrahDebug.LogWarning("Duplicate: " + duplicates[i]);
                }

                return;
            }

            if (!Directory.Exists(DROPDOWN_DIR)) Directory.CreateDirectory(DROPDOWN_DIR);

            using StreamWriter sw = new(Path.Combine(DROPDOWN_DIR, fileName + TXT));

            for (int i = 0; i < input.Count; i++)
            {
                sw.WriteLine(input[i]);
            }
        }

        public static bool GetDropdown(string fileName, out IEnumerable<string> result)
        {
            string path = Path.Combine(DROPDOWN_DIR,fileName + TXT);

            if (File.Exists(path))
            {
                result = File.ReadAllLines(path).ToList();

                return true;
            }

            result = new List<string>();

            return false;
        }
    }
}