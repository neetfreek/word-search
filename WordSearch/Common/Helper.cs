/*==========================================================*
*  Common helper methods for Word Search.                   *
*===========================================================*
*  1. Return all STRING elements in array1D capitalised     *
*  2. Return counts:                                        *
*   a. Characters in STRINGS                                *
*   b. Digits in INTEGERS                                   *
*   c. Elements in arrays1D                                 * 
*  3. Return longest STRING in STRING array1D               *
*  4. Return random (inclusive) CHARACTERS (a-z), INTEGERS  *
*===========================================================*/
using System;
using System.Text.RegularExpressions;

namespace WordSearch.Common
{
    public static class Helper
    {
        /*==================================================================================*
        *  Used in Random() methods                                                         *
        *  Delcared here to prevent same Random.Next() values when used in quick succession *
        *===================================================================================*/
        private static Random random = new Random();

        /*======================================*
        *  1. Return lower case Words array1D  *
        *=======================================*/
        public static string[] LowerCase(string[] array)
        {
            string[] arrayLowerCase = new string[array.Length];
            string wordLowerCase = "";

            for (int word = 0; word < array.Length; word++)
            {
                wordLowerCase = array[word].ToLower();
                arrayLowerCase[word] = wordLowerCase;
            }
            return arrayLowerCase;
        }

        /*==================*
        *  2. Return counts *
        *===================*/
        public static int CountDigits(int number)
        {
            int numCharsInInt = number.ToString().Length;

            return numCharsInInt;
        }
        public static int CountWordsCharactersAll(string[] array)
        {
            int count = 0;
            foreach (string word in array)
            {
                foreach (char character in word)
                {
                    count++;
                }
            }
            return count;
        }
        public static int CountElements(string[] array)
        {
            int count = 0;
            foreach (string word in array)
            {
                count++;
            }
            return count;
        }

        /*==========================*
        *  3. Return longest word   *
        *===========================*/
        public static string LongestWord(string[] array)
        {
            string longestWord = "";

            foreach (string word in array)
            {
                if (word.Length > longestWord.Length)
                {
                    longestWord = word;
                }
            }
            return longestWord;
        }

        /*======================================*
        *  Return CHARACTER matrix as vector    *
        *=======================================*/
        public static char[] MatrixToVector(char[,] matrix)
        {
            // Declare, initialise vector with default (0) values
            char[] vector = new char[matrix.Length];

            int counter = 0;

            // Iterate rows
            for (int row = 0; row < matrix.GetLength(0); row++)
            {   // Iterate columns (elements)
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    // Assign col value to vector
                    vector[counter] = matrix[row, col];
                    counter++;
                }
            }

            return vector;
        }

        /*==========================*
        *  4. Return random values  *
        *===========================*/
        public static int Random(int intMin, int intMax)
        {
            int intRandom = random.Next(intMin, intMax + 1);

            return intRandom;
        }
        public static char Random(char min, char max)
        {
            char minFixed = char.ToLower(min);
            char maxFixed = char.ToLower(max);
            string charSetTotal = "abcdefghijklmnopqrstuvwxyz";
            int indexStart = charSetTotal.IndexOf(minFixed);
            int indexEnd = charSetTotal.IndexOf(maxFixed);
            int charsToInsertLength = (indexEnd - indexStart) + 1;

            // Return default null value('\0') if min or max not letter
            if (indexStart == -1 | indexEnd == -1)
            {
                return ('\0');
            }

            string charSet = charSetTotal.Substring(indexStart, charsToInsertLength);
            char charToInsert = charSet[Random(0, charSet.Length - 1)];

            return charToInsert;
        }

        /*======================================*
        *  Return CHARACTER matrix as vector    *
        *=======================================*/
        public static string RemoveNonLetters(string word)
        {
            string wordsSanitised = Regex.Replace(word, "[^A-Za-z ]", "");

            return wordsSanitised;
        }

        public static string UppercaseFirst(string word)
        {
            string upperCase = word;
            char.ToUpper(upperCase[0]);

            return char.ToUpper(upperCase[0]) + upperCase.Substring(1);
        }
    }
}