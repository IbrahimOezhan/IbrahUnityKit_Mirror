using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IbrahKit
{
    public static class Dropdown_Utilities
    {
        private static string directory = "Assets/Resources/DropdownFiles/";
        private const string fileEnding = ".txt";

        public static void CreateDropdown(List<string> input, string fileName)
        {
            input = new(input);

            if (input == null)
            {
                Debug.LogWarning("Passed input list is null");
                return;
            }

            if (input.Count == 0)
            {
                Debug.LogWarning("Passed input list is empty. Possible error");
                return;
            }

            if (String_Utilities.IsEmpty(fileName))
            {
                Debug.LogWarning("File name is empty or null");
                return;
            }

            int count = input.Count;

            input.RemoveAll(x => String_Utilities.IsEmpty(x));

            int removed = count - input.Count;

            if (removed > 0)
            {
                Debug.LogWarning("Removed " + removed + " empty elements");
            }

            List<string> distinct = input.Distinct().ToList();

            if (input.Count != distinct.Count)
            {
                Debug.LogWarning("Duplicate keys found in input");

                List<string> duplicates = input.Except(distinct).ToList();

                for (int i = 0; i < duplicates.Count; i++)
                {
                    Debug.LogWarning("Duplicate: " + duplicates[i]);
                }

                return;
            }

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            using (StreamWriter sw = new(GetPath(fileName)))
            {
                for (int i = 0; i < input.Count; i++)
                {
                    sw.WriteLine(input[i]);
                }
            }
        }

        public static bool GetDropdown(string fileName, out IEnumerable<string> result)
        {
            string path = GetPath(fileName);

            if (File.Exists(path))
            {
                result = File.ReadAllLines(path).ToList();
                return true;
            }

            result = new List<string>();
            return false;
        }

        private static string GetPath(string fileName)
        {
            return Path.Combine(directory, fileName + fileEnding);
        }
    }
}