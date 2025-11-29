using Mono.CSharp;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IbrahKit
{
    public static class Collection_Utilities
    {
        public static List<T> RemoveInvalid<T>(this List<T> list) where T : class
        {
            List<T> removed = new(list);
            removed.RemoveAll(x => x == null);
            return removed;
        }

        /// <summary>
        /// Shuffels a list and returns it
        /// </summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="list">The list to shuffle</param>
        /// <returns>The shuffled list</returns>
        public static List<T> Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = Random.Range(i, list.Count);

                (list[randomIndex], list[i]) = (list[i], list[randomIndex]);
            }
            return list;
        }

        /// <summary>
        /// Shuffels an array and returns it
        /// </summary>
        /// <typeparam name="T">The type of Array Elements</typeparam>
        /// <param name="list">The array to shuffle</param>
        /// <returns>The shuffled array</returns>
        public static T[] Shuffle<T>(this T[] list)
        {
            T[] result = new T[list.Length];

            for (int i = 0; i < list.Length; i++)
            {
                result[i] = list[i];
            }

            for (int i = 0; i < result.Length; i++)
            {
                int randomIndex = Random.Range(i, result.Length);
                (result[randomIndex], result[i]) = (result[i], result[randomIndex]);
            }

            return result;
        }

        /// <summary>
        /// Returns an element from an aray but clamps it at its max index to prevent outofbounds exceptions
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="array">The array to be used</param>
        /// <param name="index">The index to be used</param>
        /// <returns>An element from the array preferably at the specified index but if its outofbounds its the next closest element</returns>
        public static T GetClampedElement<T>(this T[] array, int index)
        {
            if (array.Length == 0) return default;

            return array[Mathf.Clamp(index, 0, array.Length - 1)];
        }

        /// <summary>
        /// Returns an element from a list but clamps it at its max index to prevent outofbounds exceptions
        /// </summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="list">The list to be used</param>
        /// <param name="index">The index to be used</param>
        /// <returns>An element from the list preferably at the specified index but if its outofbounds its the next closest element</returns>
        public static T GetClampedElement<T>(this List<T> list, int index)
        {
            if (list.Count == 0) return default;

            return list[Mathf.Clamp(index, 0, list.Count - 1)];
        }

        public static string[,] GetAsMatrix(string _text)
        {
            List<string> lineSplit = _text.Split('\n').ToList();

            lineSplit.RemoveAll(x => String_Utilities.IsEmpty(x.Trim()) || x.Trim() == "");

            int rowAmount = lineSplit.Count;

            int columnAmount = lineSplit[0].Split(';').Length;

            string[,] table = new string[columnAmount, rowAmount];

            for (int x = 0; x < columnAmount; x++)
            {
                for (int y = 0; y < rowAmount; y++)
                {
                    string[] rowSplit = lineSplit[y].Split(';');

                    table[x, y] = rowSplit[x];
                }
            }

            return table;
        }
    }
}
